using RepairFlatWPF.Controller;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Data.SQLite;
using Newtonsoft.Json;

namespace RepairFlatWPF.UserControls
{
    /// <summary>
    /// Interaction logic for MakeLoading.xaml
    /// </summary>
    public partial class MakeLoading : UserControl
    {
        public MakeLoading()
        {
            InitializeComponent();
            ((Storyboard)FindResource("WaitStoryboard")).Begin();
            MakeDownload();
        }

        public async void MakeDownload()
        {
            await Task.Run(() => MakeWorkWirthDataBase.MakeFilePathAndCheck());
            string CheckLastDate = "Select Max(idUpdate), DateOfUpdate from DateOfLastUpdate;";
            var MaxDate = Task.Run(() => MakeWorkWirthDataBase.MakeSomeQueryWork(CheckLastDate, WorkWithTables: true));

            if (MaxDate.Result != null)
            {
                DataTable infAbDate = MaxDate.Result as DataTable;
                if (infAbDate.Rows.Count != 0)
                {//Берем с датой
                    DescriptionOfWork.Content = "Получение данных об услугах с сервера ...";
                    string someDate = infAbDate.Rows[0]["DateOfUpdate"].ToString();
                    //Дата получена

                    #region Получение данных от сервера
                    DescriptionOfWork.Content = "Получение данных об услугах с сервера ...";
                    var ServisesDownload = await Task.Run(() => MakeDownloadByLink($"api/substring/servises/get?dateofclientlastupdate={someDate}")); //Скачивание данных о услугах
                    var ContaktsDownload = await Task.Run(() => MakeDownloadByLink($"api/substring/contact/get?dateofclientlastupdate={someDate}")); //Скачивание данных о контактах
                    DescriptionOfWork.Content = "Получение данных о типах помещений с сервера ...";
                    var PremisesDownload = await Task.Run(() => MakeDownloadByLink($"api/substring/premises/get?dateofclientlastupdate={someDate}")); //Скачивание данных о типах помещений
                    DescriptionOfWork.Content = "Получение данных о материалах с сервера ...";
                    var MaterialDownload = await Task.Run(() => MakeDownloadByLink($"api/substring/material/get?dateofclientlastupdate={someDate}")); //Скачивание данных о материалах
                    #endregion
                    DescriptionOfWork.Content = "Запись данных в локальную базу данных ...";

                    #region Преобразование данных в читаемое состояние
                    MakeSubs.ServisesMake servisesMake = JsonConvert.DeserializeObject<MakeSubs.ServisesMake>(ServisesDownload.ToString());

                    #endregion

                    #region Загрузка данных в локальную БД
                    if (servisesMake.success)
                        await Task.Run(() => ServisesUplToDB(servisesMake));
                    #endregion


                    return;
                }

            }
            //Берем все
            //TODO ПРОТЕСТИТЬ
            //TODO Загрузить
            //TODO Протестить локаль          
        }

        public object MakeDownloadByLink(string UrlOfDownload)
        {
            return BaseWorkWithServer.CatchErrorWithGet(UrlOfDownload, "GET", nameof(MakeLoading), nameof(MakeDownloadByLink));
        }

        public void ServisesUplToDB(MakeSubs.ServisesMake InformationAboutServises)
        {
            string MakeQuery = "Insert into OurServices (idServis,Nomination,TypeOfServices,UnitOfMeasue,Cost,Descriprtion) Values (@idServis,@Nomination,@TypeOfServices,@UnitOfMeasue,@Cost,@Descriprtion) On CONFLICT(idServis) DO UPDATE SET  Nomination=@Nomination,TypeOfServices=@TypeOfServices,UnitOfMeasue=@UnitOfMeasue,Cost=@Cost,Descriprtion=@Descriprtion;";
            MakeWorkWirthDataBase.Run((command) =>
            {
                foreach (var ServisUpdate in InformationAboutServises.ListOfServises)
                {
                    SQLiteParameter[] parameters = new SQLiteParameter[6];
                    parameters[0] = new SQLiteParameter("@idServis", ServisUpdate.idServises);
                    parameters[1] = new SQLiteParameter("@Nomination", ServisUpdate.Nomination);
                    parameters[2] = new SQLiteParameter("@TypeOfServices", ServisUpdate.TypeOfServises);
                    parameters[3] = new SQLiteParameter("@UnitOfMeasue", ServisUpdate.UnitOfMeasue);
                    parameters[4] = new SQLiteParameter("@Cost", ServisUpdate.Cost);
                    parameters[5] = new SQLiteParameter("@Descriprtion", ServisUpdate.Description);
                    command.Parameters.AddRange(parameters);
                    command.CommandText = MakeQuery;
                    command.ExecuteNonQuery();
                }
            });
        }

    }
}

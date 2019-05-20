using RepairFlatWPF.Controller;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Data.SQLite;

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
                    string someDate = infAbDate.Rows[0]["DateOfUpdate"].ToString();
                    //Дата получена

                    //TODO Прописать ссылки
                    DescriptionOfWork.Content = "Получение данных об услугах с сервера ...";
                    var ServisesDownload = await Task.Run(() => MakeDownloadByLink("")); //Скачивание данных о услугах
                    var ContaktsDownload = await Task.Run(() => MakeDownloadByLink("")); //Скачивание данных о контактах
                    DescriptionOfWork.Content = "Получение данных о типах помещений с сервера ...";
                    var PremisesDownload = await Task.Run(() => MakeDownloadByLink("")); //Скачивание данных о типах помещений
                    DescriptionOfWork.Content = "Получение данных о материалах с сервера ...";
                    var MaterialDownload = await Task.Run(() => MakeDownloadByLink("")); //Скачивание данных о материалах
                    DescriptionOfWork.Content = "Запись данных в локальную базу данных ...";

                    return;
                }

            }
            //Берем все




        }

        public object MakeDownloadByLink(string UrlOfDownload)
        {
            return BaseWorkWithServer.CatchErrorWithGet(UrlOfDownload, "GET", nameof(MakeLoading), nameof(MakeDownloadByLink));
        }

        public void ServisesUplToDB(MakeSubs.ServisesMake InformationAboutServises)
        {
            MakeWorkWirthDataBase.Run((command) =>
            {
                foreach(var ServisUpdate in InformationAboutServises.ListOfServises)
                {
                   // string MakeQuery=""
                }
            });
        }

    }
}

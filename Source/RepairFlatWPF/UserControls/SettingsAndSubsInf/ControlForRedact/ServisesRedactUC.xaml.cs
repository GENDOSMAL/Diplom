using Newtonsoft.Json;
using RepairFlat.Model;
using RepairFlatWPF.Controller;
using RepairFlatWPF.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static RepairFlat.Model.MakeSubs;

namespace RepairFlatWPF.UserControls.SettingsAndSubsInf.ControlForRedact
{
    /// <summary>
    /// Interaction logic for ServisesRedactUC.xaml
    /// </summary>
    public partial class ServisesRedactUC : UserControl
    {
        BaseWindow window;
        Guid idServises;
        bool Redact = false;
        decimal cost;

        public ServisesRedactUC(ref BaseWindow baseWindow, object DataAboutServises = null)
        {
            InitializeComponent();
            window = baseWindow;
            if (DataAboutServises != null)
            {
                Redact = true;
                var obf = DataAboutServises as RepairFlat.Model.MakeSubs.ListOfServises;
                NameOfServises.Text = obf.Nomination?.Trim();
                TypeOfServis.Text = obf.TypeOfServises?.Trim();
                Cost.Text = obf.Cost.Value.ToString();
                Descriprtion.Text = obf.Description?.Trim();
                AddBtn.Content = "Редактировать";
                idServises = obf.idServises;
            }
            else
            {
                idServises = Guid.NewGuid();
            }
        }

        private void RetutnBTN_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckData())
            {
                string query = "";
                if (!Redact)
                {
                    query = "Insert into OurServices (idServis,Nomination,TypeOfServices,Cost,Descriprtion) values (@idServis,@Nomination,@TypeOfServices,@Cost,@Descriprtion)";
                }
                else
                {
                    query = "Update OurServices set Nomination=@Nomination, TypeOfServices=@TypeOfServices, Cost=@Cost , Descriprtion=@Descriprtion where idServis=@idServis;";
                }
                SQLiteParameter[] sQLiteParameter = new SQLiteParameter[5];
                sQLiteParameter[0] = new SQLiteParameter("@idServis", idServises.ToString());
                sQLiteParameter[1] = new SQLiteParameter("@Nomination", NameOfServises.Text.Trim());
                sQLiteParameter[2] = new SQLiteParameter("@TypeOfServices", TypeOfServis.Text?.Trim());
                sQLiteParameter[3] = new SQLiteParameter("@Cost", cost);
                sQLiteParameter[4] = new SQLiteParameter("@Descriprtion", Descriprtion.Text?.Trim());
                MakeWorkWirthDataBase.MakeSomeQueryWork(query, parameters: sQLiteParameter);
                MakeUpdateServer();
            }
        }
        private async void MakeUpdateServer()
        {
            MakeUpdOrInsServises makeUpdOrInsServ = new MakeUpdOrInsServises();
            makeUpdOrInsServ.idUser = SaveSomeData.IdUser ?? default;
            makeUpdOrInsServ.DateOfMake = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            ListOfServises listOfPost = new ListOfServises
            {
                idServises = idServises,
                Cost = cost,
                Description = Descriprtion.Text?.Trim(),
                Nomination = NameOfServises.Text.Trim(),
                TypeOfServises = TypeOfServis.Text?.Trim()
            };

            makeUpdOrInsServ.ListOfServises = new List<ListOfServises>();
            makeUpdOrInsServ.ListOfServises.Add(listOfPost);
            string Json = JsonConvert.SerializeObject(makeUpdOrInsServ);
            string urlSend = "api/substring/servises/update";
            var task = await Task.Run(() => BaseWorkWithServer.CatchErrorWithPost(urlSend, "POST", Json, nameof(BaseWorkWithServer), nameof(MakeUpdateServer)));
            var deserializedProduct = JsonConvert.DeserializeObject<BaseResult>(task.ToString());
            if (!deserializedProduct.success)
            {
                MakeSomeHelp.MSG($"Произошла ошикбка при работе {deserializedProduct.description}", MsgBoxImage: MessageBoxImage.Error);
            }
            else
            {
                MakeSomeHelp.MSG("Операции над данными были произведены!", MsgBoxImage: MessageBoxImage.Information);
            }
            window.Close();
        }

        bool CheckData()
        {
            if (string.IsNullOrEmpty(NameOfServises.Text.Trim()))
            {
                MakeSomeHelp.MSG("Необходимо указать название услуги", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            if (string.IsNullOrEmpty(TypeOfServis.Text.Trim()))
            {
                MakeSomeHelp.MSG("Необходимо указать тип услуги", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            if (string.IsNullOrEmpty(Descriprtion.Text.Trim()))
            {
                MakeSomeHelp.MSG("Необходимо указать описание данной услуги", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            if (!decimal.TryParse(Cost.Text.Trim(), out cost) || string.IsNullOrEmpty(Cost.Text.Trim()))
            {
                MakeSomeHelp.MSG("Необходимо стоимость", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            return true;
        }
    }
}

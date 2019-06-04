using Newtonsoft.Json;
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
    /// Interaction logic for PremisesRedactUC.xaml
    /// </summary>
    public partial class PremisesRedactUC : UserControl
    {
        BaseWindow window;
        Guid idPremises;
        bool Redact = false;
        public PremisesRedactUC(ref BaseWindow baseWindow, object DataAboutPremises = null)
        {
            InitializeComponent();
            window = baseWindow;
            if (DataAboutPremises != null)
            {
                Redact = true;
                var dat = DataAboutPremises as RepairFlat.Model.MakeSubs.ListOfPremises;
                NameOfPremises.Text = dat.Name.Trim();
                Description.Text = dat.Description.Trim();
                idPremises = dat.idPremises;
                AddBtn.Content = "Редактировать";
            }
            else
            {
                idPremises = Guid.NewGuid();
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
                    query = "Insert into PremisesType (idPremises,NameOfPremises,Descriprtion) values (@idPremises,@NameOfPremises,@Descriprtion)";
                }
                else
                {
                    query = "Update PremisesType set NameOfPremises=@NameOfPremises, Descriprtion=@Descriprtion where idPremises=@idPremises;";
                }
                SQLiteParameter[] sQLiteParameter = new SQLiteParameter[4];
                sQLiteParameter[0] = new SQLiteParameter("@idPremises", idPremises.ToString());
                sQLiteParameter[1] = new SQLiteParameter("@NameOfPremises", NameOfPremises.Text.Trim());
                sQLiteParameter[2] = new SQLiteParameter("@Descriprtion", Description.Text.Trim());
                MakeWorkWirthDataBase.MakeSomeQueryWork(query, parameters: sQLiteParameter);
                MakeUpdateServer();
            }
        }

        private void MakeUpdateServer()
        {
            MakeUpdOrInsPremises makeUpdOrInsPremises = new MakeUpdOrInsPremises();
            makeUpdOrInsPremises.idUser = SaveSomeData.IdUser ?? default;
            makeUpdOrInsPremises.DateOfMake = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            ListOfPremises listOfPremises = new ListOfPremises { idPremises = idPremises, Description = Description.Text.Trim(), Name= Description.Text.Trim() };
            makeUpdOrInsPremises.ListOfPremises = new List<ListOfPremises>();
            makeUpdOrInsPremises.ListOfPremises.Add(listOfPremises);
            string Json = JsonConvert.SerializeObject(makeUpdOrInsPremises);
            string urlSend = "api/substring/premises/update";
            MakeSomeHelp.UpdloadDataToServer(urlSend, Json);
            window.Close();
        }

        bool CheckData()
        {
            if (string.IsNullOrEmpty(NameOfPremises.Text.Trim()))
            {
                MakeSomeHelp.MSG("Необходимо указать название помещения", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            if (string.IsNullOrEmpty(Description.Text.Trim()))
            {
                MakeSomeHelp.MSG("Необходимо указать описание помещения", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            return true;
        }
    }
}

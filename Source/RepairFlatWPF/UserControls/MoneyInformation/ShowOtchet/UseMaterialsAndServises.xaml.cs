using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace RepairFlatWPF.UserControls.MoneyInformation.ShowOtchet
{
    /// <summary>
    /// Interaction logic for UseMaterialsAndServises.xaml
    /// </summary>
    public partial class UseMaterialsAndServises : UserControl
    {
        BaseWindow window;
        public UseMaterialsAndServises(ref BaseWindow baseWindow)
        {
            InitializeComponent();
            this.window = baseWindow;
            MakeDataAboutSalaryWorker();
        }

        private async void MakeDataAboutSalaryWorker()
        {
            var InformFromserver = await Task.Run(() => MakeSomeHelp.MakeDownloadByLink($"api/Statistik/infabsub"));
            var DataAbSalaryFromServer = JsonConvert.DeserializeObject<Model.StatModel.DescAboutOrderSubUsed>(InformFromserver.ToString());

                DataTable ServData = new DataTable("ServInf");
                foreach (string NameOfColumn in SomeEnums.InformationAboutServStat)
                {
                    ServData.Columns.Add(NameOfColumn);
                }
                DataAboutServ.ItemsSource = ServData.DefaultView;
                DataTable MaterialData = new DataTable("MatInf");
                foreach (string NameOfColumn in SomeEnums.InformationAboutMatStat)
                {
                    MaterialData.Columns.Add(NameOfColumn);
                }
                DataAboutMaterial.ItemsSource = MaterialData.DefaultView;
                int Numb = 1;
                foreach (var InfABoutMat in DataAbSalaryFromServer.MaterialsSubInf)
                {
                    DataRow dataRow = MaterialData.NewRow();
                    dataRow[0] = Numb;
                    dataRow[1] = InfABoutMat.NameOfMaterial;
                    dataRow[2] = InfABoutMat.UnitOfMeasue;
                    dataRow[3] = InfABoutMat.cost;
                    dataRow[4] = InfABoutMat.count;
                    dataRow[5] = InfABoutMat.summa;
                    MaterialData.Rows.Add(dataRow);
                    Numb++;
                }
                Numb = 1;
                foreach (var InfABoutServ in DataAbSalaryFromServer.ServSubInf)
                {
                    DataRow dataRow = ServData.NewRow();
                    dataRow[0] = Numb;
                    dataRow[1] = InfABoutServ.NameOfServ;
                    dataRow[2] = InfABoutServ.TypeOfServ;
                    dataRow[3] = InfABoutServ.cost;
                    dataRow[4] = InfABoutServ.count;
                    dataRow[5] = InfABoutServ.summa;
                    ServData.Rows.Add(dataRow);
                    Numb++;
                }

                DataOfCreate.Content += $" {DateTime.Now.ToString("dd.MM.yyyy")}";
                Summa.Content += $" {DataAbSalaryFromServer.summaAll}";
                SummaServ.Content += $" {DataAbSalaryFromServer.summaServ}";
                SummaAbMat.Content += $" {DataAbSalaryFromServer.summaMaterials}";

        }

        private void ReturnBTN_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }

        private void GetData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {

                    printDialog.PrintVisual(ForPrint, "");
                    window.Close();
                }
            }
            finally
            {
                this.IsEnabled = true;
            }
        }
    }
}

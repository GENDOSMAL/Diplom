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
    /// Interaction logic for MakeSalaryPayment.xaml
    /// </summary>
    public partial class MakeSalaryPayment : UserControl
    {
        BaseWindow window;
        public MakeSalaryPayment(SomeEnums.TypeOfReport typeOfReport, ref BaseWindow baseWindow)
        {
            InitializeComponent();
            this.window = baseWindow;
            if (typeOfReport == SomeEnums.TypeOfReport.AboutSalary)
            {
                MakeDataAboutSalaryWorker();
            }
            else
            {
                MakeDataAboutOrderPayment();
            }           
        }

        private async void MakeDataAboutSalaryWorker()
        {
            var InformFromserver = await Task.Run(() => MakeSomeHelp.MakeDownloadByLink($"api/Statistik/salary"));
            var DataAbSalaryFromServer = JsonConvert.DeserializeObject<Model.StatModel.DataAboutWorkerPayment>(InformFromserver.ToString());
            
            if (DataAbSalaryFromServer.success)
            {
                DataTable DataAboutSalary = new DataTable("Salary");
                foreach (string NameOfColumn in SomeEnums.InformationAboutSalary)
                {
                    DataAboutSalary.Columns.Add(NameOfColumn);
                }
                DataAboutPayWorker.ItemsSource = DataAboutSalary.DefaultView;
                int Numb = 1;
                foreach(var InfABoutSalaryForWorker in DataAbSalaryFromServer.InformationAboutWorker)
                {
                    DataRow dataRow = DataAboutSalary.NewRow();
                    dataRow[0] = Numb;
                    dataRow[1] = InfABoutSalaryForWorker.FIOWorker;
                    dataRow[2] = InfABoutSalaryForWorker.NameOfPost;
                    dataRow[3] = InfABoutSalaryForWorker.SalaryOfWork;
                    dataRow[4] = InfABoutSalaryForWorker.DateOfOperation.Value.ToString("dd.MM.yyyy");
                    DataAboutSalary.Rows.Add(dataRow);
                    Numb++;
                }
                DataOfCreate.Content += $" {DateTime.Now.ToString("dd.MM.yyyy")}";
                Summa.Content += $" {DataAbSalaryFromServer.Summa}";
            }
            else
            {
                window.Close();
                MakeSomeHelp.MSG($"Произошла ошибка <{DataAbSalaryFromServer.description}>",MsgBoxImage:MessageBoxImage.Error);
            }
        }

        private async void MakeDataAboutOrderPayment()
        {
            var InformFromserver = await Task.Run(() => MakeSomeHelp.MakeDownloadByLink($"api/Statistik/payminf"));
            var DataFromServerOrderPayment = JsonConvert.DeserializeObject<Model.StatModel.ListOfDataAboOrderPayment>(InformFromserver.ToString());

            if (DataFromServerOrderPayment.success)
            {
                DataTable DataAboutPayment = new DataTable("Payemt");
                foreach (string NameOfColumn in SomeEnums.InformationAboutOrderPay)
                {
                    DataAboutPayment.Columns.Add(NameOfColumn);
                }
                DataAboutPayWorker.ItemsSource = DataAboutPayment.DefaultView;
                int Numb = 1;
                foreach (var InfAboutOrderPayment in DataFromServerOrderPayment.InformationAboutOrderPay)
                {
                    DataRow dataRow = DataAboutPayment.NewRow();
                    dataRow[0] = Numb;
                    dataRow[1] = InfAboutOrderPayment.FIOClient;
                    dataRow[2] = InfAboutOrderPayment.Summa;
                    dataRow[3] = InfAboutOrderPayment.Desc;
                    dataRow[4] = InfAboutOrderPayment.DateOfMake.ToString("dd.MM.yyyy");
                    dataRow[5] = InfAboutOrderPayment.FIOOfWorker;
                    DataAboutPayment.Rows.Add(dataRow);
                    Numb++;
                }
                DataOfCreate.Content += $" {DateTime.Now.ToString("dd.MM.yyyy")}";
                Summa.Content += $" {DataFromServerOrderPayment.Summa}";
            }
            else
            {
                window.Close();
                MakeSomeHelp.MSG($"Произошла ошибка <{DataFromServerOrderPayment.description}>", MsgBoxImage: MessageBoxImage.Error);
            }
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

        private void ReturnBTN_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }
    }
}

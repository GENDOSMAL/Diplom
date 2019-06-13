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
        public MakeSalaryPayment(ref BaseWindow baseWindow)
        {
            InitializeComponent();
            this.window = baseWindow;
            MakeDataAboutPayWorker();
        }

        private async void MakeDataAboutPayWorker()
        {
            var InformFromserver = await Task.Run(() => MakeSomeHelp.MakeDownloadByLink($"api/Statistik/salary"));
            var DataFromServerSalary = JsonConvert.DeserializeObject<Model.StatModel.DataAboutWorkerPayment>(InformFromserver.ToString());
            
            if (DataFromServerSalary.success)
            {
                DataTable DataAboutSalary = new DataTable("Salary");
                foreach (string NameOfColumn in SomeEnums.InformationAboutSalary)
                {
                    DataAboutSalary.Columns.Add(NameOfColumn);
                }
                DataAboutPayWorker.ItemsSource = DataAboutSalary.DefaultView;
                int Numb = 1;
                foreach(var InfABoutSalaryForWorker in DataFromServerSalary.InformationAboutWorker)
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
                Summa.Content += $" {DataFromServerSalary.Summa}";

            }
            else
            {
                window.Close();
                MakeSomeHelp.MSG("Не найдено информации о выплате заработной платы!",MsgBoxImage:MessageBoxImage.Error);
            }




        }

        private void GetData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(ForPrint, "Информация о выдвче заработной платы");
                }
            }
            finally
            {
                this.IsEnabled = true;
            }
        }

        private void ReturnBTN_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

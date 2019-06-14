using Newtonsoft.Json;
using RepairFlat.Model;
using RepairFlatWPF.Model;
using RepairFlatWPF.UserControls.WorkerInformation.KadrWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
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

namespace RepairFlatWPF.UserControls.MoneyInformation
{
    /// <summary>
    /// Interaction logic for MakePayForWorker.xaml
    /// </summary>
    public partial class MakePayForWorker : UserControl
    {
        Guid idUser;
        BaseWindow window;
        string WorkerFIO;
        public MakePayForWorker( ref BaseWindow baseWindow)
        {
            InitializeComponent();
            window = baseWindow;
        }

        private void SelectWorker_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow("Выбор кандидата");
            baseWindow.MakeOpen(new ShowAllWorkers(ref baseWindow, SomeEnums.TypeOfUserNeed.forpayment));
            baseWindow.ShowDialog();
            if (SaveSomeData.MakeSomeOperation)
            {
                SaveSomeData.MakeSomeOperation = false;
                var rows = SaveSomeData.SomeObject as DataRow;
                SaveSomeData.SomeObject = null;
                idUser = SaveSomeData.idSubs;
                SaveSomeData.idSubs = new Guid();
                Salary.Text = rows[7].ToString();
                WorkerFIO = $"{rows[1]?.ToString().Trim()} {rows[2]?.ToString().Trim().Substring(0, 1)}.{rows[3]?.ToString().Trim().Substring(0, 1)} ";
                WorkerName.Text = $"{rows[1]?.ToString().Trim()} {rows[2]?.ToString().Trim().Substring(0, 1)}.{rows[3]?.ToString().Trim().Substring(0, 1)} : {rows[5]}";
            }
        }

        private async void MakeOperation_Click(object sender, RoutedEventArgs e)
        {
            if (idUser == new Guid())
            {
                MakeSomeHelp.MSG("Выбрерите работника для оплаты!");
            }
            else
            {
                PayWagesM payWagesM = new PayWagesM
                {
                    Data = DateTime.Now,
                    idAdressat = idUser,
                    idGive = Guid.NewGuid(),
                    idMakeWorker = SaveSomeData.IdUser ?? default,
                    SizeOfData = Convert.ToDecimal(Salary.Text),

                };
                string urlSend = "api/worker/giveworker";
                MakeOperation.Content = "Ожидайте...";
                Return.Content = "Ожидайте...";
                Return.IsEnabled = false;
                MakeOperation.IsEnabled = false;
                string Json = JsonConvert.SerializeObject(payWagesM);
                var task = await Task.Run(() => BaseWorkWithServer.CatchErrorWithPost(urlSend, "POST", Json, nameof(BaseWorkWithServer), nameof(MakeOperation_Click)));
                var deserializedProduct = JsonConvert.DeserializeObject<BaseResult>(task.ToString());

                if (!deserializedProduct.success)
                {
                    MakeSomeHelp.MSG($"Произошла ошикбка  {deserializedProduct.description}", MsgBoxImage: MessageBoxImage.Error);
                }
                else
                {
                    if(MakeSomeHelp.MSG("Данные добавлены. Напечатать информацию?", MsgBoxImage: MessageBoxImage.Question, MsgBoxButton: MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    {
                        var Director = System.IO.Path.Combine(Environment.CurrentDirectory, "Temp");
                        if (!Directory.Exists(Director))
                        {
                            Directory.CreateDirectory(Director);
                        }
                        string NameOfFile = System.IO.Path.Combine(Director, "payworker.dotx");
                        if (!File.Exists(NameOfFile))
                            File.WriteAllBytes(NameOfFile, RepairFlatWPF.Properties.Resources.PayForWorker);
                        using (var application = new NetOffice.WordApi.Application { Visible = true })
                        {
                            using (var document = application.Documents.Add(NameOfFile))
                            {
                                var DateMake = document.Bookmarks["DateMake"].Range;
                                DateMake.Text = $" {DateTime.Now.ToString("dd.MM.yyyy")}";
                                var FIOMake = document.Bookmarks["FIOMake"].Range;
                                FIOMake.Text = $" {SaveSomeData.LastNameAndIni}";
                                var FIOMake1 = document.Bookmarks["FIOMake1"].Range;
                                FIOMake1.Text = $"{SaveSomeData.LastNameAndIni}";
                                var FIORab = document.Bookmarks["FIORab"].Range;
                                FIORab.Text = $"{WorkerFIO}";
                                var FIORab1 = document.Bookmarks["FIORab1"].Range;
                                FIORab1.Text = $"{WorkerFIO}";
                                var Mounth = document.Bookmarks["Mounth"].Range;
                                Mounth.Text = $" {DateTimeExtensions.ToMonthName(DateTime.Now)}";
                                var Salary = document.Bookmarks["Salary"].Range;
                                Salary.Text = $" {Salary.Text?.Trim()}";
                            }
                            application.Activate();
                        }
                    }
                }

                window.Close();
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }

        public class PayWagesM
        {
            public Guid idGive;
            public Guid? idAdressat;
            public Guid idMakeWorker;
            public decimal SizeOfData;
            public DateTime Data;
            public string Descriptiom;
        }
        
    }
    static class DateTimeExtensions
    {
        public static string ToMonthName(this DateTime dateTime)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateTime.Month);
        }


    }
}

using Newtonsoft.Json;
using RepairFlatWPF.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static RepairFlatWPF.Model.DescMakePayment;

namespace RepairFlatWPF.UserControls.OrderWork
{
    public partial class InformationAboutOrderPay : UserControl
    {
        #region Переменные
        DataTable DataAboutPayment;
        Guid idOrder;
        List<Tuple<int, Guid>> ListOfIdPayment;
        TextBlock NeedPay;
        decimal nepay;
        string FiOUser, AdressOfClient;
        #endregion

        #region Контструктор
        public InformationAboutOrderPay(Guid idOrder, ref TextBlock NeedPay, string FiOUser, string AdressOfClient)
        {
            InitializeComponent();
            this.idOrder = idOrder;
            this.NeedPay = NeedPay;
            this.FiOUser = FiOUser;
            this.AdressOfClient = AdressOfClient;

            MakeDefaultDataAboutDT();
        }
        #endregion

        #region Обработчики событий
        private void AddPayment_Click(object sender, RoutedEventArgs e)
        {
            nepay = Convert.ToDecimal(NeedPay.Text);
            if (nepay != 0)
            {
                BaseWindow baseWindow = new BaseWindow("Добавление данных о платежах");
                baseWindow.MakeOpen(new AddInfromationUserControl.AddPaymentInformation(idOrder, ref NeedPay, ref baseWindow));
                baseWindow.ShowDialog();
                if (SaveSomeData.MakeSomeOperation)
                {
                    SaveSomeData.MakeSomeOperation = false;
                    MakeDefaultDataAboutDT();
                }
            }
            else
            {
                MakeSomeHelp.MSG("Все денежные средства были выплачены!", MsgBoxImage: MessageBoxImage.Hand);
            }
        }

        private async void EditPayment_Click(object sender, RoutedEventArgs e)
        {
            int index = DataGrid.SelectedIndex;
            if (index != -1)
            {
                var indexOfSelectedRows = MakeSomeHelp.SelectedRowsInDataGrid(ref DataGrid, index);
                int numberOfRows = 0;
                if (int.TryParse(indexOfSelectedRows.ToString(), out numberOfRows))
                {
                    Guid idPayment = ListOfIdPayment.Where(e2 => e2.Item1 == numberOfRows).Select(e1 => e1.Item2).First();
                    var DataDle = await Task.Run(() => MakeSomeHelp.MakeDownloadByLink($"api/payment/getpay?idPayment={idPayment}"));
                    var DataAbInf = JsonConvert.DeserializeObject<MakeDataAboutPayment>(DataDle.ToString());
                    BaseWindow baseWindow = new BaseWindow("Редактирование данных об оплате");
                    baseWindow.MakeOpen(new AddInfromationUserControl.AddPaymentInformation(idOrder, ref NeedPay, ref baseWindow, DataAbInf));
                    baseWindow.ShowDialog();
                    if (SaveSomeData.MakeSomeOperation)
                    {
                        SaveSomeData.MakeSomeOperation = false;
                        MakeDefaultDataAboutDT();
                    }
                }
            }
            else
            {
                MakeSomeHelp.MSG("Не выбрана оплата для редактирование!", MsgBoxImage: MessageBoxImage.Hand);
            }
        }

        private async void PrintDoc_Click(object sender, RoutedEventArgs e)
        {
            int index = DataGrid.SelectedIndex;
            if (index != -1)
            {
                var indexOfSelectedRows = MakeSomeHelp.SelectedRowsInDataGrid(ref DataGrid, index);
                int numberOfRows = 0;
                if (int.TryParse(indexOfSelectedRows.ToString(), out numberOfRows))
                {
                    Guid idPayment = ListOfIdPayment.Where(e2 => e2.Item1 == numberOfRows).Select(e1 => e1.Item2).First();
                    DataRow row = default;
                    for (int i = 0; i < DataAboutPayment.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(DataAboutPayment.Rows[i][0].ToString()) == numberOfRows)
                        {
                            row = DataAboutPayment.Rows[i];
                        }
                    }
                    var Director = System.IO.Path.Combine(Path.GetDirectoryName(Path.GetTempPath()), "Repflat", "Temp");
                    if (!Directory.Exists(Director))
                    {
                        Directory.CreateDirectory(Director);
                    }
                    string NameOfFile = System.IO.Path.Combine(Director, "oplata.dotx");
                    if (!File.Exists(NameOfFile))
                        File.WriteAllBytes(NameOfFile, RepairFlatWPF.Properties.Resources.OplataDoc);
                    var DataDle = await Task.Run(() => MakeSomeHelp.MakeDownloadByLink($"api/payment/getdata"));
                    var DataAbInf = JsonConvert.DeserializeObject<DataAboutPayment>(DataDle.ToString());


                    using (var application = new NetOffice.WordApi.Application { Visible = true })
                    {
                        using (var document = application.Documents.Add(NameOfFile))
                        {
                            string Name = "Плата за оказание услуг";
                            string text = $"Получатель платежа: {DataAbInf.NameOfRecipient?.Trim()} {Environment.NewLine}ИНН: {DataAbInf.InnOfOrganization?.Trim()} {Environment.NewLine}КПП: {DataAbInf.KppOfOrganization?.Trim()} {Environment.NewLine}Банк получатель: {DataAbInf.BankOfPayment?.Trim()} {Environment.NewLine}Расчетный счет: {DataAbInf.CheckingAcount?.Trim()} {Environment.NewLine}БИК: {DataAbInf.BIK?.Trim()}  {Environment.NewLine}УИН: {DataAbInf.YIN?.Trim()}";
                            var InfrormationForPayment = document.Bookmarks["InfrormationForPayment"].Range;
                            InfrormationForPayment.Text = text;
                            var InfrormationForPayment1 = document.Bookmarks["InfrormationForPayment1"].Range;
                            InfrormationForPayment1.Text = text;
                            var AdressOfClient = document.Bookmarks["AdressOfClient"].Range;
                            AdressOfClient.Text = this.AdressOfClient.ToString();
                            var AdressOfClient1 = document.Bookmarks["AdressOfClient1"].Range;
                            AdressOfClient1.Text = this.AdressOfClient.ToString();
                            var NameOfClient = document.Bookmarks["NameOfClient"].Range;
                            NameOfClient.Text = FiOUser.ToString();
                            var NameOfClient1 = document.Bookmarks["NameOfClient1"].Range;
                            NameOfClient1.Text = FiOUser.ToString();
                            var NameOfPayment = document.Bookmarks["NameOfPayment"].Range;
                            NameOfPayment.Text = Name.ToString();
                            var NameOfPayment1 = document.Bookmarks["NameOfPayment1"].Range;
                            NameOfPayment1.Text = Name.ToString();
                            var Summa = document.Bookmarks["Summa"].Range;
                            Summa.Text = row[2].ToString();
                            var Summa1 = document.Bookmarks["Summa1"].Range;
                            Summa1.Text = row[2].ToString();
                        }
                        application.Activate();
                    }
                }
            }
            else
            {
                MakeSomeHelp.MSG("Выберите информацию для печати квитанции!", MsgBoxImage: MessageBoxImage.Hand);
            }
        }
        #endregion

        #region Прочие методы
        private void MakeDefaultDataAboutDT()
        {
            DataAboutPayment = new DataTable();
            ListOfIdPayment = new List<Tuple<int, Guid>>();
            foreach (string asd in SomeEnums.PayInf)
            {
                DataAboutPayment.Columns.Add(asd);
            }
            DataGrid.ItemsSource = DataAboutPayment.DefaultView;
            MakeDataAboutPayment();
        }

        async private void MakeDataAboutPayment()
        {
            var InformFromserver = await Task.Run(() => MakeSomeHelp.MakeDownloadByLink($"api/order/get/payment?idOrder={idOrder}"));
            var ListOfPayment = JsonConvert.DeserializeObject<Model.OrderDesc.DataAboutPaymentInOrder>(InformFromserver.ToString());
            MakeDataTable(ListOfPayment);
        }
        private void MakeDataTable(Model.OrderDesc.DataAboutPaymentInOrder paymentInOrder)
        {
            if (paymentInOrder.success)
            {
                NeedPay.Text = paymentInOrder.NeedPay.ToString();
                int number = 1;
                foreach (var Pay in paymentInOrder.InfPayment)
                {
                    DataRow dataRow = DataAboutPayment.NewRow();
                    dataRow[0] = number;
                    dataRow[1] = Pay.DateOfMake.Value.ToString("dd.MM.yyy");
                    dataRow[2] = Pay.Summa?.ToString();
                    dataRow[3] = Pay.FioMake?.Trim();
                    dataRow[4] = Pay.Description?.Trim();
                    DataAboutPayment.Rows.Add(dataRow);
                    ListOfIdPayment.Add(new Tuple<int, Guid>(number, Pay.idPayment));
                    number++;
                }

            }
        }        
        #endregion
    }
}

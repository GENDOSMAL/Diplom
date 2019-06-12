using Newtonsoft.Json;
using RepairFlatWPF.Model;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using static RepairFlatWPF.Model.DescMakePayment;

namespace RepairFlatWPF.UserControls.MoneyInformation
{
    /// <summary>
    /// Interaction logic for ShowDataForPayment.xaml
    /// </summary>
    public partial class ShowDataForPayment : UserControl
    {
        #region Переменные
        DataAboutPayment InfAboutPayment = new DataAboutPayment();
        #endregion

        #region Конструктор
        public ShowDataForPayment()
        {
            InitializeComponent();
            MakeDataFromPayment();
        }
        #endregion

        #region Обработчики событий
        private void ExtionPayment_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow("Работа с данными для оплаты");
            baseWindow.MakeOpen(new MakeDataForPayment(ref baseWindow, InfAboutPayment));
            baseWindow.ShowDialog();
            if (SaveSomeData.MakeSomeOperation)
            {
                SaveSomeData.MakeSomeOperation = false;
                MakeDataFromPayment();
            }
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            if (MakeSomeHelp.MSG("Вы дейсвительно хотите создать шаблон квитанции для оплаты ", MsgBoxImage: MessageBoxImage.Question, MsgBoxButton: MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                var Director = System.IO.Path.Combine(Environment.CurrentDirectory, "Temp");
                if (!Directory.Exists(Director))
                {
                    Directory.CreateDirectory(Director);
                }

                string NameOfFile = System.IO.Path.Combine(Director, "oplata.dotx");
                if (!File.Exists(NameOfFile))
                    File.WriteAllBytes(NameOfFile, RepairFlatWPF.Properties.Resources.OplataDoc);

                using (var application = new NetOffice.WordApi.Application { Visible = true })
                {
                    using (var document = application.Documents.Add(NameOfFile))
                    {

                        string text = $"Получатель платежа: {InfAboutPayment.NameOfRecipient?.Trim()} {Environment.NewLine}ИНН: {InfAboutPayment.InnOfOrganization?.Trim()} {Environment.NewLine}КПП: {InfAboutPayment.KppOfOrganization?.Trim()} {Environment.NewLine}Банк получатель: {InfAboutPayment.BankOfPayment?.Trim()} {Environment.NewLine}Расчетный счет: {InfAboutPayment.CheckingAcount?.Trim()} {Environment.NewLine}БИК: {InfAboutPayment.BIK?.Trim()}  {Environment.NewLine}УИН: {InfAboutPayment.YIN?.Trim()}";
                        var InfrormationForPayment = document.Bookmarks["InfrormationForPayment"].Range;
                        InfrormationForPayment.Text = text;
                        var InfrormationForPayment1 = document.Bookmarks["InfrormationForPayment1"].Range;
                        InfrormationForPayment1.Text = text;
                    }
                    application.Activate();

                }

            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.DataGridMakeWork(new FinansInformation());
        }
        #endregion

        #region Прочие обработчики

        private async void MakeDataFromPayment()
        {

            var DataDle = await Task.Run(() => MakeSomeHelp.MakeDownloadByLink($"api/payment/getdata"));
            var DataAbInf = JsonConvert.DeserializeObject<DataAboutPayment>(DataDle.ToString());

            if (DataAbInf.success)
            {
                InfAboutPayment = DataAbInf;
                ExtionPayment.Content = "Редактировать";
                TextRange doc = new TextRange(IformationAb.Document.ContentStart, IformationAb.Document.ContentEnd);
                doc.Text = $"Текущие данные:{Environment.NewLine}";
                doc.Text += $"Были созданы: <{DataAbInf.NameOfWorkerMake?.Trim()}> {Environment.NewLine}";
                doc.Text += $"Наименование получателя: <{DataAbInf.NameOfRecipient?.Trim()}> {Environment.NewLine}";
                doc.Text += $"ИНН организации: <{DataAbInf.InnOfOrganization?.Trim()}> {Environment.NewLine}";
                doc.Text += $"КПП организации: <{DataAbInf.KppOfOrganization?.Trim()}> {Environment.NewLine}";
                doc.Text += $"Банк получатель: <{DataAbInf.BankOfPayment?.Trim()}> {Environment.NewLine}";
                doc.Text += $"Расчетный счет: <{DataAbInf.CheckingAcount?.Trim()}> {Environment.NewLine}";
                doc.Text += $"БИК: <{DataAbInf.BIK}> {Environment.NewLine}";
                doc.Text += $"УИН: <{DataAbInf.YIN}> {Environment.NewLine}";
                doc.Text += $"Дата создания/последнего обновления: <{DataAbInf.DateOfMake.Value.ToString("dd.MM.yyyy")}> {Environment.NewLine}";
            }
            else
            {
                InfAboutPayment = null;
                ExtionPayment.Content = "Указать";
                TextRange doc = new TextRange(IformationAb.Document.ContentStart, IformationAb.Document.ContentEnd);
                doc.Text = "Необходимо указать данные для оплаты клиентам";
            }
        }

        #endregion


    }
}

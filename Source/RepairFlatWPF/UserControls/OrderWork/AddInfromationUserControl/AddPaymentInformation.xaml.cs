using Newtonsoft.Json;
using RepairFlat.Model;
using RepairFlatWPF.Model;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static RepairFlatWPF.Model.DescMakePayment;

namespace RepairFlatWPF.UserControls.OrderWork.AddInfromationUserControl
{
    /// <summary>
    /// Interaction logic for AddPaymentInformation.xaml
    /// </summary>
    public partial class AddPaymentInformation : UserControl
    {
        #region Переменные

        Guid idOrder;
        Guid idPayment;
        bool NewData = true;
        decimal SummaPay, needPayde;
        BaseWindow window;
        MakeDataAboutPayment makeData = new MakeDataAboutPayment();
        TextBlock needPay;
        #endregion

        #region Обработки
        public AddPaymentInformation(Guid idOrder, ref TextBlock needPay, ref BaseWindow baseWindow, object DataAboutPayment = null)
        {
            InitializeComponent();
            this.needPay = needPay;
            decimal.TryParse(needPay.Text, out needPayde);
            NeedSoPay.Text += needPay.Text;
            window = baseWindow;
            this.idOrder = idOrder;
            if (DataAboutPayment != null)
            {
                NewData = false;
                makeData = DataAboutPayment as MakeDataAboutPayment;
                idPayment = makeData.idPayment;
                Summa.Text = makeData.summa?.ToString();
                Desc.Text = makeData.Desc?.Trim();
                Summa.IsEnabled = false;
                AddPayment.Content = "Редактировать";
            }
            else
            {
                idPayment = Guid.NewGuid();
            }
        }

        private async void AddPayment_Click(object sender, RoutedEventArgs e)
        {
            if (CheckFields())
            {
                var DataDle = await Task.Run(() => MakeSomeHelp.MakeDownloadByLink($"api/payment/getdata"));
                var DataAbInf = JsonConvert.DeserializeObject<DataAboutPayment>(DataDle.ToString());

                MakeDataAboutPayment Result = new MakeDataAboutPayment();
                if (NewData)
                {
                    Result.summa = SummaPay;
                    Result.idWorkerMake = SaveSomeData.IdUser;
                    Result.idPayment = idPayment;
                    Result.idOrder = idOrder;
                    Result.idInfForPayment = DataAbInf.idInfPayment;
                    Result.Desc = Desc.Text?.Trim();
                    Result.DateOfDoc = DateTime.Now;
                }
                else
                {
                    Result.summa = SummaPay;
                    Result.idWorkerMake = makeData.idWorkerMake;
                    Result.idPayment = makeData.idPayment;
                    Result.idOrder = makeData.idOrder;
                    Result.idInfForPayment = makeData.idInfForPayment;
                    Result.Desc = Desc.Text?.Trim();
                    Result.DateOfDoc = makeData.DateOfDoc;
                }
                string Json = JsonConvert.SerializeObject(Result);
                var task = await Task.Run(() => BaseWorkWithServer.CatchErrorWithPost("api/payment/create/payment", "POST", Json, nameof(BaseWorkWithServer), nameof(AddPayment_Click)));
                var deserializedProduct = JsonConvert.DeserializeObject<BaseResult>(task.ToString());

                if (!deserializedProduct.success)
                {
                    MakeSomeHelp.MSG($"Произошла ошикбка при работе {deserializedProduct.description}", MsgBoxImage: MessageBoxImage.Error);
                }
                else
                {
                    Model.SaveSomeData.MakeSomeOperation = true;
                    MakeSomeHelp.MSG("Операции над данными совершены!", MsgBoxImage: MessageBoxImage.Information);
                }
                window.Close();
            }
        }



        private void Return_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }

        #endregion

        #region Дополнительные методы
        private bool CheckFields()
        {//TODO Глобально над этим подумать
            bool result = true;
            if (!decimal.TryParse(Summa.Text.Trim(), out SummaPay))
            {
                MakeSomeHelp.MSG("Необходимо указать сумму оплаты!", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            else if (SummaPay > needPayde)
            {
                MakeSomeHelp.MSG($"Сумма оплаты больше, чем остаток {needPay.Text}", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            return result;
        }
        #endregion
    }
}

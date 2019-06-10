using Newtonsoft.Json;
using RepairFlat.Model;
using RepairFlatWPF.Model;
using System;
using System.Collections.Generic;
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
using static RepairFlatWPF.Model.DescMakePayment;

namespace RepairFlatWPF.UserControls.MoneyInformation
{
    /// <summary>
    /// Interaction logic for MakeDataForPayment.xaml
    /// </summary>
    public partial class MakeDataForPayment : UserControl
    {
        #region Переменные
        BaseWindow window;
        Guid idInf;
        #endregion
        
        #region Конструктор
        public MakeDataForPayment(ref BaseWindow baseWindow, DataAboutPayment dataAbout=null)
        {
            InitializeComponent();
            this.window = baseWindow;
            if (dataAbout != null)
            {
                this.idInf = dataAbout.idInfPayment;
                NameOfRecepient.Text = dataAbout.NameOfRecipient;
                INN.Text = dataAbout.InnOfOrganization;
                KPP.Text = dataAbout.KppOfOrganization;
                Bank.Text = dataAbout.BankOfPayment;
                CheckingAcount.Text = dataAbout.CheckingAcount;
                BIK.Text = dataAbout.BIK;
                YIN.Text = dataAbout.YIN;                
            }
            else
            {
                idInf = Guid.NewGuid();
            }
            
        }

        #endregion

        #region Обработчики событий

        private async void SaveBTN_Click(object sender, RoutedEventArgs e)
        {
            if (MakeCheckData())
            {
                DataAboutPayment dataAboutPayment = new DataAboutPayment
                {
                    DateOfMake = DateTime.Now,
                    InnOfOrganization = INN.Text.Trim(),
                    BankOfPayment = Bank.Text.Trim(),
                    BIK = BIK.Text.Trim(),
                    KppOfOrganization = KPP.Text.Trim(),
                    CheckingAcount = CheckingAcount.Text.Trim(),
                    idInfPayment = idInf,
                    idWorkerMake = SaveSomeData.IdUser,
                    YIN = YIN.Text.Trim(),
                    NameOfRecipient = NameOfRecepient.Text.Trim(),
                };

                string Json = JsonConvert.SerializeObject(dataAboutPayment);
                var task = await Task.Run(() => BaseWorkWithServer.CatchErrorWithPost("api/payment/create/inf", "POST", Json, nameof(BaseWorkWithServer), nameof(SaveBTN_Click)));
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

        private void RetutnBTN_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }
        #endregion

        #region Прочие обработчики
        private bool MakeCheckData()
        {
            if (string.IsNullOrEmpty(NameOfRecepient.Text.Trim()))
            {
                MakeSomeHelp.MSG("Необходимо указаить наименование получателя", MsgBoxImage:MessageBoxImage.Hand);
                return false;
            }
            if(!IsDigitsOnly(INN.Text.Trim()))
            {
                MakeSomeHelp.MSG("ИНН может содержать только цифры", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            else
            {
                int le = INN.Text.Length;
                if (INN.Text.Length >= 12 && INN.Text.Length <=10)
                {
                    MakeSomeHelp.MSG("Длина ИНН должна быть больше или равна 10 и не больше 12", MsgBoxImage: MessageBoxImage.Hand);
                    return false;
                }
            }

            if (!IsDigitsOnly(YIN.Text.Trim()))
            {
                MakeSomeHelp.MSG("УИН может содержать только цифры", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            else
            {
                if (YIN.Text.Length <= 12 && YIN.Text.Length >= 20)
                {
                    MakeSomeHelp.MSG("Длина УИН должна быть больше или равна 12, но не превышать 20", MsgBoxImage: MessageBoxImage.Hand);
                    return false;
                }
            }

            if (!IsDigitsOnly(KPP.Text.Trim()))
            {
                MakeSomeHelp.MSG("КПП может содержать только цифры", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            else
            {
                if (KPP.Text.Length != 10)
                {
                    MakeSomeHelp.MSG("Длина КПП должна быть равна 10", MsgBoxImage: MessageBoxImage.Hand);
                    return false;
                }
            }
            if (string.IsNullOrEmpty(Bank.Text.Trim()))
            {
                MakeSomeHelp.MSG("Информация о банке получателе не может быть пустой", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }

            if (!IsDigitsOnly(CheckingAcount.Text.Trim()))
            {
                MakeSomeHelp.MSG("Расчетный счет может содержать только цифры", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            else
            {
                if (CheckingAcount.Text.Length != 20)
                {
                    MakeSomeHelp.MSG("Длина расчетного счета  должна быть равна 20", MsgBoxImage: MessageBoxImage.Hand);
                    return false;
                }
            }
            if (!IsDigitsOnly(BIK.Text.Trim()))
            {
                MakeSomeHelp.MSG("БИК может содержать только цифры", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            else
            {
                if (BIK.Text.Length != 9)
                {
                    MakeSomeHelp.MSG("Длина БИК должна быть равна 9", MsgBoxImage: MessageBoxImage.Hand);
                    return false;
                }
            }

            return true;
        }

        static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
        #endregion
    }
}

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
        bool NewOrder = true;
        double SummaPay;
        BaseWindow window;
        #endregion

        #region Обработки
        public AddPaymentInformation(Guid idOrder,ref BaseWindow baseWindow, int number=0)
        {
            InitializeComponent();
            window = baseWindow;
            this.idOrder = idOrder;
            if (number ==0)
            {
                string query = $"select * from InformationAboutPyment where Number={number}";
                NewOrder = false;
            }
            else
            {
               
            }
            
            //if (InfromationAboutPayment != null)
            //{
            //    NewOrder = false;
            //    AddPayment.Content = "Редактировать";
            //}
            //else
            //{
            //    idPayment = Guid.NewGuid();
            //}
        }

        private void AddPayment_Click(object sender, RoutedEventArgs e)
        {
            if (CheckFields())
            {
                if (NewOrder)
                {
                    //Если новый заказ
                }
                else
                {
                    //Если обновление
                }
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
            if (string.IsNullOrEmpty(NumberOfDoc.Text.Trim()))
            {
                MakeSomeHelp.MSG("Необходимо указать номер документа об оплате!", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            if (!DatePayment.SelectedDate.HasValue)
            {
                MakeSomeHelp.MSG("Необходимо указать дату оплаты!", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            if(!double.TryParse(Summa.Text.Trim(),out SummaPay))
            {
                MakeSomeHelp.MSG("Необходимо указать сумму оплаты!", MsgBoxImage: MessageBoxImage.Error);
                return false;
            }
            return result;
        }
        #endregion
    }
}

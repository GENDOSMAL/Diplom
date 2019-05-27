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

namespace RepairFlatWPF.UserControls.OrderWork
{
    /// <summary>
    /// Interaction logic for InformationAboutOrderPay.xaml
    /// </summary>
    public partial class InformationAboutOrderPay : UserControl
    {
        Guid idOrder;
        public InformationAboutOrderPay(Guid idOrder)
        {
            InitializeComponent();
            this.idOrder = idOrder;
        }

        private void AddPayment_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow("Добавление данных о платежах");
            baseWindow.MakeOpen(new AddInfromationUserControl.AddPaymentInformation(idOrder,ref baseWindow));
            baseWindow.ShowDialog();

        }

        private void EditPayment_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow("Редактирование данных о платежах");
            baseWindow.MakeOpen(new AddInfromationUserControl.AddPaymentInformation(idOrder, ref baseWindow,idOrder));
            baseWindow.ShowDialog();
        }

        private void DeletePayment_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.MSG("Не реализовано");
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.MSG("Не реализовано");
        }
    }
}

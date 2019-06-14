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

namespace RepairFlatWPF.UserControls.MoneyInformation
{
    /// <summary>
    /// Interaction logic for InfromationAboutPayAllClient.xaml
    /// </summary>
    public partial class FinansInformation : UserControl
    {
        public FinansInformation()
        {
            InitializeComponent();
        }

        private void GiveWorkerPayment_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow("Выдача денег работникам");
            baseWindow.MakeOpen(new MakePayForWorker(ref baseWindow));
            baseWindow.ShowDialog();
        }

        private void SetDataForPayment_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.DataGridMakeWork(new ShowDataForPayment());
        }
    }
}

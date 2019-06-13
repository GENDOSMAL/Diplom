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

namespace RepairFlatWPF.UserControls.MoneyInformation.ShowOtchet
{
    public partial class StatistikMenu : UserControl
    {
        public StatistikMenu()
        {
            InitializeComponent();
        }

        private void StatistiAboutPaymentWorkerSalary_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow("Инфомация о выплатах сотрудникам.");
            baseWindow.MakeOpen(new MakeSalaryPayment(ref baseWindow));
            baseWindow.ShowDialog();
        }

        private void ShowDataAboutPymentOrderInf_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StatistikAboutSubInf_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow("Тип данных для отчета.");
            baseWindow.MakeOpen(new SelectSubDataForOtchet(ref baseWindow));
            baseWindow.ShowDialog();
        }

        private void ReturnBTN_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.DataGridMakeWork(new FinansInformation());
        }
    }
}

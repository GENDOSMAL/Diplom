using RepairFlatWPF.UserControls.KadrWork;
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

namespace RepairFlatWPF.UserControls.WorkerInformation.KadrWork
{
    /// <summary>
    /// Interaction logic for TypeOfPeremeMenu.xaml
    /// </summary>
    public partial class TypeOfPeremeMenu : UserControl
    {
        public TypeOfPeremeMenu()
        {
            InitializeComponent();
        }

        private void MakeNewWorker_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow window = new BaseWindow("Принятие нового сотрудника");
            window.MakeOpen(new UserControls.WorkerInformation.KadrWork.SetWorkerDolzn(ref window));
            window.ShowDialog();
        }

        private void MakePeremechenie_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow window = new BaseWindow("Кадровые операции над сотрудниками");
            window.MakeOpen(new UserControls.WorkerInformation.KadrWork.SetWorkerDolzn(ref window,false));
            window.ShowDialog();
        }

        private void YVolnenie_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ReturnBTN_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.DataGridMakeWork(new MenuKadrWork());
        }

        private void MakeLoginAndPassword_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow window = new BaseWindow("Указания информации для авторизации");
            window.MakeOpen(new MakeLoginAndPassword(ref window));
            window.ShowDialog();
        }
    }
}

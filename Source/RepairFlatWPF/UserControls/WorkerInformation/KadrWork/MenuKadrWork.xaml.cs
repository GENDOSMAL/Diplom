using RepairFlatWPF.UserControls.WorkerInformation.KadrWork;
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

namespace RepairFlatWPF.UserControls.KadrWork
{
    /// <summary>
    /// Interaction logic for MenuKadrWork.xaml
    /// </summary>
    public partial class MenuKadrWork : UserControl
    {
        public MenuKadrWork()
        {
            InitializeComponent();
        }

        private void KadrPermises_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void WorkWithPost_Click(object sender, RoutedEventArgs e)
        {

        }

        private void WorkWithWorker_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.DataGridMakeWork(new ShowAllWorkers());
            
        }
    }
}

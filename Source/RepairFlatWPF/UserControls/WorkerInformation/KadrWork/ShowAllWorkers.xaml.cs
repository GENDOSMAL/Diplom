using RepairFlatWPF.UserControls.KadrWork;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for ShowAllWorkers.xaml
    /// </summary>
    public partial class ShowAllWorkers : UserControl
    {
        DataTable DataAboutWorker;
        List<Tuple<int, Guid>> DataAboutGuid;

        public ShowAllWorkers(bool SelectWorkerForTask=false)
        {
            InitializeComponent();
            MakeWorkBetter();
            if (SelectWorkerForTask)
            {
                ForWindow.Visibility = Visibility.Visible;
            }
            else
            {
                ForUserControl.Visibility = Visibility.Visible;
            }
        }
        private void MakeWorkBetter()
        {
            DataAboutWorker = new DataTable("WorkerInf");
            DataAboutGuid = new List<Tuple<int, Guid>>();
            DataGrid.ItemsSource = DataAboutWorker.DefaultView;
            foreach (string ColumnName in SomeEnums.WorkerTables)
            {
                DataAboutWorker.Columns.Add(ColumnName);
            }
        }



        private void AddWorker_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow("Создание нового кандидата");
            baseWindow.MakeOpen(new CreateNewWorker(ref baseWindow));
            baseWindow.ShowDialog();
        }

        private void EditWorker_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SelectWorker_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ReturnBTN_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.DataGridMakeWork(new MenuKadrWork());
        }

    }
}

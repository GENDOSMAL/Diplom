using RepairFlatWPF.UserControls.WorkerInformation.KadrWork;
using System.Windows;
using System.Windows.Controls;

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
            MakeSomeHelp.DataGridMakeWork(new TypeOfPeremeMenu());
        }

        private void WorkWithPost_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.DataGridMakeWork(new UserControls.SettingsAndSubsInf.RedactSomeSubs(SomeEnums.TypeOfSubs.Post, true));
        }

        private void WorkWithWorker_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow("");
            MakeSomeHelp.DataGridMakeWork(new ShowAllWorkers(ref baseWindow));
        }
    }
}

using System.Windows;
using System.Windows.Controls;

namespace RepairFlatWPF.UserControls.SettingsAndSubsInf
{
    /// <summary>
    /// Interaction logic for WorkWithSubInfromation.xaml
    /// </summary>
    public partial class WorkWithSubInfromation : UserControl
    {
        public WorkWithSubInfromation()
        {
            InitializeComponent();
        }

        private void ShowStatistik_Click(object sender, RoutedEventArgs e)
        {

        }


        private void LoadAllDataFromExcel_Click(object sender, RoutedEventArgs e)
        {

            MakeSomeHelp.DataGridMakeWork(new UserControls.SettingsAndSubsInf.SelectTypeForUpdateAndFile());
        }

        private void WorkWithData_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.DataGridMakeWork(new UserControls.SettingsAndSubsInf.SelectDataForRedactUC());
        }
    }
}

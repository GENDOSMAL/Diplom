using Microsoft.Win32;
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

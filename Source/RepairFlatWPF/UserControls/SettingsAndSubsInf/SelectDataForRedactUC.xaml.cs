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

namespace RepairFlatWPF.UserControls.SettingsAndSubsInf
{
    /// <summary>
    /// Interaction logic for SelectDataForRedactUC.xaml
    /// </summary>
    public partial class SelectDataForRedactUC : UserControl
    {
        public SelectDataForRedactUC()
        {
            InitializeComponent();
        }

        private void WorkWithContact_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.DataGridMakeWork(new UserControls.SettingsAndSubsInf.RedactSomeSubs(SomeEnums.TypeOfSubs.Contact));
        }

        private void WorkWithMaterials_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.DataGridMakeWork(new UserControls.SettingsAndSubsInf.RedactSomeSubs(SomeEnums.TypeOfSubs.Materials));
        }

        private void WorkWithServises_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.DataGridMakeWork(new UserControls.SettingsAndSubsInf.RedactSomeSubs(SomeEnums.TypeOfSubs.Servises));
        }
        private void WorkWithPremises_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.DataGridMakeWork(new UserControls.SettingsAndSubsInf.RedactSomeSubs(SomeEnums.TypeOfSubs.Premises));
        }

        private void WorkWithPost_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.DataGridMakeWork(new UserControls.SettingsAndSubsInf.RedactSomeSubs(SomeEnums.TypeOfSubs.Post));
        }
        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.DataGridMakeWork(new UserControls.SettingsAndSubsInf.WorkWithSubInfromation());
        }
    }
}

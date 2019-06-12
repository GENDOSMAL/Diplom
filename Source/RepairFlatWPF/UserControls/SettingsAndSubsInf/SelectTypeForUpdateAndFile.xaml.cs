using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace RepairFlatWPF.UserControls.SettingsAndSubsInf
{
    /// <summary>
    /// Interaction logic for SelectTypeForUpdateAndFile.xaml
    /// </summary>
    public partial class SelectTypeForUpdateAndFile : UserControl
    {
        List<int> IndexOfSheet = new List<int>();

        public SelectTypeForUpdateAndFile()
        {
            InitializeComponent();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.DataGridMakeWork(new UserControls.SettingsAndSubsInf.WorkWithSubInfromation());
        }

        private void ReadData_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.DataGridMakeWork(new UserControls.SettingsAndSubsInf.ShowDataAboutInsert(IndexOfSheet));
        }

        private void Checbox_Checked(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((CheckBox)e.Source).Uid);
            if (!IndexOfSheet.Contains(index))
            {
                IndexOfSheet.Add(index);
            }

        }

        private void Checbox_Unchecked(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((CheckBox)e.Source).Uid);
            if (IndexOfSheet.Contains(index))
            {
                IndexOfSheet.Remove(index);
            }
        }
    }
}

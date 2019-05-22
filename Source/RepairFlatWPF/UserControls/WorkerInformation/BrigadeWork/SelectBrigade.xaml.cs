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

namespace RepairFlatWPF.UserControls.WorkerInformation
{
    /// <summary>
    /// Interaction logic for SelectBrigade.xaml
    /// </summary>
    public partial class SelectBrigadeTable : UserControl
    {
        Model.SomeEnums.TypeOfConrols typeOfConrols;
        public SelectBrigadeTable(Model.SomeEnums.TypeOfConrols typeOfConrols)
        {
            InitializeComponent();
            this.typeOfConrols = typeOfConrols;
            if (typeOfConrols == Model.SomeEnums.TypeOfConrols.Window)
            {
                ButtonForWindow.Visibility = Visibility.Visible;
            }
            else
            {
                ButtonForUserControl.Visibility = Visibility.Visible;
            }
        }

        private void DeletBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditBrigade_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddBrigade_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SelectBrigade_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

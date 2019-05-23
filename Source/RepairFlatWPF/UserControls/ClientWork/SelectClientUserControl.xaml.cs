using System.Windows;
using System.Windows.Controls;

namespace RepairFlatWPF.UserControls.ClientWork
{
    /// <summary>
    /// Interaction logic for SelectWorkerUserControl.xaml
    /// </summary>
    public partial class SelectClientUserControl : UserControl
    {
        Model.SomeEnums.TypeOfConrols typeOfConrols;

        public SelectClientUserControl(Model.SomeEnums.TypeOfConrols typeOfConrols)
        {
            InitializeComponent();
            this.typeOfConrols = typeOfConrols;
            if (this.typeOfConrols == Model.SomeEnums.TypeOfConrols.UserControl)
            {
                ForUserControl.Visibility = Visibility.Visible;
            }
            else
            {
                ForWindow.Visibility = Visibility.Visible;
            }
        }

        private void SelectClient_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditClient_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ReturnBTN_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.CloseBaseWindow();
        }

        private void DeleteClient_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

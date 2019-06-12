using System.Windows;
using System.Windows.Controls;

namespace RepairFlatWPF.UserControls
{
    /// <summary>
    /// Interaction logic for MakeNewOrder.xaml
    /// </summary>
    public partial class BaseWindow : Window
    {

        public BaseWindow(string title)
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel(this, title);

        }
        public void MakeOpen(UserControl userControl)
        {
            GridForContent.Children.Clear();
            GridForContent.Children.Add(userControl);
        }
    }
}

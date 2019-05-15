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

namespace RepairFlatWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool open = true;

        public MainWindow()
        {
            InitializeComponent();
            //GridMenu.Width = 0;
            //TODO Поменять имена полей, сделать переходы
            this.DataContext = new MainWindowViewModel(this);
            //MainGrid.Children.Clear();
            //MainGrid.Children.Add(new LoginUserControl());

        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            open = false;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            //bright.Visibility = Visibility.Collapsed;
        }
        private void ButtonCloseMenu1_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            open = true;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            //bright.Visibility = Visibility.Visible;
        }

    }
}

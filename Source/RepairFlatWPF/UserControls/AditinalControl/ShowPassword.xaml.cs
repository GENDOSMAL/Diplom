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

namespace RepairFlatWPF.UserControls.AditinalControl
{
    /// <summary>
    /// Interaction logic for ShowPassword.xaml
    /// </summary>
    public partial class ShowPassword : UserControl
    {
        BaseWindow window;
        public ShowPassword(string login,string password,ref BaseWindow baseWindow)
        {
            window = baseWindow;
            InitializeComponent();
            Login.Text = login;
            Password.Text = password;
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }
    }
}

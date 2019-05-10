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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void CheckLogin_Click(object sender, RoutedEventArgs e)
        {
            var dd = new LoginWork();
            string base64Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(PasswordText.Password));
            MessageBox.Show(dd.MakeAuth(Login.Text, base64Password).ToString());

        }
    }
}

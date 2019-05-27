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
    /// Interaction logic for ShowDataForAuth.xaml
    /// </summary>
    public partial class ShowDataForAuth : UserControl
    {
        Guid idUser;
        string login,password;
        BaseWindow window;
        public ShowDataForAuth(Guid idUser,ref BaseWindow baseWindow)
        {
            InitializeComponent();
            window = baseWindow;
        }

        private void ForWord_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OnScreen_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow("Информация о логине и пароле");
            baseWindow.MakeOpen(new ShowPassword(login, password,ref baseWindow));
            baseWindow.ShowDialog();            
        }

        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }
    }
}

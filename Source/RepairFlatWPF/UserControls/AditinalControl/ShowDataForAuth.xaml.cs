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
        public ShowDataForAuth(Guid idUser)
        {
            InitializeComponent();
        }

        private void ForWord_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OnScreen_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow(new AditinalControl.ShowPassword(login, password), "Данные о логине и пароле");
            try
            {
                baseWindow.ShowDialog();
            }
            catch
            {
                baseWindow.Close();
            }
        }

        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            MakeSomeHelp.CloseBaseWindow();
        }
    }
}

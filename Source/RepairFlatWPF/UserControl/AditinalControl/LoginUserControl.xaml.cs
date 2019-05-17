using Newtonsoft.Json;
using RepairFlatWPF.Properties;
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
using static RepairFlatWPF.LoginModel;

namespace RepairFlatWPF
{
    /// <summary>
    /// Interaction logic for LoginUserControl.xaml
    /// </summary>
    public partial class LoginUserControl : UserControl
    {
        public  LoginUserControl()
        {
            InitializeComponent();
            

        }
        /// <summary>
        /// Проверка на то, что логин и пароль актуален
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void CheckLogin_Click(object sender, RoutedEventArgs e)
        {
            var SucPing= await MakePingAsync();
            string TextForUser = SucPing ? "Ожидайте связь с сервером" : "Сервер не доступен!";
            Result.Content = TextForUser;

            var Logining = new LoginWork();
            string base64Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(PasswordText.Password));
            var task = await Task.Run(() => Logining.MakeAuth(Login.Text, base64Password));
            var deserializedProduct = JsonConvert.DeserializeObject<LoginModel>(task.ToString());
            if (!deserializedProduct.success)
            {
                MakeSomeHelp.MakeMessageBox(deserializedProduct.description,MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {

            }
        }

        private async Task<bool> MakePingAsync()
        {
            var task = await Task.Run(() => MakeSomeHelp.MakePingToServer(Settings.Default.BaseAdress));
            return task;           
        }


        private void OpenEye_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenEye.Visibility = Visibility.Collapsed;
            CloseEye.Visibility = Visibility.Visible;
            PasswordVisbleText.Text = PasswordText.Password.ToString();
            PasswordVisbleText.Visibility = Visibility.Visible;
            PasswordText.Visibility = Visibility.Collapsed;
        }

        private void CloseEye_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PasswordText.Password = PasswordVisbleText.Text;
            PasswordText.Visibility = Visibility.Visible;
            PasswordVisbleText.Visibility = Visibility.Collapsed;
            CloseEye.Visibility = Visibility.Collapsed;
            OpenEye.Visibility = Visibility.Visible;
        }
    }
}

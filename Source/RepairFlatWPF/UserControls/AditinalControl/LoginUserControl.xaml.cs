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
        /// <summary>
        /// Конструктор
        /// </summary>
        public  LoginUserControl()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Проверка на то, что логин и пароль актуален
        /// </summary>
        public async void CheckLogin_Click(object sender, RoutedEventArgs e)
        {

            var task2 = await Task.Run(() => MakeSomeHelp.MakePingToServer(Settings.Default.BaseAdress));
            string TextForUser = task2 ? "Связь с сервером установлена..." : "Сервер не доступен!";
            Result.Content = TextForUser;
            if (!task2) return;
            CheckLogin.IsEnabled = false;
            CheckLogin.Content = "Ожидание...";

            string base64Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(PasswordText.Password));
            string UrlSend = "api/main/auth";
            string Json = JsonConvert.SerializeObject(new LoginModel.MakeAuth() { login = Login.Text, password = base64Password });

            var task = await Task.Run(() => BaseWorkWithServer.CatchErrorWithPost(UrlSend, "POST", Json, nameof(LoginWork), nameof(MakeAuth)));
            var deserializedProduct = JsonConvert.DeserializeObject<LoginModel>(task.ToString());

            if (!deserializedProduct.success)
            {
                if(MakeSomeHelp.MakeMessageBox(deserializedProduct.description,MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK)
                {
                    Result.Content = "";
                    CheckLogin.Content = "Авторизация";
                    CheckLogin.IsEnabled = true;
                }
            }
            else
            {
                //TODO закрыть окно и перейти к загрузке
            }
        }

        /// <summary>
        /// Показать скрытый пароль
        /// </summary>
        private void OpenEye_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenEye.Visibility = Visibility.Collapsed;
            CloseEye.Visibility = Visibility.Visible;
            PasswordVisbleText.Text = PasswordText.Password.ToString();
            PasswordVisbleText.Visibility = Visibility.Visible;
            PasswordText.Visibility = Visibility.Collapsed;
        }
        /// <summary>
        /// Скрыть показанный пароль
        /// </summary>
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

using RepairFlatWPF.Properties;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RepairFlatWPF.UserControls.SettingsAndSubsInf
{
    /// <summary>
    /// Interaction logic for SettingsUserControls.xaml
    /// </summary>
    public partial class SettingsUserControls : UserControl
    {
        bool Work = true;
        public SettingsUserControls()
        {
            InitializeComponent();
            AdressOfServer.Text = Settings.Default.BaseAdress;
            HederOfMSG.Text = Settings.Default.DefaultHeaderOfMessageBox;
        }

        private void SetSetings_Click(object sender, RoutedEventArgs e)
        {
            MakePingAsync(AdressOfServer.Text);
            if (!Work)
            {
                MakeSomeHelp.MSG("Укажите актуальный адресс к серверу");
            }
            else
            {
                Settings.Default.DefaultHeaderOfMessageBox = HederOfMSG.Text.Trim();
                Settings.Default.BaseAdress = AdressOfServer.Text.Trim();
                MakeSomeHelp.MSG("Настройки установлены!");
            }
        }

        private void CheckServer_Click(object sender, RoutedEventArgs e)
        {
            _ = MakePingAsync(AdressOfServer.Text);
        }
        private async Task MakePingAsync(string adressToServer)
        {
            CheckServer.Content = "Ожидайте...";
            var task2 = await Task.Run(() => MakeSomeHelp.MakePingToServer(adressToServer));
            string TextForUser = task2 ? "Связь с сервером установлена..." : "Сервер не доступен!";
            Work = task2;
            Result.Content = TextForUser;
        }
    }
}

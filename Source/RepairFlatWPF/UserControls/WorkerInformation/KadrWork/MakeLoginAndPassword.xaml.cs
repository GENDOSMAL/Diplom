using Newtonsoft.Json;
using RepairFlat.Model;
using RepairFlatWPF.Model;
using System;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RepairFlatWPF.UserControls.WorkerInformation.KadrWork
{

    public partial class MakeLoginAndPassword : UserControl
    {
        Guid idUser;
        BaseWindow window;
        public MakeLoginAndPassword(ref BaseWindow baseWindow)
        {
            InitializeComponent();
            window = baseWindow;
        }

        private void SelectWorker_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow("Выбор кандидата");
            baseWindow.MakeOpen(new ShowAllWorkers(ref baseWindow, SomeEnums.TypeOfUserNeed.ForRedact));
            baseWindow.ShowDialog();
            if (SaveSomeData.MakeSomeOperation)
            {
                SaveSomeData.MakeSomeOperation = false;
                var rows = SaveSomeData.SomeObject as DataRow;
                SaveSomeData.SomeObject = null;
                idUser = SaveSomeData.idSubs;
                SaveSomeData.idSubs = new Guid();
                WorkerName.Text = $"{rows[1]?.ToString().Trim()} {rows[2]?.ToString().Trim().Substring(0, 1)}.{rows[3]?.ToString().Trim().Substring(0, 1)} : {rows[5]}";
            }
        }

        public class RegisterLoginPerson
        {
            public string login;
            public string password;
            public Guid idUser;
        }


        private async void MakeOperation_Click(object sender, RoutedEventArgs e)
        {
            if (MakeCheck())
            {
                string base64Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(PasswordText.Password));
                var loginf = new RegisterLoginPerson
                {
                    login = Login.Text.Trim(),
                    password = base64Password,
                    idUser = idUser,
                };
                string Json = JsonConvert.SerializeObject(loginf);
                string urlSend = "api/main/createLogin";
                MakeOperation.Content = "Ожидайте...";
                Return.Content = "Ожидайте...";
                Return.IsEnabled = false;
                MakeOperation.IsEnabled = false;
                var task = await Task.Run(() => BaseWorkWithServer.CatchErrorWithPost(urlSend, "POST", Json, nameof(BaseWorkWithServer), nameof(MakeOperation_Click)));
                var deserializedProduct = JsonConvert.DeserializeObject<BaseResult>(task.ToString());

                if (!deserializedProduct.success)
                {
                    MakeSomeHelp.MSG($"Произошла ошикбка  {deserializedProduct.description}", MsgBoxImage: MessageBoxImage.Error);
                }
                else
                {
                    MakeSomeHelp.MSG("Данные добавлены!", MsgBoxImage: MessageBoxImage.Information);
                }
                window.Close();

            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }
        bool MakeCheck()
        {
            if (idUser == new Guid())
            {
                MakeSomeHelp.MSG("Необходимо выбрать работника", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            if (string.IsNullOrEmpty(Login.Text.Trim()))
            {
                MakeSomeHelp.MSG("Необходимо указать логин", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            if (string.IsNullOrEmpty(PasswordText.Password.Trim()))
            {
                MakeSomeHelp.MSG("Необходимо указать пароль", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            return true;
        }
    }
}

using Newtonsoft.Json;
using RepairFlat.Model;
using RepairFlatWPF.Model;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static RepairFlatWPF.Model.WorkerDescriptiom;
using static RepairFlatWPF.SomeEnums;

namespace RepairFlatWPF.UserControls.WorkerInformation.KadrWork
{
    /// <summary>
    /// Interaction logic for SetWorkerDolzn.xaml
    /// </summary>
    public partial class SetWorkerDolzn : UserControl
    {
        Guid idUser;
        Guid idPost;
        BaseWindow window;
        bool Prinyt = true;
        decimal Dsalary;
        string[] NameOfType = new string[] { "AD", "MG", "KW", "BW", "BB", "SW", };
        public SetWorkerDolzn(ref BaseWindow baseWindow, bool Prinyt = true)
        {
            InitializeComponent();
            TypeOfUser.Items.Add("Администратор");
            TypeOfUser.Items.Add("Менеджер");
            TypeOfUser.Items.Add("Работник отдела кадров");
            TypeOfUser.Items.Add("Бухгалтер");
            TypeOfUser.Items.Add("Начальник");
            TypeOfUser.Items.Add("Простой работник");
            window = baseWindow;
            this.Prinyt = Prinyt;
            if (Prinyt)
            {
                TypeOfOperation.Text = "Принятие нового сотрудника";
            }
            else
            {
                TypeOfOperation.Text = "Кадровые перемещения";
            }
        }

        private async void MakeOperation_Click(object sender, RoutedEventArgs e)
        {
            if (MakeCheck())
            {
                DataAboutPost dataAbout = new DataAboutPost();
                string typeofoperation = Prinyt == true ? TypeOfOperate.Adoption.ToString() : TypeOfOperate.Permutation.ToString();
                dataAbout = new DataAboutPost()
                {
                    DateOfOperate = DateTime.Now,
                    idEstabilisment = Guid.NewGuid(),
                    idPost = idPost,
                    idWorker = idUser,
                    Salary = Dsalary,
                    TypeOfOperation = typeofoperation,
                    TypeOfUser = NameOfType[TypeOfUser.SelectedIndex]
                };
                string Json = JsonConvert.SerializeObject(dataAbout);
                string urlSend = "api/worker/createorupdate/postdata";
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

        private void SelectPost_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow("Выбор должности");
            baseWindow.MakeOpen(new UserControls.SettingsAndSubsInf.SelectSomeSubs(ref baseWindow, SomeEnums.TypeOfSubs.Post));
            baseWindow.ShowDialog();
            if (SaveSomeData.MakeSomeOperation)
            {
                SaveSomeData.MakeSomeOperation = false;
                var rows = SaveSomeData.SomeObject as DataRow;
                SaveSomeData.SomeObject = null;
                idPost = SaveSomeData.idSubs;
                SaveSomeData.idSubs = new Guid();
                SelectDolz.Text = $"{rows[1]?.ToString().Trim()} : {rows[2]}";
            }

        }

        private void SelectWorker_Click(object sender, RoutedEventArgs e)
        {
            if (Prinyt)
            {//Выбираем кандидатов
                BaseWindow baseWindow = new BaseWindow("Выбор кандидата");
                baseWindow.MakeOpen(new ShowAllWorkers(ref baseWindow, SomeEnums.TypeOfUserNeed.KD));
                baseWindow.ShowDialog();

            }
            else
            {//Выбираем всех кроме кандидатов
                BaseWindow baseWindow = new BaseWindow("Выбор кандидата");
                baseWindow.MakeOpen(new ShowAllWorkers(ref baseWindow, SomeEnums.TypeOfUserNeed.ForRedact));
                baseWindow.ShowDialog();
            }

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

        bool MakeCheck()
        {
            if (idUser == new Guid())
            {
                MakeSomeHelp.MSG("Необходимо выбрать работника", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            if (idPost == new Guid())
            {
                MakeSomeHelp.MSG("Необходимо выбрать должность", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }

            if (!decimal.TryParse(Salary.Text?.Trim(), out Dsalary))
            {
                MakeSomeHelp.MSG("Необходимо указать заработную плату", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            if (TypeOfUser.SelectedIndex == -1)
            {
                MakeSomeHelp.MSG("Необходимо выбрать тип работника", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            return true;
        }
    }
}

using RepairFlatWPF.Model;
using RepairFlatWPF.UserControls.WorkerInformation.KadrWork;
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
using static RepairFlatWPF.Model.OrderDesc;

namespace RepairFlatWPF.UserControls.OrderWork.AddInfromationUserControl
{
    /// <summary>
    /// Interaction logic for AddWorkerInTask.xaml
    /// </summary>
    public partial class AddWorkerInTask : UserControl
    {
        #region Переменные
        Guid IdWorker;
        bool WorkerSel = false;
        string NameOfPost;
        BaseWindow window;
        #endregion
        #region Конструктор
        public AddWorkerInTask(ref BaseWindow baseWindow, object InfAbWorker = null)
        {
            InitializeComponent();
            window = baseWindow;
            foreach (string Role in SomeEnums.RoleOfWorker)
            {
                RoleOfWorker.Items.Add(Role);
            }
            if (InfAbWorker != null)
            {
                var dd = InfAbWorker as TaskWorker;
                NameOfWorker.Text = dd.FioOfWorker?.Trim();
                int ind = Array.IndexOf(SomeEnums.RoleOfWorker, dd.Role);
                RoleOfWorker.SelectedIndex = ind;
                IdWorker = dd.idWorker;
                WorkerSel = true;
                SetWorkers.Content = "Редактировать";
                NameOfPost = dd.NameOfPost;
            }
        }
        #endregion

        #region Обработчики событий
        private void SetWorkers_Click(object sender, RoutedEventArgs e)
        {
            if (MakeCheck())
            {
                SaveSomeData.MakeSomeOperation = true;
                TaskWorker taskWorker = new TaskWorker
                {
                    FioOfWorker = NameOfWorker.Text,
                    idWorker = IdWorker,
                    NameOfPost = NameOfPost,
                    Role = RoleOfWorker.Text,
                };
                SaveSomeData.SomeObject = taskWorker;
                window.Close();
            }
        }

        private void SelectWorker_Click(object sender, RoutedEventArgs e)
        {
            BaseWindow baseWindow = new BaseWindow("Выбор работника");
            baseWindow.MakeOpen(new ShowAllWorkers(ref baseWindow, SomeEnums.TypeOfUserNeed.ForOrder));
            baseWindow.ShowDialog();
            if (SaveSomeData.MakeSomeOperation)
            {
                SaveSomeData.MakeSomeOperation = false;
                IdWorker = SaveSomeData.idSubs;
                SaveSomeData.idSubs = new Guid();
                var row = SaveSomeData.SomeObject as System.Data.DataRow;
                SaveSomeData.SomeObject = null;
                NameOfWorker.Text = $"{row[1]?.ToString().Trim()} {row[2]?.ToString().Substring(0,1)}.{row[3]?.ToString().Substring(0, 1)}.";
                NameOfPost = row[6]?.ToString();
                WorkerSel = true;
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }
        #endregion

        #region Прочие обработки 

        private bool MakeCheck()
        {
            if (!WorkerSel)
            {
                MakeSomeHelp.MSG("Необходимо указать работника", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            if (RoleOfWorker.SelectedIndex == -1)
            {
                MakeSomeHelp.MSG("Необходимо указать роль работника", MsgBoxImage: MessageBoxImage.Hand);
                return false;
            }
            return true;
        }
        #endregion
    }
}

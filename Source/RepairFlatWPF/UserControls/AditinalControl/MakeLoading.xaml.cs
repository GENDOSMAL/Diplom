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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RepairFlatWPF.UserControls
{
    /// <summary>
    /// Interaction logic for MakeLoading.xaml
    /// </summary>
    public partial class MakeLoading : UserControl
    {
        public MakeLoading()
        {
            InitializeComponent();
            ((Storyboard)FindResource("WaitStoryboard")).Begin();
            MakeDownload();
        }

        public async void MakeDownload()
        {
            #region Получение информаци по обновлению придлагаемых услуг
            string UrlServises = $"api/main/getservises?dateofclient={DateTime.Now.ToString("dd.MM.yyyy")}";
            var taskForServises = await Task.Run(() => BaseWorkWithServer.CatchErrorWithGet(UrlServises, "GET", nameof(MakeLoading), nameof(MakeDownload)));

            #endregion

            #region Получение информации по обновлению типов помещений
            string UrlPremises = $"api/main/getpremises?dateofclient={DateTime.Now.ToString("dd.MM.yyyy")}";
            #endregion

            #region Получение информации по обновлению списка материалов
            string ThirdtUrl = $"api/main/getmaterial?dateofclient={DateTime.Now.ToString("dd.MM.yyyy")}";
            #endregion
        }

    }
}

using Newtonsoft.Json;
using RepairFlatWPF.Properties;
using RepairFlatWPF.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RepairFlatWPF
{
    /// <summary>
    /// Выполнение некоторых повторяющихся событий
    /// </summary>
    public static class MakeSomeHelp
    {
        /// <summary>
        /// Метод простого создния MessageBox 
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="MsgBoxButton"></param>
        /// <param name="MsgBoxImage"></param>
        /// <returns></returns>
        public static MessageBoxResult MakeMessageBox(string Message,MessageBoxButton MsgBoxButton=MessageBoxButton.OK,MessageBoxImage MsgBoxImage=MessageBoxImage.None)
        {
            return MessageBox.Show(Message,Settings.Default.DefaultHeaderOfMessageBox,MsgBoxButton,MsgBoxImage);
        }

        public static bool MakePingToServer(string ServerAdress)
        {
            try
            {
                Ping ping = new Ping();
                PingReply pingReply = null;
                pingReply = ping.Send("ya.ru");
                if(pingReply.Status != IPStatus.TimedOut)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public static WorkWithDB.Tableses ReturnJsonOfTable()
        {
            var Resourses = Properties.Resources.ResourceManager.GetObject("DescriprionOfDB") as byte[];
            string json = Encoding.UTF8.GetString(Resourses);
            var ListOFTablesDescription = JsonConvert.DeserializeObject<WorkWithDB.Tableses>(json);
            return ListOFTablesDescription;
        }

        public static void ChengeGridInMainWindow(UserControl controls)
        {
            ((MainWindow)Application.Current.MainWindow).MainGrid.Children.Clear();
            if(controls!=null)
                ((MainWindow)Application.Current.MainWindow).MainGrid.Children.Add(controls);
        }

        public static void CloseBaseWindow()
        {
            ((UserControls.BaseWindow)Application.Current.MainWindow).Close();
        }


        public static void MakeShowLoading()
        {
            ((MainWindow)Application.Current.MainWindow).ForLogin.Visibility = Visibility.Visible;
            ((MainWindow)Application.Current.MainWindow).ForLogin.Children.Clear();
            ((MainWindow)Application.Current.MainWindow).ForLogin.Children.Add(new LoginUserControl());
            ((MainWindow)Application.Current.MainWindow).ForLogin.Background = (System.Windows.Media.Brush)Application.Current.Resources["BackLogAndLoadColor"];
        }

        public static void MakeLoading()
        {
            ((MainWindow)Application.Current.MainWindow).ForLogin.Children.Clear();
            ((MainWindow)Application.Current.MainWindow).ForLogin.Children.Add(new MakeLoading());
            ((MainWindow)Application.Current.MainWindow).ForLogin.Background = (System.Windows.Media.Brush)Application.Current.Resources["GradientForLoading"];
        }

        public static void ShowMainGrid()
        {
            ((MainWindow)Application.Current.MainWindow).ForLogin.Visibility = Visibility.Collapsed;
            ((MainWindow)Application.Current.MainWindow).ForLogin.Children.Clear();
        }
        public static void ChengeGridBackGroundStyle(string NameOfStyle)
        {
            ((MainWindow)Application.Current.MainWindow).MainGrid.Background =(System.Windows.Media.Brush)Application.Current.Resources[NameOfStyle];
        }
        public static void GridChengeGridBackGroundStyle(string NameOfStyle)
        {
            ((MainWindow)Application.Current.MainWindow).ForLogin.Background = (System.Windows.Media.Brush)Application.Current.Resources[NameOfStyle];
        }

        public static void DataGridMakeWork(UserControl controls)
        {
            ((MainWindow)Application.Current.MainWindow).GridForContent.Children.Clear();
            if (controls != null)
                ((MainWindow)Application.Current.MainWindow).GridForContent.Children.Add(controls);
        }

        public static void ShowOrCloseMenu(bool Open)
        {
            if(Open)
                ((MainWindow)Application.Current.MainWindow).GridMenu.Width = 300;
            else
                ((MainWindow)Application.Current.MainWindow).GridMenu.Width = 0;
        }
    }
}

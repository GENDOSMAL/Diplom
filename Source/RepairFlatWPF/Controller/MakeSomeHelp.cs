using Newtonsoft.Json;
using RepairFlatWPF.Properties;
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
            ((MainWindow)Application.Current.MainWindow).MainGrid.Children.Add(controls);
        }

        public static void ChengeGridBackGroundStyle(string NameOfStyle)
        {
            ((MainWindow)Application.Current.MainWindow).MainGrid.Background =(System.Windows.Media.Brush)Application.Current.Resources[NameOfStyle];
        }
    }
}

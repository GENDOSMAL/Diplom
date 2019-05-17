using Newtonsoft.Json;
using RepairFlatWPF.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
    }
}

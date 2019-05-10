using System;
using System.IO;
using System.Reflection;
using System.Web.Hosting;

namespace RepairFlatRestApi.Controllers.OtherController
{
    public class Logger
    {
        public enum TypeOfRecord { Information = 'I', Warning = 'W', Exception = 'E', Transaktion = 'T' }


        public static void WriteToLog(TypeOfRecord WhatType, string Class, string Method, string MessageToLog)
        {
            string LogPath = Path.GetDirectoryName(HostingEnvironment.ApplicationPhysicalPath);
            if (WorkWithFile(ref LogPath))
            {
                string content = $"{DateTime.Now.TimeOfDay.ToString()}\t\t{(char)WhatType}\t\t{Class}::{Method}\t\t{MessageToLog}{Environment.NewLine}";
                File.AppendAllText(LogPath, content);
            }
            DeleteAfter();
        }


        public static bool WorkWithFile(ref string LogPath)
        {
            LogPath = Path.Combine(LogPath, Properties.Settings.Default.DefaultLogPath);
            if (Properties.Settings.Default.DefaultLogPath == "")
            {
                LogPath = Path.Combine(Assembly.GetExecutingAssembly().Location, @"Log");
            }
            if (!Directory.Exists(LogPath))
            {
                Directory.CreateDirectory(LogPath);
            }
            DateTime dateTime = DateTime.Now;
            string fileName = "RepFlat" + dateTime.ToString("''yy") + dateTime.ToString("MM", System.Globalization.CultureInfo.GetCultureInfo("ru-RU")) + dateTime.ToString("dd''");
            LogPath = Path.Combine(LogPath, $"{fileName}.log");
            return true;
        }

        public static void DeleteAfter()
        {
            string LogPath = Path.Combine(Path.GetDirectoryName(HostingEnvironment.ApplicationPhysicalPath), Properties.Settings.Default.DefaultLogPath);
            if (Directory.Exists(LogPath))
            {
                int KeepLogsDays = Properties.Settings.Default.KeepLogsDays;
                if (KeepLogsDays <= 0)
                {
                    KeepLogsDays = -1;
                }
                else
                {
                    KeepLogsDays = KeepLogsDays * -1;
                }
                DateTime dateTime = DateTime.Now;
                dateTime = dateTime.AddDays(KeepLogsDays);
                for (int i = 0; i < 30; i++)
                {
                    string fileName = "EP" + dateTime.ToString("''yy") + dateTime.ToString("MM", System.Globalization.CultureInfo.GetCultureInfo("ru-RU")) + dateTime.ToString("dd''");
                    string sr = LogPath + @"\" + fileName + ".log";
                    if (File.Exists(sr))
                    {
                        File.Delete(sr);
                        WriteToLog(
                            TypeOfRecord.Information,
                            nameof(Logger),
                            nameof(DeleteAfter),
                            $"Удаление файла логов по адресу <{Path.GetFullPath(sr)}>;");
                    }
                    dateTime = dateTime.AddDays(-1);
                }
            }
            else
            {
                return;
            }
        }
    }
}

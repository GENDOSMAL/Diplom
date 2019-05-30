using RepairFlatWPF.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace RepairFlatWPF
{

    public static class BaseWorkWithServer
    {

        public static object CatchErrorWithPost(string PosfixUrl, string typeOfMessage, string WhatSend, string NameOfClass, string nameOfMethod)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(UrlSendMake(PosfixUrl));
                request.ContentType = "application/json";
                request.Method = typeOfMessage;

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.WriteAsync(WhatSend);
                }
                var response = (HttpWebResponse)request.GetResponse();
                object result = new object();
                using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                return result;
            }
            catch (Exception ex)
            {

                MakeSomeHelp.MSG($"Произошла ошибка при работе с сервером {ex.Message}");
                return null;
            }
        }

        public static object CatchErrorWithGet(string PosfixUrl, string typeOfMessage, string NameOfClass, string nameOfMethod)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(UrlSendMake(PosfixUrl));
                request.ContentType = "application/json";
                request.Method = typeOfMessage;
                var response = (HttpWebResponse)request.GetResponse();
                object result = new object();
                using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                return result;
            }
            catch (Exception ex)
            {
                MakeSomeHelp.MSG($"Произошла ошибка при работе с сервером {ex.Message}");
                return null;
            }
        }

        private static string UrlSendMake(string urlSecondPart)
        {
            string baseAdress = Settings.Default.BaseAdress;
            if (string.IsNullOrEmpty(baseAdress)) throw new Exception("Необходимо указать адрес для сервера для работы!");
            Uri baseUri = new Uri(baseAdress);
            Uri resultUrl = new Uri(baseUri, urlSecondPart);
            return resultUrl.ToString();
        }
    }
}

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
    /// <summary>
    /// 
    /// </summary>
    public static class BaseWorkWithServer
    {
        /// <summary>
        /// Метод выполняет запрос на получение данных после чего считывает их и возвращает данные в качестве объекта.
        /// </summary>
        /// <param name="PosfixUrl"></param>
        /// <param name="typeOfMessage"></param>
        /// <param name="WhatSend"></param>
        /// <param name="NameOfClass"></param>
        /// <param name="nameOfMethod"></param>
        /// <returns></returns>
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
                //Скопировать класс с сервера Logger.WriteToLog(Logger.TypeOfRecord.Exception, NameOfClass, nameOfMethod, ex.ToString().Replace(Environment.NewLine, ""));
                // Что-то сделать с этим
                throw new Exception("Произошла ошибка!" + ex.ToString());
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
                //Скопировать класс с сервера Logger.WriteToLog(Logger.TypeOfRecord.Exception, NameOfClass, nameOfMethod, ex.ToString().Replace(Environment.NewLine, ""));
                // Что-то сделать с этим

                throw new Exception("Произошла ошибка! " + ex.ToString());
            }
        }


        /// <summary>
        /// Создание итоговой ссылки для работы из базового адреса сервера и постфикса для необхожимой операции
        /// </summary>
        /// <param name="urlSecondPart">Постфикс необходимой операции</param>
        /// <returns></returns>
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

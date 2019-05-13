using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RepairFlatWPF
{
    public static class BaseWorkWithServer
    {
        //ПОДУМАТЬ НАД ЭТИМ
        public static object CatchErrorWithPost(string url, string typeOfMessage, string WhatSend, string NameOfClass, string nameOfMethod)
        {
            try
            {
                //var request = (HttpWebRequest)WebRequest.Create(url);
                //request.ContentType = "application/json";
                //request.Method = typeOfMessage;

                //using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                //{
                //    streamWriter.Write(WhatSend);
                //}
                //var response =await Task.Run(()=> (HttpWebResponse)request.GetResponse());
                //Task<object> result = null;
                //using (var streamReader = new StreamReader(response.GetResponseStream()))
                //{
                //    result =  streamReader.ReadToEnd();
                //}
                //return result;


                Task<object> t = Task<object>.Run(async () =>
                {

                    var request = (HttpWebRequest)WebRequest.Create(url);
                    request.ContentType = "application/json";
                    request.Method = typeOfMessage;

                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        streamWriter.Write(WhatSend);
                    }
                    var response = (HttpWebResponse)request.GetResponse();

                    object result = new object();
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        result = await streamReader.ReadToEndAsync();
                    }
                    return result;
                });

                t.Wait();
                return t.Result;
            }
            catch (Exception ex)
            {
                //Скопировать класс с сервера Logger.WriteToLog(Logger.TypeOfRecord.Exception, NameOfClass, nameOfMethod, ex.ToString().Replace(Environment.NewLine, ""));
                // Что-то сделать с этим
                throw new Exception("Произошла ошибка!");
            }
        }
    }
}

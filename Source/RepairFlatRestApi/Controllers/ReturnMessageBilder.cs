using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;

namespace RepairFlatRestApi.Controllers
{
    public class ReturnMessageBilder
    {
        public static HttpResponseMessage MakeResponseMessage(object Message, HttpStatusCode httpStatusCode)
        {
            try
            {
                HttpResponseMessage resultMessage = new HttpResponseMessage(httpStatusCode);
                var JsonResult = JsonConvert.SerializeObject(Message, Formatting.Indented);
                resultMessage.Content = new StringContent(JsonResult.ToString(), Encoding.UTF8, "application/json");
                return resultMessage;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString().Replace(Environment.NewLine, ""));
            }
        }
    }
}
using Newtonsoft.Json;
using RepairFlatRestApi.Models.DescriptionJSON;
using System;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace RepairFlatRestApi.Controllers.MainConroller
{
    public class BaseController : ApiController
    {
        protected HttpResponseMessage CatchError(Func<HttpResponseMessage> action, string NameOfClass, string nameOfMethod)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(Logger.TypeOfRecord.Exception, NameOfClass, nameOfMethod, ex.ToString().Replace(Environment.NewLine, ""));
                return new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new BaseResult() { success = false, description = ex.ToString() }), Encoding.UTF8, "application/json"),
                };
            }
        }

        protected HttpResponseMessage CatchError2(Func<object> action, string NameOfClass, string nameOfMethod)
        {
            try
            {
                var resp = action();
                var response = new HttpResponseMessage();
                response.Content = new StringContent(JsonConvert.SerializeObject(resp), Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(Logger.TypeOfRecord.Exception, NameOfClass, nameOfMethod, ex.ToString().Replace(Environment.NewLine, ""));
                return new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new BaseResult() { success = false, description = ex.ToString() }), System.Text.Encoding.UTF8, "application/json"),
                };
            }
        }
    }
}
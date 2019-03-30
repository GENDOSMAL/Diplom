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
    public class MakeReturnMessage
    {
        public static HttpResponseMessage MakeAnswerOfAuth(bool sucess, string description, int? id, int? typeOfpolz, HttpStatusCode httpStatusCode)
        {
            try
            {
                HttpResponseMessage resultMessage = new HttpResponseMessage(httpStatusCode);
                var infromationToClient = new Models.AuthDescription.ResultOfInformation()
                {
                    sucess = sucess,
                    description = description,
                    idPolz = id,
                    typeofpolz = typeOfpolz
                };

                var JsonResult = JsonConvert.SerializeObject(infromationToClient, Formatting.Indented);
                resultMessage.Content = new StringContent(JsonResult.ToString(), Encoding.UTF8, "application/json");
                return resultMessage;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
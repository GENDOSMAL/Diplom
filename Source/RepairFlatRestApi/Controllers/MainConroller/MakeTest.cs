using RepairFlatRestApi.Models.DescriptionJSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace RepairFlatRestApi.Controllers
{
    public class MakeTest:BaseController
    {
        [HttpGet, Route("api/get")]
        public HttpResponseMessage SendAllUpdateServises()
        {
            return CatchError(() =>
            {
                return new BaseResult() { description = "sad" };
                //return DBController.CreateLoginPerson(InformationAboutNewPerson);
                //throw new NotImplementedException();
            }, nameof(LoginController), nameof(SendAllUpdateServises));
        }
    }
}
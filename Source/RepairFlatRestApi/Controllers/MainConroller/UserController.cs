using RepairFlatRestApi.Models.DescriptionJSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace RepairFlatRestApi.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController:BaseController
    {
        [HttpPost, Route("create")]
        public HttpResponseMessage CreateNewPerson([FromBody] PersonDesctiption.CreateNewClient DescriptionPerson)
        {
            return CatchError(() =>
            {
                return DBController.CreateNewClient(DescriptionPerson);
            }, nameof(LoginController), nameof(CreateNewPerson));
        }

    }
}
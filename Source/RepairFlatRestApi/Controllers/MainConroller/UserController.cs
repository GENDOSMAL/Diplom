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
        /// <summary>
        /// Метод для создании базовой информации о пользователе в таблице User
        /// </summary>
        /// <param name="DescriptionPerson"></param>
        /// <returns></returns>
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
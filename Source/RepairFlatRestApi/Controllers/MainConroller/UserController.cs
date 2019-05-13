using RepairFlatRestApi.Models.DescriptionJSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace RepairFlatRestApi.Controllers
{
    /// <summary>
    /// Класс для работы с информацией о пользователях в том добавлении информации о новом пользователе и еще много чего...
    /// </summary>
    public class UserController:BaseController
    {
        /// <summary>
        /// Метод для создании базовой информации о пользователе в таблице User
        /// </summary>
        /// <param name="DescriptionPerson"></param>
        /// <returns></returns>
        [HttpPost, Route("api/main/createperson")]
        public HttpResponseMessage CreateNewPerson([FromBody] PersonDesctiption.DescriptionOfNewUser DescriptionPerson)
        {
            return CatchError(() =>
            {
                return DBController.CreateUser(DescriptionPerson);
            }, nameof(LoginController), nameof(CreateNewPerson));
        }
    }
}
﻿using RepairFlatRestApi.Models.DescriptionJSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace RepairFlatRestApi.Controllers.MainConroller
{
    public class WorkWithUserController:BaseController
    {
        [HttpPost, Route("api/main/createperson")]
        public HttpResponseMessage CreateNewPerson([FromBody] PersonDesctiption.DescriptionOfNewPerson DescriptionPerson)
        {
            return CatchError2(() =>
            {
                return OtherController.DBController.CreateUser(DescriptionPerson);
            }, nameof(LoginController), nameof(CreateNewPerson));
        }
    }
}
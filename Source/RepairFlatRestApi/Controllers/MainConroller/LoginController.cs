using Newtonsoft.Json;
using RepairFlatRestApi.Controllers.MainConroller;
using RepairFlatRestApi.Controllers.OtherController;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using RepairFlatRestApi.Areas.HelpPage.Controllers;
using System.Text;
using RepairFlatRestApi.Models.DescriptionJSON;

namespace RepairFlatRestApi.Controllers
{
    public class LoginController : BaseController
    {
        [HttpPost, Route("api/main/auth")]
        public HttpResponseMessage MakeAuth([FromBody]AuthDescription.AskedInformation InformationAboutAuth)
        {
            return CatchError2(()=> 
            {
                AuthDescription.ResultOfInformation resLog = DBController.Logining(InformationAboutAuth);
                return resLog;
            }, nameof(LoginController), nameof(MakeAuth));
        }

        [HttpPost, Route("api/main/createLogin")]
        public HttpResponseMessage MakeNewLoginPerson([FromBody]Models.DescriptionJSON.AuthDescription.RegisterLoginPerson InformationAboutNewPerson)
        {
            return CatchError2(() =>
            {
                return DBController.CreateLoginPerson(InformationAboutNewPerson);
            }, nameof(LoginController), nameof(MakeNewLoginPerson));
        }


    }
}
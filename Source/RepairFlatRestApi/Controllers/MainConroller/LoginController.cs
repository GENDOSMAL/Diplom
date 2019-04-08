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

        //[HttpPost, Route("api/main/createperson")]
        //public HttpResponseMessage MakeNewPerson([FromBody]object InformationAboutNewAuth)
        //{
        //    if (InformationAboutNewAuth != null)
        //    {
        //        try
        //        {
        //            var infromationToMakeNewLogin = JsonConvert.DeserializeObject<Models.DescriptionJSON.AuthDescription.RegisterLoginPerson>(InformationAboutNewAuth.ToString());
        //            string query = "if not EXISTS (Select login from RepairFlat.dbo.login where login=@login) begin Insert into RepairFlat.dbo.login (login,password,idPersonInOtherTable,typeofPolz) values (@login,@password,@idPersonInOtherTable,@typeofPolz) end;";
        //            SqlParameter[] sqlParameters = new SqlParameter[4];
        //            sqlParameters[0] = new SqlParameter("@login", infromationToMakeNewLogin.login);
        //            sqlParameters[1] = new SqlParameter("@password", System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(infromationToMakeNewLogin.password)))));
        //            sqlParameters[2] = new SqlParameter("@idPersonInOtherTable", infromationToMakeNewLogin.idPolz);
        //            sqlParameters[3] = new SqlParameter("@typeofPolz", infromationToMakeNewLogin.typeofpolz);
        //            var result = WorkWithDataBase.MakeUpdateAndInsert(query, sqlParameters);
        //            if (result)
        //            {
        //                return ReturnMessageBilder.MakeResponseMessage(new AuthDescription.ResultOfInformation { sucess = true }, HttpStatusCode.OK);
        //            }
        //            else
        //            {
        //                return ReturnMessageBilder.MakeResponseMessage(new AuthDescription.ResultOfInformation { sucess = false, description = "Логин уже используется" }, HttpStatusCode.InternalServerError);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Logger.WriteToLog(Logger.TypeOfRecord.Exception, nameof(LoginController), nameof(MakeNewPerson), ex.ToString().Replace(Environment.NewLine, ""));
        //            return ReturnMessageBilder.MakeResponseMessage(new AuthDescription.ResultOfInformation { sucess = true ,description =ex.ToString()}, HttpStatusCode.InternalServerError);
        //        }
        //    }
        //    else
        //    {
        //        string description = $"Передано пустое сообщение < >";
        //        Logger.WriteToLog(Logger.TypeOfRecord.Exception, nameof(LoginController), nameof(MakeNewPerson), description);
        //        return ReturnMessageBilder.MakeResponseMessage(new AuthDescription.ResultOfInformation { sucess = true,description=description }, HttpStatusCode.BadRequest);
        //    }
        //    throw new NotImplementedException();
        //}

        //[HttpPost, Route("api/main/createperson")]
    }
}
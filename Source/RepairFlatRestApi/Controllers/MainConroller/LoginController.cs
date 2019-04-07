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
    public class LoginController : ApiController
    {
        [HttpPost, Route("api/main/auth")]
        public HttpResponseMessage MakeAuth([FromBody]Models.DescriptionJSON.AuthDescription.AskedInformation InformationAboutAuth)
        {
            HttpResponseMessage resultMessage = new HttpResponseMessage(HttpStatusCode.OK);
            if (InformationAboutAuth != null)
            {
                try
                {
                    //Models.DescriptionJSON.AuthDescription.AskedInformation infromationToAuth = JsonConvert.DeserializeObject<Models.DescriptionJSON.AuthDescription.AskedInformation>(InformationAboutAuth.ToString());
                    Logger.WriteToLog(Logger.TypeOfRecord.Information, nameof(LoginController), nameof(MakeAuth), $"Сервер получил и обработал переданную информацию <{InformationAboutAuth.ToString().Replace(Environment.NewLine, "")}>");
                    AuthDescription.ResultOfInformation result =DBController.Logining(InformationAboutAuth);
                    if (!result.sucess)
                    {
                        Logger.WriteToLog(Logger.TypeOfRecord.Information, nameof(LoginController), nameof(MakeAuth), $"Не корректный логин или пароль!");
                        return ReturnMessageBilder.MakeResponseMessage(result, HttpStatusCode.NotAcceptable);
                    }
                    else
                    {
                        Logger.WriteToLog(Logger.TypeOfRecord.Information, nameof(LoginController), nameof(MakeAuth), $"Найдена информация о логине и пароле!");
                        return ReturnMessageBilder.MakeResponseMessage(result, HttpStatusCode.OK);
                    }

                }
                catch (Exception ex)
                {
                    Logger.WriteToLog(Logger.TypeOfRecord.Exception, nameof(LoginController), nameof(MakeAuth), ex.ToString().Replace(Environment.NewLine, ""));
                    return ReturnMessageBilder.MakeResponseMessage(new AuthDescription.ResultOfInformation {sucess=false, description=ex.ToString()}, HttpStatusCode.InternalServerError);
                }
            }
            else
            {
                string description = $"Передано пустое сообщение < >";
                Logger.WriteToLog(Logger.TypeOfRecord.Exception, nameof(LoginController), nameof(MakeAuth), description);
                return ReturnMessageBilder.MakeResponseMessage(new AuthDescription.ResultOfInformation { sucess = false, description = description }, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost, Route("api/main/createperson")]
        public HttpResponseMessage MakeNewPerson([FromBody]object InformationAboutNewAuth)
        {
            if (InformationAboutNewAuth != null)
            {
                try
                {
                    var infromationToMakeNewLogin = JsonConvert.DeserializeObject<Models.DescriptionJSON.AuthDescription.RegisterLoginPerson>(InformationAboutNewAuth.ToString());
                    string query = "if not EXISTS (Select login from RepairFlat.dbo.login where login=@login) begin Insert into RepairFlat.dbo.login (login,password,idPersonInOtherTable,typeofPolz) values (@login,@password,@idPersonInOtherTable,@typeofPolz) end;";
                    SqlParameter[] sqlParameters = new SqlParameter[4];
                    sqlParameters[0] = new SqlParameter("@login", infromationToMakeNewLogin.login);
                    sqlParameters[1] = new SqlParameter("@password", System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(infromationToMakeNewLogin.password)))));
                    sqlParameters[2] = new SqlParameter("@idPersonInOtherTable", infromationToMakeNewLogin.idPolz);
                    sqlParameters[3] = new SqlParameter("@typeofPolz", infromationToMakeNewLogin.typeofpolz);
                    var result = WorkWithDataBase.MakeUpdateAndInsert(query, sqlParameters);
                    if (result)
                    {
                        return ReturnMessageBilder.MakeResponseMessage(new AuthDescription.ResultOfInformation { sucess = true }, HttpStatusCode.OK);
                    }
                    else
                    {
                        return ReturnMessageBilder.MakeResponseMessage(new AuthDescription.ResultOfInformation { sucess = false, description = "Логин уже используется" }, HttpStatusCode.InternalServerError);
                    }
                }
                catch (Exception ex)
                {
                    Logger.WriteToLog(Logger.TypeOfRecord.Exception, nameof(LoginController), nameof(MakeNewPerson), ex.ToString().Replace(Environment.NewLine, ""));
                    return ReturnMessageBilder.MakeResponseMessage(new AuthDescription.ResultOfInformation { sucess = true ,description =ex.ToString()}, HttpStatusCode.InternalServerError);
                }
            }
            else
            {
                string description = $"Передано пустое сообщение < >";
                Logger.WriteToLog(Logger.TypeOfRecord.Exception, nameof(LoginController), nameof(MakeNewPerson), description);
                return ReturnMessageBilder.MakeResponseMessage(new AuthDescription.ResultOfInformation { sucess = true,description=description }, HttpStatusCode.BadRequest);
            }
            throw new NotImplementedException();
        }

        //[HttpPost, Route("api/main/createperson")]
    }
}
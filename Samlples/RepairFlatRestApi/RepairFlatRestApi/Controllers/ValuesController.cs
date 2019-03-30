﻿using Newtonsoft.Json;
using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace RepairFlatRestApi.Controllers
{
    public class ValuesController : ApiController
    {

        [HttpPost, Route("api/main/auth")]
        public HttpResponseMessage MakeAuth([FromBody]object InformationAboutAuth)
        {
            HttpResponseMessage resultMessage = new HttpResponseMessage(HttpStatusCode.OK);
            if (InformationAboutAuth != null)
            {
                try
                {
                    var infromationToAuth = JsonConvert.DeserializeObject<Models.AuthDescription.AskedInformation>(InformationAboutAuth.ToString());
                    Logger.WriteToLog(Logger.TypeOfRecord.Information, nameof(ValuesController), nameof(MakeAuth), $"Сервер получил и обработал переданную информацию <{InformationAboutAuth.ToString().Replace(Environment.NewLine,"")}>");
                    string[] whatSelect = new string[] { "typeOfPolz", "idPersonInOtherTable" };
                    string query = "Select typeOfPolz,idPersonInOtherTable from RepairFlat.dbo.login where login = @login and password = @password; ";
                    SqlParameter[] sqlParameters = new SqlParameter[2];
                    sqlParameters[0] = new SqlParameter("@login", infromationToAuth.login);
                    sqlParameters[1] = new SqlParameter("@password", infromationToAuth.password);
                    var resultOfSelect = WorkWithDataBase.SelectSmallVolumeOfData(query, sqlParameters, whatSelect);
                    if (resultOfSelect != null)
                    {
                        int? typeOfPolz = 0;
                        int? idPersonInOtherTable = 0;
                        for (int i = 0; i < resultOfSelect.Count; i++)
                        {
                            if (resultOfSelect[i].Item1 == "typeOfPolz")
                            {
                                typeOfPolz = Convert.ToInt32(resultOfSelect[i].Item2);
                            }
                            else if (resultOfSelect[i].Item1 == "idPersonInOtherTable")
                            {
                                idPersonInOtherTable = Convert.ToInt32(resultOfSelect[i].Item2);
                            }
                        }
                        Logger.WriteToLog(Logger.TypeOfRecord.Information, nameof(ValuesController), nameof(MakeAuth), $"Найдена информация о логине и пароле!");

                        return MakeReturnMessage.MakeAnswerOfAuth(true, null, typeOfPolz, idPersonInOtherTable, HttpStatusCode.OK);
                    }
                    else
                    {
                        Logger.WriteToLog(Logger.TypeOfRecord.Warning, nameof(ValuesController), nameof(MakeAuth), $"Не найдена информация о данном логине и пароле!");
                        return MakeReturnMessage.MakeAnswerOfAuth(false, "Не логин или пароль!", null, null, HttpStatusCode.BadRequest);
                    }
                }
                catch (Exception ex)
                {
                    Logger.WriteToLog(Logger.TypeOfRecord.Exception, nameof(ValuesController), nameof(MakeAuth), ex.ToString());
                    return MakeReturnMessage.MakeAnswerOfAuth(false, ex.ToString(), null, null, HttpStatusCode.InternalServerError);
                }
            }
            else
            {
                string description = $"Передано пустое сообщение < >";
                Logger.WriteToLog(Logger.TypeOfRecord.Exception,nameof(ValuesController),nameof(MakeAuth), description);
                return MakeReturnMessage.MakeAnswerOfAuth(false, description, null, null, HttpStatusCode.BadRequest);
            }
        }

       
    }
}

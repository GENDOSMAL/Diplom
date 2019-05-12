using System.Net.Http;
using System.Web.Http;
using RepairFlatRestApi.Models.DescriptionJSON;
using static RepairFlatRestApi.Controllers.Logger;

namespace RepairFlatRestApi.Controllers
{
    public class LoginController : BaseController
    {
        /// <summary>
        /// Метод для авторизации 
        /// </summary>
        /// <param name="InformationAboutAuth"></param>
        /// <returns></returns>
        [HttpPost, Route("api/main/auth")]
        public HttpResponseMessage MakeAuth([FromBody]AuthDescription.AskedInformation InformationAboutAuth)
        {
            return CatchError2(() =>
            {
                AuthDescription.ResultOfInformation resLog = DBController.Logining(InformationAboutAuth);
                return resLog;
            }, nameof(LoginController), nameof(MakeAuth));
        }
        /// <summary>
        /// Метод создания информации о логине
        /// </summary>
        /// <param name="InformationAboutNewPerson"></param>
        /// <returns></returns>
        [HttpPost, Route("api/main/createLogin")]
        public HttpResponseMessage MakeNewLoginPerson([FromBody]AuthDescription.RegisterLoginPerson InformationAboutNewPerson)
        {
            return CatchError2(() =>
            {
                return DBController.CreateLoginPerson(InformationAboutNewPerson);
            }, nameof(LoginController), nameof(MakeNewLoginPerson));
        }

        [HttpGet, Route("api/main")]
        public HttpResponseMessage GEt()
        {
            WriteToLog(
                TypeOfRecord.Information,
                nameof(Logger),
                nameof(DeleteAfter),
                $"Удаление файла логов по адресу <>;");
            return CatchError2(() =>
            {
                return DBController.CreateLog();
            }, nameof(LoginController), nameof(MakeNewLoginPerson));
        }
    }
}
using System.Net.Http;
using System.Web.Http;
using RepairFlatRestApi.Models.DescriptionJSON;
using static RepairFlatRestApi.Controllers.Logger;

namespace RepairFlatRestApi.Controllers
{
    /// <summary>
    /// Класс который принимает все переходы по ссылкам api связанные с работой с аккаунтами и авторизацией 
    /// </summary>
    public class LoginController : BaseController
    {
        /// <summary>
        /// Метод для проверки актуальности логина и пароля переданного в качестве логинов имеющихся в базе данных
        /// </summary>
        /// <param name="InformationAboutAuth">Строка которая передается в фомате Json на сервер и сериализуется в класс <see cref="AuthDescription.AskedInformation"/> </param>
        /// <returns></returns>
        [HttpPost, Route("api/main/auth")]
        public HttpResponseMessage MakeAuth([FromBody]AuthDescription.AskedInformation InformationAboutAuth)
        {
            return CatchError(() =>
            {
                AuthDescription.ResultOfInformation resLog = DBController.Logining(InformationAboutAuth);
                return resLog;
            }, nameof(LoginController), nameof(MakeAuth));
        }
        /// <summary>
        /// Данный метод вызывает базовый класс и актуальный метод в обертке проверки на ошибки, для работы с базой данных.
        /// </summary>
        /// <param name="InformationAboutNewPerson">Строка которая передается в фомате Json на сервер и сериализуется в класс <see cref="AuthDescription.RegisterLoginPerson"/></param>
        /// <returns></returns>
        [HttpPost, Route("api/main/createLogin")]
        public HttpResponseMessage MakeNewLoginPerson([FromBody]AuthDescription.RegisterLoginPerson InformationAboutNewPerson)
        {
            return CatchError(() =>
            {
                return DBController.CreateLoginPerson(InformationAboutNewPerson);
            }, nameof(LoginController), nameof(MakeNewLoginPerson));
        }
        /// <summary>
        /// Метод для тестирования работоспособности сервера. В дальнейшем удалить
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("api/test")]
        public HttpResponseMessage GEt()
        {
            WriteToLog(
                TypeOfRecord.Information,
                nameof(Logger),
                nameof(DeleteAfter),
                $"Удаление файла логов по адресу <>;");
            return CatchError(() =>
            {
                return DBController.CreateLog();
            }, nameof(LoginController), nameof(MakeNewLoginPerson));
        }
    }
}
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
        [HttpPost, Route("api/main/auth")]
        public HttpResponseMessage MakeAuth([FromBody]AuthDescription.AskedInformation InformationAboutAuth)
        {
            return CatchError(() =>
            {
                return DBBaseController.Logining(InformationAboutAuth);
            }, nameof(LoginController), nameof(MakeAuth));
        }
        [HttpPost, Route("api/main/createLogin")]
        public HttpResponseMessage MakeNewLoginPerson([FromBody]AuthDescription.RegisterLoginPerson InformationAboutNewPerson)
        {
            return CatchError(() =>
            {
                return DBBaseController.CreateLoginPerson(InformationAboutNewPerson);
            }, nameof(LoginController), nameof(MakeNewLoginPerson));
        }

    }
}
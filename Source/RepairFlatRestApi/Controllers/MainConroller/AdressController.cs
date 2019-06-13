using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace RepairFlatRestApi.Controllers.MainConroller
{
    [RoutePrefix("api/Adress")]
    public class AdressController:BaseController
    {
        [HttpPost, Route("create")]
        public HttpResponseMessage CreateNewAdress([FromBody] Models.AdressModel.AdressDesc NewAdress)
        {
            return CatchError(() =>
            {
                return DBBaseController.CreaNewAdress(NewAdress);
            }, nameof(SubStringController), nameof(CreateNewAdress));
        }
        [HttpGet, Route("data")]
        public HttpResponseMessage MakeDataAbout([FromUri] Guid idAdress)
        {
            return CatchError(() =>
            {
                return DBBaseController.GetDataAboutContact(idAdress);
            }, nameof(SubStringController), nameof(CreateNewAdress));
        }

        [HttpPost, Route("update")]
        public HttpResponseMessage UpdateAdress([FromBody] Models.AdressModel.AdressDesc UpdatedAdress)
        {
            return CatchError(() =>
            {
                return DBBaseController.UpdateDataAboutAdress(UpdatedAdress);
            }, nameof(SubStringController), nameof(CreateNewAdress));
        }
    }
}
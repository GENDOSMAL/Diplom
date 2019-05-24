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
        public HttpResponseMessage CreateNewAdress([FromBody] Models.AdressDescription NewAdress)
        {
            return CatchError(() =>
            {
                return DBController.CreaNewAdress(NewAdress);
            }, nameof(SubStringController), nameof(CreateNewAdress));
        }
        [HttpPost, Route("create")]
        public HttpResponseMessage UpdateAdress([FromBody] Models.AdressDescription NewAdress)
        {
            return CatchError(() =>
            {
                return DBController.UpdateDataAboutAdress(NewAdress);
            }, nameof(SubStringController), nameof(CreateNewAdress));
        }
    }
}
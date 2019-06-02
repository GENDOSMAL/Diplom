using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace RepairFlatRestApi.Controllers.MainConroller
{
    [RoutePrefix("api/Worker")]
    public class WorkerController:BaseController
    {

        [HttpGet, Route("allworkerWork")]
        public HttpResponseMessage GetDataAboutContact()
        {
            return CatchError(() =>
            {
                return OtherController.WorkerConroller.CreateListWorkerWork();
            }, nameof(SubStringController), nameof(GetDataAboutContact));
        }

    }
}
using RepairFlatRestApi.Controllers.OtherController.DBController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace RepairFlatRestApi.Controllers.MainConroller
{
    [RoutePrefix("api/Statistik")]
    public class StatistikController:BaseController
    {
        [HttpGet, Route("salary")]
        public HttpResponseMessage InformationAboutSalary()
        {
            return CatchError(() =>
            {
                return DBStatistikController.MakeDataAboutWorkerSalary();
            }, nameof(SubStringController), nameof(InformationAboutSalary));
        }
    }
}
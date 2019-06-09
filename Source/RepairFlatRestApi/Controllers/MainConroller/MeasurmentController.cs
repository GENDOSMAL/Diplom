using RepairFlatRestApi.Controllers.OtherController;
using RepairFlatRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace RepairFlatRestApi.Controllers.MainConroller
{
    [RoutePrefix("api/Measurment")]
    public class MeasurmentController:BaseController
    {
        [HttpGet, Route("allmeastbl")]
        public HttpResponseMessage SelectDataAbOrderByID([FromUri] Guid idOrder)
        {
            return CatchError(() =>
            {
                return OtherController.MeasurmentDBController.MakeDataAbouMeas(idOrder);
            }, nameof(SubStringController), nameof(SelectDataAbOrderByID));
        }
        [HttpGet, Route("delete")]
        public HttpResponseMessage DeletePremmises([FromUri] Guid idPremises)
        {
            return CatchError(() =>
            {
                return OrderDBController.DeletePremises(idPremises);
            }, nameof(SubStringController), nameof(SelectDataAbOrderByID));
        }

        [HttpGet, Route("infbyid")]
        public HttpResponseMessage SelectMainInfAbMeas([FromUri] Guid idMeas)
        {
            return CatchError(() =>
            {
                return OtherController.MeasurmentDBController.SelectDataAboutMeasurment(idMeas);
            }, nameof(SubStringController), nameof(SelectDataAbOrderByID));
        }
        [HttpPost, Route("create")]
        public HttpResponseMessage MakeDataAboutMeasurment([FromBody] MeasurmentModel.DataAboutMeassFromDB newMeas)
        {
            return CatchError(() =>
            {
                return OtherController.MeasurmentDBController.MakeNewDataAboutMeas(newMeas);
            }, nameof(SubStringController), nameof(SelectDataAbOrderByID));
        }
        [HttpPost, Route("update")]
        public HttpResponseMessage UpdateDataAboutMeas([FromBody] MeasurmentModel.DataAboutMeassFromDB updatedMeas)
        {
            return CatchError(() =>
            {
                return OtherController.MeasurmentDBController.UpdateDataAboutMeas(updatedMeas);
            }, nameof(SubStringController), nameof(SelectDataAbOrderByID));
        }

    }
}
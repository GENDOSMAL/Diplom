using RepairFlatRestApi.Controllers.OtherController;
using System;
using System.Net.Http;
using System.Web.Http;
using static RepairFlatRestApi.Models.DescriptionJSON.DescMakePayment;

namespace RepairFlatRestApi.Controllers.MainConroller
{
    [RoutePrefix("api/payment")]
    public class PaymentController : BaseController
    {
        [HttpGet, Route("getdata")]
        public HttpResponseMessage SelectDataAbOrderByID()
        {
            return CatchError(() =>
            {
                return PaymnetDBController.MakeDataAboutPyment();
            }, nameof(SubStringController), nameof(SelectDataAbOrderByID));
        }
        [HttpGet, Route("getpay")]
        public HttpResponseMessage GetDataById([FromUri] Guid idPayment)
        {
            return CatchError(() =>
            {
                return PaymnetDBController.SelectDataAboutPayment(idPayment);
            }, nameof(SubStringController), nameof(SelectDataAbOrderByID));
        }

        [HttpPost, Route("create/inf")]
        public HttpResponseMessage CreateDataAboutPaymentMethod([FromBody]DataAboutPayment dataAboutPayment)
        {
            return CatchError(() =>
            {
                return PaymnetDBController.CreateDataAboutPaymentMethod(dataAboutPayment);
            }, nameof(SubStringController), nameof(SelectDataAbOrderByID));
        }

        [HttpPost, Route("create/payment")]
        public HttpResponseMessage CreateDataAbPayment([FromBody]MakeDataAboutPayment dataAboutPayment)
        {
            return CatchError(() =>
            {
                return PaymnetDBController.CreateNewPayment(dataAboutPayment);
            }, nameof(SubStringController), nameof(SelectDataAbOrderByID));
        }



    }
}
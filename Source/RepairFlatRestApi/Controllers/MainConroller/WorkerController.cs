using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using static RepairFlatRestApi.Models.WorkWitthWorker;

namespace RepairFlatRestApi.Controllers.MainConroller
{
    [RoutePrefix("api/Worker")]
    public class WorkerController:BaseController
    {
        [HttpGet, Route("getData")]
        public HttpResponseMessage GetDataForRedact([FromUri] Guid idWorker)
        {
            return CatchError(() =>
            {
                return OtherController.WorkerDBConroller.SelectDataAboutClient(idWorker);
            }, nameof(SubStringController), nameof(GetDataAboutWorker));
        }
        [HttpGet, Route("allworker")]
        public HttpResponseMessage GetDataAboutWorker()
        {
            return CatchError(() =>
            {
                return OtherController.WorkerDBConroller.CreateListAllWorker();
            }, nameof(SubStringController), nameof(GetDataAboutWorker));
        }
        [HttpGet, Route("allworkerfororder")]
        public HttpResponseMessage GetDataAboutWorkerThatWork()
        {
            return CatchError(() =>
            {
                return OtherController.WorkerDBConroller.CreateListWorkerForMakeNewWorkOrUpdate(true);
            }, nameof(SubStringController), nameof(GetDataAboutWorkerThatWork));
        }

        [HttpGet, Route("allworkerforredact")]
        public HttpResponseMessage GetDataAboutWorkerForRedact()
        {
            return CatchError(() =>
            {
                return OtherController.WorkerDBConroller.CreateListWorkerForMakeNewWorkOrUpdate();
            }, nameof(SubStringController), nameof(GetDataAboutWorkerThatWork));
        }
        [HttpGet, Route("allworkercanditate")]
        public HttpResponseMessage GetDataAboutWorkerCandidate()
        {
            return CatchError(() =>
            {
                return OtherController.WorkerDBConroller.CreateListAllWorker(true);
            }, nameof(SubStringController), nameof(GetDataAboutWorkerThatWork));
        }

        [HttpPost, Route("createorupdate/worker")]
        public HttpResponseMessage CreateNewCandidate(MakeNewWorker dataAboutNewWorker)
        {
            return CatchError(() =>
            {
                return OtherController.WorkerDBConroller.CreateOrUpdateNewCanditate(dataAboutNewWorker);
            }, nameof(SubStringController), nameof(GetDataAboutWorkerThatWork));
        }
        [HttpPost, Route("createorupdate/postData")]
        public HttpResponseMessage CreateNewDataAboutPost(DataAboutPost DataAboutPost)
        {
            return CatchError(() =>
            {
                return OtherController.WorkerDBConroller.CreateNewPostData(DataAboutPost);
            }, nameof(SubStringController), nameof(GetDataAboutWorkerThatWork));
        }
        [HttpPost, Route("createorupdate/postData")]
        public HttpResponseMessage GiveMoneyToWorker(PayWagesM DataAboutGiveMoney)
        {
            return CatchError(() =>
            {
                return OtherController.WorkerDBConroller.CreateDataAboutPayWages(DataAboutGiveMoney);
            }, nameof(SubStringController), nameof(GetDataAboutWorkerThatWork));
        }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace RepairFlatRestApi.Controllers.MainConroller
{
    [RoutePrefix("api/Contact")]
    public class ContactController:BaseController
    {
        [HttpPost, Route("create")]
        public HttpResponseMessage CreateNewContact([FromBody] Models.ContactModel.InformationAboutContact NewContact)
        {
            return CatchError(() =>
            {
                return OtherController.WorkWithContactDBController.CreaNewContact(NewContact);
            }, nameof(SubStringController), nameof(CreateNewContact));
        }

        [HttpPost, Route("update")]
        public HttpResponseMessage UpdateDataAboutContact([FromBody] Models.ContactModel.InformationAboutContact UpdateContact)
        {
            return CatchError(() =>
            {
                return OtherController.WorkWithContactDBController.UpdateContactData(UpdateContact);
            }, nameof(SubStringController), nameof(UpdateDataAboutContact));
        }

        [HttpGet, Route("getusercontact")]
        public HttpResponseMessage UpdateContact([FromUri] Guid idUser)
        {
            return CatchError(() =>
            {
                return OtherController.WorkWithContactDBController.CreateListOfContactUser(idUser);
            }, nameof(SubStringController), nameof(UpdateContact));
        }
        [HttpGet, Route("delete")]
        public HttpResponseMessage DeleteContact([FromUri] Guid idContact)
        {
            return CatchError(() =>
            {
                return OtherController.WorkWithContactDBController.DeleteContact(idContact);
            }, nameof(SubStringController), nameof(UpdateContact));
        }

        
    }
}
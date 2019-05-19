using RepairFlatRestApi.Models.DescriptionJSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace RepairFlatRestApi.Controllers
{
    [RoutePrefix("api/SubString")]
    public class SubStringController : BaseController
    {

        #region Работа с предоставляемыми услугами
        /// <summary>
        /// Получение новых данных об услугах, как новых, так и обновляемых
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("servises/update")]
        public HttpResponseMessage UpdateServises([FromBody] MakeSubs.MakeUpdateOrInserNew makeUpdateOrInserNew)
        {
            return CatchError(() =>
            {
                return DBController.MakeInserAndUpdateServises(makeUpdateOrInserNew);
            }, nameof(SubStringController), nameof(UpdateServises));
        }

        /// <summary>
        /// Отсылка данных о предоставляемых услугах, которые были обновлены после даты, которую прислал клиент
        /// </summary>
        /// <param name="dateofclientlastupdate">Дата, в которую было последнее обновление</param>
        /// <returns></returns>
        [HttpGet, Route("servises/get")]
        public HttpResponseMessage SendAllUpdateServises([FromUri] string dateofclientlastupdate="")
        {
            return CatchError(() =>
            {                
                return DBController.MakeDataAboutUpdateServises(dateofclientlastupdate);
            }, nameof(LoginController), nameof(SendAllUpdateServises));
        }
        #endregion

        #region Работа с типами помещений
        /// <summary>
        /// Получение новых либо обновленных данных о типах помещений
        /// </summary>
        /// <returns></returns>
        //[HttpPost, Route("~/api/sub/premises/update")]
        //public HttpResponseMessage UpdatePremises()
        //{
        //    return CatchError(() =>
        //    {
        //        throw new NotImplementedException();
        //        //return resLog;
        //    }, nameof(SubStringController), nameof(UpdatePremises));
        //}
        /// <summary>
        /// Отсылка данных об типах помещений, которые были обновлены после даты предоставленной клиентами
        /// </summary>
        /// <param name="dateofclientlastupdate"></param>
        /// <returns></returns>
        [HttpGet, Route("~/api/sub/premises/get")]
        public HttpResponseMessage SendAllUpdatePremises([FromUri] string dateofclientlastupdate = null)
        {
            return CatchError(() =>
            {
                //return DBController.CreateLoginPerson(InformationAboutNewPerson);
                throw new NotImplementedException();
            }, nameof(LoginController), nameof(SendAllUpdatePremises));
        }

        #endregion

        #region Работа с предоставляемыми материалами
        /// <summary>
        /// Обновление либо добавление данных о материалах
        /// </summary>
        /// <returns></returns>
        //[HttpPost, Route("~/api/sub/material/update")]
        //public HttpResponseMessage UpdateMaterial()
        //{
        //    return CatchError(() =>
        //    {
        //        throw new NotImplementedException();
             
        //    }, nameof(SubStringController), nameof(UpdateMaterial));
        //}

        /// <summary>
        /// Отсылание данных о материалах, которые были обновлены после указанной клиентом даты
        /// </summary>
        /// <param name="dateofclientlastupdate"></param>
        /// <returns></returns>
        //[HttpGet, Route("~/api/sub/material/get")]
        //public HttpResponseMessage SendAllUpdateMaterial([FromUri] string dateofclientlastupdate = null)
        //{
        //    return CatchError(() =>
        //    {
        //        throw new NotImplementedException();
        //    }, nameof(LoginController), nameof(SendAllUpdateMaterial));
        //}

        #endregion

        #region Обновление данных о типах контакных данных
        /// <summary>
        /// Обновление либо добавление данных о контактах
        /// </summary>
        /// <returns></returns>
        //[HttpPost, Route("~/api/sub/contact/update")]
        //public HttpResponseMessage UpdateContacts()
        //{
        //    return CatchError(() =>
        //    {
        //        throw new NotImplementedException();

        //    }, nameof(SubStringController), nameof(UpdateContacts));
        //}

        /// <summary>
        /// Отсылание данных о контактах, которые были обновлены после указанной клиентом даты
        /// </summary>
        /// <param name="dateofclientlastupdate"></param>
        ///// <returns></returns>
        //[HttpGet, Route("~/api/sub/contact/get}")]
        //public HttpResponseMessage SendAllUpdateContacts([FromUri] string dateofclientlastupdate = null)
        //{
        //    return CatchError(() =>
        //    {
        //        throw new NotImplementedException();
        //    }, nameof(LoginController), nameof(SendAllUpdateContacts));
        //}
        #endregion

    }
}
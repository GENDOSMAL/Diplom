using RepairFlatRestApi.Models.DescriptionJSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace RepairFlatRestApi.Controllers
{
    public class WorkWithSubstitutableData:BaseController
    {
        #region Работа с предоставляемыми услугами
        /// <summary>
        /// Получение новых данных об услугах, как новых, так и обновляемых
        /// </summary>
        /// <param name="InformationAboutAuth"></param>
        /// <returns></returns>
        [HttpPost, Route("api/main/servises/update")]
        public HttpResponseMessage UpdateServises()
        {
            return CatchError(() =>
            {
                throw new NotImplementedException();
                //return resLog;
            }, nameof(WorkWithSubstitutableData), nameof(UpdateServises));
        }
        /// <summary>
        /// Отсылка данных о предоставляемых услугах, которые были обновлены после даты, которую прислал клиент
        /// </summary>
        /// <param name="dateofclientlastupdate">Дата, в которую было последнее обновление</param>
        /// <returns></returns>
        [HttpGet, Route("api/main/servises/get{dateofclientlastupdate}")]
        public HttpResponseMessage SendAllUpdateServises(string dateofclientlastupdate)
        {
            return CatchError(() =>
            {
                //return DBController.CreateLoginPerson(InformationAboutNewPerson);
                throw new NotImplementedException();
            }, nameof(LoginController), nameof(SendAllUpdateServises));
        }
        #endregion

        #region Работа с типами помещений
        /// <summary>
        /// Получение новых либо обновленных данных о типах помещений
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("api/main/premises/update")]
        public HttpResponseMessage UpdatePremises()
        {
            return CatchError(() =>
            {
                throw new NotImplementedException();
                //return resLog;
            }, nameof(WorkWithSubstitutableData), nameof(UpdatePremises));
        }
        /// <summary>
        /// Отсылка данных об типах помещений, которые были обновлены после даты предоставленной клиентами
        /// </summary>
        /// <param name="dateofclientlastupdate"></param>
        /// <returns></returns>
        [HttpGet, Route("api/main/premises/get{dateofclientlastupdate}")]
        public HttpResponseMessage SendAllUpdatePremises(string dateofclientlastupdate)
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
        [HttpPost, Route("api/main/material/update")]
        public HttpResponseMessage UpdateMaterial()
        {
            return CatchError(() =>
            {
                throw new NotImplementedException();
             
            }, nameof(WorkWithSubstitutableData), nameof(UpdateMaterial));
        }

        /// <summary>
        /// Отсылание данных о материалах, которые были обновлены после указанной клиентом даты
        /// </summary>
        /// <param name="dateofclientlastupdate"></param>
        /// <returns></returns>
        [HttpGet, Route("api/main/material/get{dateofclientlastupdate}")]
        public HttpResponseMessage SendAllUpdateMaterial(string dateofclientlastupdate)
        {
            return CatchError(() =>
            {
                throw new NotImplementedException();
            }, nameof(LoginController), nameof(SendAllUpdateMaterial));
        }

        #endregion

        #region Обновление данных о типах контакных данных
        /// <summary>
        /// Обновление либо добавление данных о контактах
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("api/main/contact/update")]
        public HttpResponseMessage UpdateContacts()
        {
            return CatchError(() =>
            {
                throw new NotImplementedException();

            }, nameof(WorkWithSubstitutableData), nameof(UpdateContacts));
        }

        /// <summary>
        /// Отсылание данных о контактах, которые были обновлены после указанной клиентом даты
        /// </summary>
        /// <param name="dateofclientlastupdate"></param>
        /// <returns></returns>
        [HttpGet, Route("api/main/material/get{dateofclientlastupdate}")]
        public HttpResponseMessage SendAllUpdateContacts(string dateofclientlastupdate)
        {
            return CatchError(() =>
            {
                throw new NotImplementedException();
            }, nameof(LoginController), nameof(SendAllUpdateContacts));
        }
        #endregion

    }
}
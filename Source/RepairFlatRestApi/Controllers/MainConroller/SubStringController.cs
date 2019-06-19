using RepairFlatRestApi.Models.DescriptionJSON;
using System.Net.Http;
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
        public HttpResponseMessage UpdateServises([FromBody] MakeSubs.MakeUpdOrInsServises makeUpdateOrInserNew)
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
        public HttpResponseMessage SendAllUpdateServises([FromUri] string dateofclientlastupdate = "")
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
        [HttpPost, Route("premises/update")]
        public HttpResponseMessage UpdatePremises([FromBody] MakeSubs.MakeUpdOrInsPremises makeUpdateOrInserNew)
        {
            return CatchError(() =>
            {
                return DBController.MakeDataAboutUpdatePremises(makeUpdateOrInserNew);
            }, nameof(SubStringController), nameof(UpdatePremises));
        }
        /// <summary>
        /// Отсылка данных об типах помещений, которые были обновлены после даты предоставленной клиентами
        /// </summary>
        /// <param name="dateofclientlastupdate"></param>
        /// <returns></returns>
        [HttpGet, Route("premises/get")]
        public HttpResponseMessage SendAllUpdatePremises([FromUri] string dateofclientlastupdate = "")
        {
            return CatchError(() =>
            {
                return DBController.GetAllPremises(dateofclientlastupdate);
            }, nameof(LoginController), nameof(SendAllUpdatePremises));
        }

        #endregion

        #region Работа с предоставляемыми материалами
        /// <summary>
        /// Обновление либо добавление данных о материалах
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("material/update")]
        public HttpResponseMessage UpdateMaterial([FromBody]MakeSubs.MakeUpdOrInsMaterials ListOfMaterials)
        {
            return CatchError(() =>
            {
                return DBController.MakeUpdateMaterials(ListOfMaterials);

            }, nameof(SubStringController), nameof(UpdateMaterial));
        }

        /// <summary>
        /// Отсылание данных о материалах, которые были обновлены после указанной клиентом даты
        /// </summary>
        /// <param name="dateofclientlastupdate"></param>
        /// <returns></returns>
        [HttpGet, Route("material/get")]
        public HttpResponseMessage SendAllUpdateMaterial([FromUri] string dateofclientlastupdate = null)
        {
            return CatchError(() =>
            {
                return DBController.MakeListOfMaterials(dateofclientlastupdate);
            }, nameof(LoginController), nameof(SendAllUpdateMaterial));
        }

        #endregion

        #region Обновление данных о типах контакных данных
        [HttpPost, Route("contact/update")]
        public HttpResponseMessage UpdateContacts(MakeSubs.MakeUpdOrInsContacts ListOfContacts)
        {
            return CatchError(() =>
            {
                return DBController.MakeUpdateContacts(ListOfContacts);
            }, nameof(SubStringController), nameof(UpdateContacts));
        }

        [HttpGet, Route("contact/get")]
        public HttpResponseMessage SendAllUpdateContacts([FromUri] string dateofclientlastupdate = null)
        {
            return CatchError(() =>
            {
                return DBController.MakeListOfContacts(dateofclientlastupdate);
            }, nameof(LoginController), nameof(SendAllUpdateContacts));
        }
        [HttpPost, Route("post/update")]
        public HttpResponseMessage UpdatePost(MakeSubs.MakeUpdOrInsPost ListOfPost)
        {
            return CatchError(() =>
            {
                return DBController.MakeUpdatePost(ListOfPost);
            }, nameof(SubStringController), nameof(UpdateContacts));
        }

        /// <summary>
        /// Отсылание данных о контактах, которые были обновлены после указанной клиентом даты
        /// </summary>
        /// <param name="dateofclientlastupdate"></param>
        ///// <returns></returns>
        [HttpGet, Route("post/get")]
        public HttpResponseMessage SendAllUpdatePost([FromUri] string dateofclientlastupdate = null)
        {
            return CatchError(() =>
            {
                return DBController.MakeDataAboutPost(dateofclientlastupdate);
            }, nameof(LoginController), nameof(SendAllUpdateContacts));
        }
        #endregion

    }
}
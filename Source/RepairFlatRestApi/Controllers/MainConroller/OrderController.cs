using RepairFlatRestApi.Models.DescriptionJSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace RepairFlatRestApi.Controllers.MainConroller
{
    [RoutePrefix("api/Order")]
    public class OrderController:BaseController
    {
        #region Контроллеры самого заказа
        [HttpGet, Route("allorders")]
        public HttpResponseMessage SelectOrderByID([FromUri] Guid idOrder)
        {
            return CatchError(() =>
            {
                return DBController.SelectAllDataAboutOrder(idOrder);
            }, nameof(SubStringController), nameof(SelectOrderByID));
        }

        [HttpGet, Route("allorders")]
        public HttpResponseMessage SelectAllOrders()
        {
            return CatchError(() =>
            {
                return DBController.MakeDataAboutAllOrder();
            }, nameof(SubStringController), nameof(SelectAllOrders));
        }
        [HttpPost, Route("create")]
        public HttpResponseMessage CreateNewOrders([FromBody] WorkWithOrder.BaseOrderInformation NewOrderData)
        {
            return CatchError(() =>
            {
                return DBController.CreateNewOrder(NewOrderData);
            }, nameof(SubStringController), nameof(SelectAllOrders));
        }
        [HttpPost, Route("update/order")]
        public HttpResponseMessage UpdateOrderInformation([FromBody] WorkWithOrder.BaseOrderInformation UpdateDataAbOrder)
        {
            return CatchError(() =>
            {
                return DBController.UpdateDataAboutOrder(UpdateDataAbOrder);
            }, nameof(SubStringController), nameof(SelectAllOrders));
        }
        #endregion

        #region Контроллеры данных об услугах
        [HttpPost, Route("create/servis")]
        public HttpResponseMessage CreateNewServis([FromBody] WorkWithOrder.DataAboutServis NewServisData)
        {
            return CatchError(() =>
            {
                return DBController.CreateNewServis(NewServisData);
            }, nameof(SubStringController), nameof(SelectAllOrders));
        }
        #endregion 
    }
}
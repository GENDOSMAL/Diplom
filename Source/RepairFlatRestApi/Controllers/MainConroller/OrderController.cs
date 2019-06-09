﻿using RepairFlatRestApi.Controllers.OtherController;
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

        [HttpGet, Route("alldata")]
        public HttpResponseMessage SelectDataAbOrderByID([FromUri] Guid idOrder)
        {
            return CatchError(() =>
            {
                return DBController.SelectAllDataAboutOrder(idOrder);
            }, nameof(SubStringController), nameof(SelectDataAbOrderByID));
        }

        [HttpGet, Route("allorders")]
        public HttpResponseMessage SelectAllOrder()
        {
            return CatchError(() =>
            {
                return OtherController.OrderDBController.SelectDataAboutAllOrder();
            }, nameof(SubStringController), nameof(SelectAllOrder));
        }
        [HttpGet, Route("dataforupdate")]
        public HttpResponseMessage GetDataAboutOrderToUpdate([FromUri] Guid idOrder)
        {
            return CatchError(() =>
            {
                return OtherController.OrderDBController.SelectDataForUpdate(idOrder);
            }, nameof(SubStringController), nameof(GetDataAboutOrderToUpdate));
        }

        [HttpPost, Route("create")]
        public HttpResponseMessage CreateNewOrders([FromBody] WorkWithOrder.BaseOrderInformation NewOrderData)
        {
            return CatchError(() =>
            {
                return DBController.CreateNewOrder(NewOrderData);
            }, nameof(SubStringController), nameof(CreateNewOrders));
        }
        [HttpPost, Route("update")]
        public HttpResponseMessage UpdateOrderInformation([FromBody] WorkWithOrder.BaseOrderInformation UpdateDataAbOrder)
        {
            return CatchError(() =>
            {
                return DBController.UpdateDataAboutOrder(UpdateDataAbOrder);
            }, nameof(SubStringController), nameof(UpdateOrderInformation));
        }
        #endregion

        #region Контроллер оплаты
        [HttpGet, Route("get/payment")]
        public HttpResponseMessage SelectDataAbOrderPaymentByIDorder([FromUri] Guid idOrder)
        {
            return CatchError(() =>
            {
                return OrderDBController.MakeDataAboutAllOrderPayment(idOrder);
            }, nameof(SubStringController), nameof(SelectDataAbOrderPaymentByIDorder));
        }

        #endregion





        #region Работа с заданиями
        [HttpGet, Route("get/task")]
        public HttpResponseMessage SelectDataAbOrderTaskByIDOrder([FromUri] Guid idOrder)
        {
            return CatchError(() =>
            {
                return OrderDBController.MakeDataAboutAllOrderTask(idOrder);
            }, nameof(SubStringController), nameof(SelectDataAbOrderTaskByIDOrder));
        }

        [HttpPost, Route("create/task")]
        public HttpResponseMessage CreateTaskInOrder([FromBody] WorkWithOrder.InformationAboutTask DataAboutTask)
        {
            return CatchError(() =>
            {
                return OrderDBController.CreateNewTask(DataAboutTask);
            }, nameof(SubStringController), nameof(SelectDataAbOrderTaskByIDOrder));
        }

        [HttpGet, Route("get/task")]
        public HttpResponseMessage CreateTaskInOrder([FromUri] Guid idTask)
        {
            return CatchError(() =>
            {
                return OrderDBController.GetDataAboutTask(idTask);
            }, nameof(SubStringController), nameof(SelectDataAbOrderTaskByIDOrder));
        }

        [HttpGet, Route("delete/task")]
        public HttpResponseMessage DeleteTask([FromUri] Guid idTask,[FromUri] Guid idOrder)
        {
            return CatchError(() =>
            {
                return OrderDBController.DeleteTask(idTask,idOrder);
            }, nameof(SubStringController), nameof(SelectDataAbOrderTaskByIDOrder));
        }
        #endregion
    }
}
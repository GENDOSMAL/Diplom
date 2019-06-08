﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static RepairFlatRestApi.Models.DescriptionJSON.WorkWithOrder;
using static RepairFlatRestApi.Models.OrderData;

namespace RepairFlatRestApi.Controllers.OtherController
{
    public class OrderDBController : DBController
    {
        internal static object SelectDataAboutAllOrder()
        {
            return Run((db) =>
            {
                AllOrder allOrder = new AllOrder();
                allOrder.listOfOrders = new List<AllDataAboutOrder>();
                var ListOfOrder = db.OrderInformation.AsEnumerable();
                foreach (var order in ListOfOrder)
                {
                    string female = order.ClientDetails.User.Female == 1 ? "МУЖ" : "Жен";
                    AllDataAboutOrder allDataAbout = new AllDataAboutOrder();
                    allDataAbout.idOrder = order.IdOrder;
                    allDataAbout.DataStart = order.DateStart;
                    allDataAbout.Status = order.Status;
                    allDataAbout.Desc = order.Description?.Trim();
                    allDataAbout.DataAboutAdress = $"{order.AdressDescription.CiryName?.Trim()} {order.AdressDescription.Street?.Trim()} {order.AdressDescription.House?.Trim()} {order.AdressDescription.Entrance?.Trim()} {order.AdressDescription.NumberOfDelen?.Trim()}";
                    allDataAbout.ContactData = $"{order.UserContact.TypeOfContact.Value.Trim()} : {order.UserContact.Value.Trim()}";
                    allDataAbout.AllSumma = order.AllSumma;
                    allDataAbout.FIOClient = $"{order.ClientDetails.User.LastName?.Trim()} {order.ClientDetails.User.Name?.Substring(0, 1).ToUpper()}.{order.ClientDetails.User.Patronymic?.Substring(0, 1).ToUpper()}. {order.ClientDetails.User.BirstDay.Value.ToString("dd.MM.yyyy") } {female}";

                    allOrder.listOfOrders.Add(allDataAbout);

                }
                allOrder.success = true;
                return allOrder;
            });
        }

        internal static object SelectDataForUpdate(Guid idOrder)
        {
            return Run((db) =>
            {
                var selectedOrder = db.OrderInformation.Where(ee => ee.IdOrder == idOrder).FirstOrDefault();
                string female = selectedOrder.ClientDetails.User.Female == 1 ? "МУЖ" : "Жен";
                return new Models.OrderData.DataForUpdate
                {
                    AllSumma = selectedOrder.AllSumma,
                    idOrder = selectedOrder.IdOrder,
                    idAdress = selectedOrder.IdAdress,
                    idContact = selectedOrder.MainContactID,
                    Desc = selectedOrder.Description?.Trim(),
                    idUser = selectedOrder.idClient,
                    ContactData = $"{selectedOrder.UserContact.TypeOfContact.Value.Trim()} : {selectedOrder.UserContact.Value.Trim()}",
                    FIOClient = $"{selectedOrder.ClientDetails.User.LastName?.Trim()} {selectedOrder.ClientDetails.User.Name?.Substring(0, 1).ToUpper()}.{selectedOrder.ClientDetails.User.Patronymic?.Substring(0, 1).ToUpper()}. {selectedOrder.ClientDetails.User.BirstDay.Value.ToString("dd.MM.yyyy") } {female}",
                    DataStart = selectedOrder.DateStart,
                    DataAboutAdress = $"{selectedOrder.AdressDescription.CiryName?.Trim()} {selectedOrder.AdressDescription.Street?.Trim()} {selectedOrder.AdressDescription.House?.Trim()} {selectedOrder.AdressDescription.Entrance?.Trim()} {selectedOrder.AdressDescription.NumberOfDelen?.Trim()}",
                    Status = selectedOrder.Status
                };
            });
        }

        internal static object MakeDataAboutAllOrderTask(Guid idOrder)
        {
            return Run((db) =>
            {
                var DataAboutTasks = db.OrderTasks.Where(e => e.IdOrder == idOrder).AsEnumerable();
                if (DataAboutTasks.Any())
                {
                    DataAboutTaskInOrder TasksData = new DataAboutTaskInOrder();
                    TasksData.InfTask = new List<TaskInf>();
                    foreach(var Task in DataAboutTasks)
                    {
                        TaskInf taskInf = new TaskInf();
                        taskInf.DateEnd = Task.DeadEnd;
                        taskInf.DateStart = Task.DateStart;
                        taskInf.Description = Task.Description;
                        taskInf.idTask = Task.IdTask;
                        taskInf.Summa = Task.SummaAboutTask;
                        TasksData.InfTask.Add(taskInf);
                    }
                    TasksData.success = true;
                    return TasksData;
                }
                else
                {
                    return new DataAboutTaskInOrder { success = false };
                }

            });
        }

        internal static object MakeDataAboutAllOrderPayment(Guid idOrder)
        {
            return Run((db) =>
            {
                var DataAboutAllPayment = db.OrderPayment.Where(e => e.IdOrder == idOrder).AsEnumerable();
                if (DataAboutAllPayment.Any())
                {
                    DataAboutPaymentInOrder dataAboutPaymentInOrder = new DataAboutPaymentInOrder();
                    dataAboutPaymentInOrder.InfPayment = new List<PaymentInf>();
                    foreach (var Payment in DataAboutAllPayment)
                    {
                        PaymentInf inf = new PaymentInf();
                        inf.Summa = Payment.Summa;
                        inf.Description = Payment.Description;
                        inf.idPayment = Payment.IdPayment;
                        inf.FioMake = $"{Payment.WorkerDetails.User.LastName?.Trim()} {Payment.WorkerDetails.User.Name?.Substring(0, 1)}.{Payment.WorkerDetails.User.Patronymic?.Substring(0, 1)}";
                        inf.DateOfMake = Payment.DateOfDoc;
                        dataAboutPaymentInOrder.InfPayment.Add(inf);
                    }
                    dataAboutPaymentInOrder.success = true;
                    return dataAboutPaymentInOrder;
                }
                else
                {
                    return new DataAboutPaymentInOrder { success = false };
                }


            });
        }
    }
}
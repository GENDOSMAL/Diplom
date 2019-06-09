using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using RepairFlatRestApi.Models.DescriptionJSON;
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

        internal static object DeletePremises(Guid idPremises)
        {
            return Run((db) =>
            {
                try
                {
                    var dat = db.OrderMeasurements.Where(ee => ee.idMeasurements == idPremises).First();
                    if (dat != null)
                    {
                        db.OrderMeasurements.Remove(dat);
                    }
                    var dd = db.OrderElementOfMeasurments.Where(ee => ee.idMeasurements == idPremises).AsEnumerable();
                    if (dd != null)
                    {
                        if (dd.Any())
                        {
                            db.OrderElementOfMeasurments.RemoveRange(dd);
                        }
                    }
                    db.SaveChanges();
                    return new BaseResult { success = true };

                }
                catch (Exception ex)
                {
                    return new BaseResult { success = false, description = ex.ToString() };
                }
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

        internal static object DeleteTask(Guid idTask,Guid idOrder)
        {
            return Run((db) =>
            {
                try
                {
                    var task = db.OrderTasks.Where(ee => ee.IdTask == idTask).First();
                    if (task != null)
                    {
                        db.OrderTasks.Remove(task);
                        var mat = db.TaskMaterials.Where(ee => ee.idTask == idTask).AsEnumerable();
                        if (mat != null)
                        {
                            if (mat.Any())
                            {
                                db.TaskMaterials.RemoveRange(mat);
                            }
                        }
                        var Serv = db.TaskServis.Where(ee => ee.idTask == idTask).AsEnumerable();
                        if (Serv != null)
                        {
                            if (Serv.Any())
                            {
                                db.TaskServis.RemoveRange(Serv);
                            }
                        }
                        var Work = db.TaskWorker.Where(ee => ee.idTask == idTask).AsEnumerable();
                        if (Work != null)
                        {
                            if (Work.Any())
                            {
                                db.TaskWorker.RemoveRange(Work);
                            }
                        }
                        db.SaveChanges();
                        decimal summa = db.OrderTasks.Where(ee => ee.IdOrder == idOrder).Sum(ee => ee.SummaAboutTask) ?? default;
                        decimal Opl = db.OrderPayment.Where(ee => ee.IdOrder == idOrder).Sum(ee => ee.Summa) ?? default;
                        decimal Needd = summa - Opl;
                        return new SummaOfOrder { success = true, IdOrder = idOrder, summaOfOrder = summa, NeedPay = Needd };
                    }
                    else
                    {
                        return new SummaOfOrder { success = false};
                    }
                }
                catch(Exception ex)
                {
                    return new SummaOfOrder { success = false, description = ex.ToString() };
                }
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
                    foreach (var Task in DataAboutTasks)
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
                    decimal summa = db.OrderTasks.Where(ee => ee.IdOrder == idOrder).Sum(ee => ee.SummaAboutTask) ?? default;
                    decimal Opl = db.OrderPayment.Where(ee => ee.IdOrder == idOrder).Sum(ee => ee.Summa) ?? default;
                    decimal Needd = summa - Opl;
                    TasksData.Ostatok = Needd;
                    return TasksData;
                }
                else
                {
                    return new DataAboutTaskInOrder { success = false };
                }

            });
        }

        internal static object GetDataAboutTask(Guid idTask)
        {
            return Run((db) =>
            {
                var data = db.OrderTasks.Where(ee => ee.IdTask == idTask).FirstOrDefault();
                if (data != null)
                {
                    InformationAboutTask aboutTask = new InformationAboutTask();
                    aboutTask.success = true;
                    aboutTask.idOrder = data.IdOrder ?? default;
                    aboutTask.idTask = data.IdTask;
                    aboutTask.DateEnd = data.DeadEnd;
                    aboutTask.DateStart = data.DateStart;
                    aboutTask.Description = data.Description;
                    aboutTask.Summa = data.SummaAboutTask;

                    if (data.TaskMaterials != null)
                    {
                        if (data.TaskMaterials.Any())
                        {
                            aboutTask.InfAbMat = new MaterialInfTask();
                            aboutTask.InfAbMat.materialsInf = new List<TaskMaterial>();
                            foreach (var Mat in data.TaskMaterials)
                            {
                                TaskMaterial taskMaterial = new TaskMaterial
                                {
                                    cost = Mat.Cost,
                                    count = Mat.Count,
                                    idMaterial = Mat.idMaterial ?? default,
                                    idTaskMaterial = Mat.idMaterial ?? default,
                                    NameOfMaterials = Mat.OurMaterials.NameOfMaterial,
                                };
                                aboutTask.InfAbMat.materialsInf.Add(taskMaterial);
                            }
                        }
                    }

                    if (data.TaskServis != null)
                    {
                        if (data.TaskServis.Any())
                        {
                            aboutTask.InfAbServ = new ServisesInfTask();
                            aboutTask.InfAbServ.ServisInf = new List<TaskServises>();
                            foreach (var Serv in data.TaskServis)
                            {
                                TaskServises taskServis = new TaskServises
                                {
                                    cost = Serv.Cost,
                                    count = Serv.Count,
                                    idServis = Serv.idServis ?? default,
                                    idTaskServises = Serv.IdTaskServises,
                                    NameOfServises = Serv.OurServices.Nomination,
                                };
                                aboutTask.InfAbServ.ServisInf.Add(taskServis);
                            }
                        }
                    }
                    if (data.TaskWorker != null)
                    {
                        if (data.TaskWorker.Any())
                        {
                            aboutTask.InfAbWorkers = new WorkersInfTask();
                            aboutTask.InfAbWorkers.WorkerInf = new List<TaskWorker>();
                            foreach (var Worker in data.TaskWorker)
                            {
                                TaskWorker TaskWorker = new TaskWorker
                                {
                                    FioOfWorker = $"{Worker.User.LastName?.Trim()} {Worker.User.Name?.Substring(0, 1)}.{Worker.User.Patronymic?.Substring(0, 1)}",
                                    idTaskWorker = Worker.idCmbination,
                                    idWorker = Worker.idWorker ?? default,
                                    NameOfPost = Worker.User.WorkerDetails.EstabilismentPost.FirstOrDefault().WorkerPosts.NameOfPost,
                                    Role = Worker.Role
                                };
                                aboutTask.InfAbWorkers.WorkerInf.Add(TaskWorker);
                            }
                        }
                    }
                    return aboutTask;

                }
                else
                {
                    return new InformationAboutTask
                    {
                        success = false,
                    };
                }

            });
        }

        internal static object CreateNewTask(InformationAboutTask dataAboutTask)
        {
            return Run((db) =>
            {
                try
                {
                    db.OrderTasks.AddOrUpdate(new Models.OrderTasks
                    {
                        DateStart = dataAboutTask.DateStart,
                        DeadEnd = dataAboutTask.DateEnd,
                        Description = dataAboutTask.Description,
                        IdOrder = dataAboutTask.idOrder,
                        IdTask = dataAboutTask.idTask,
                        SummaAboutTask = dataAboutTask.Summa
                    });
                    if (dataAboutTask.InfAbServ.ServisInf != null)
                        if (dataAboutTask.InfAbServ.ServisInf.Any())
                        {
                            foreach (var servInf in dataAboutTask.InfAbServ.ServisInf)
                            {
                                db.TaskServis.AddOrUpdate(new Models.TaskServis
                                {
                                    idTask = dataAboutTask.idTask,
                                    Cost = servInf.cost,
                                    Count = servInf.count,
                                    idServis = servInf.idServis,
                                    IdTaskServises = servInf.idTaskServises
                                });
                            }
                        }
                    if (dataAboutTask.InfAbServ.DeleteServises != null)

                        if (dataAboutTask.InfAbServ.DeleteServises.Any())
                        {
                            foreach (var servInf in dataAboutTask.InfAbServ.DeleteServises)
                            {
                                var ss = db.TaskServis.Where(ee => ee.IdTaskServises == servInf).First();
                                if (ss != null)
                                    db.TaskServis.Remove(ss);
                            }
                        }
                    if (dataAboutTask.InfAbMat.materialsInf != null)

                        if (dataAboutTask.InfAbMat.materialsInf.Any())
                        {
                            foreach (var MatInf in dataAboutTask.InfAbMat.materialsInf)
                            {
                                db.TaskMaterials.AddOrUpdate(new Models.TaskMaterials
                                {
                                    Cost = MatInf.cost,
                                    idMaterial = MatInf.idMaterial,
                                    idTask = dataAboutTask.idTask,
                                    idTaskMaterials = MatInf.idTaskMaterial,
                                    Count = MatInf.count,
                                });
                            }
                        }
                    if (dataAboutTask.InfAbMat.DeleteMaterials != null)
                        if (dataAboutTask.InfAbMat.DeleteMaterials.Any())
                        {
                            foreach (var MatInf in dataAboutTask.InfAbMat.DeleteMaterials)
                            {
                                var ss = db.TaskMaterials.Where(ee => ee.idTaskMaterials == MatInf).First();
                                if (ss != null)
                                    db.TaskMaterials.Remove(ss);
                            }
                        }

                    if (dataAboutTask.InfAbWorkers.WorkerInf != null)
                        if (dataAboutTask.InfAbWorkers.WorkerInf.Any())
                        {
                            foreach (var WorkInf in dataAboutTask.InfAbWorkers.WorkerInf)
                            {
                                db.TaskWorker.AddOrUpdate(new Models.TaskWorker
                                {
                                    idTask = dataAboutTask.idTask,
                                    idCmbination = WorkInf.idTaskWorker,
                                    idWorker = WorkInf.idWorker,
                                    Role = WorkInf.Role
                                });
                            }
                        }
                    if (dataAboutTask.InfAbWorkers.DeleteWorkers != null)
                        if (dataAboutTask.InfAbWorkers.DeleteWorkers.Any())
                        {
                            foreach (var WorkerInf in dataAboutTask.InfAbWorkers.DeleteWorkers)
                            {
                                var ss = db.TaskWorker.Where(ee => ee.idCmbination == WorkerInf).First();
                                if (ss != null)
                                    db.TaskWorker.Remove(ss);
                            }
                        }
                    db.SaveChanges();
                    decimal summa = db.OrderTasks.Where(ee => ee.IdOrder == dataAboutTask.idOrder).Sum(ee => ee.SummaAboutTask) ?? default;
                    decimal Opl = db.OrderPayment.Where(ee => ee.IdOrder == dataAboutTask.idOrder).Sum(ee => ee.Summa) ?? default;
                    decimal Needd = summa - Opl;
                    db.OrderInformation.Where(ee => ee.IdOrder == dataAboutTask.idOrder).First().AllSumma = summa;
                    db.SaveChanges();
                    return new SummaOfOrder { IdOrder = dataAboutTask.idOrder, summaOfOrder = summa, success = true, NeedPay = Needd };
                }
                catch (Exception ex)
                {
                    string ex1 = ex.ToString();
                    return new SummaOfOrder
                    {
                        success = false,
                        description = ex.ToString()
                    };
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
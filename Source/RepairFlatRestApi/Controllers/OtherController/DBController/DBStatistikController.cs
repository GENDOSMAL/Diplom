using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static RepairFlatRestApi.Models.DescriptionJSON.StatModel;

namespace RepairFlatRestApi.Controllers.OtherController.DBController
{
    public class DBStatistikController : DBBaseController
    {
        internal static object MakeDataAboutWorkerSalary()
        {
            return Run((db) => 
            {
                var DataAboutSalary = db.WorkersPayGive.AsEnumerable();
                if (DataAboutSalary != null)
                {
                    if (DataAboutSalary.Any())
                    {
                        DataAboutWorkerPayment dataAboutWorkerPayment = new DataAboutWorkerPayment();
                        dataAboutWorkerPayment.InformationAboutWorker = new List<DataAboutWorkerSalary>();
                        double summa=0;
                        foreach(var SalaryWorker in DataAboutSalary)
                        {
                            DataAboutWorkerSalary workerSalary = new DataAboutWorkerSalary();
                            workerSalary.DateOfOperation = SalaryWorker.Data;
                            workerSalary.FIOWorker = $"{SalaryWorker.WorkerDetails.User.LastName?.Trim()} {SalaryWorker.WorkerDetails.User.Name?.Substring(0, 1).ToUpper()}.{SalaryWorker.WorkerDetails.User.Patronymic?.Substring(0, 1).ToUpper()}. {SalaryWorker.WorkerDetails.User.BirstDay.Value.ToString("dd.MM.yyyy") }"; ;
                            workerSalary.NameOfPost = SalaryWorker.WorkerDetails.WorkersOperats.FirstOrDefault().EstabilismentPost.WorkerPosts.NameOfPost?.Trim();
                            workerSalary.SalaryOfWork = SalaryWorker.Size;
                            summa += Convert.ToDouble(SalaryWorker.Size);
                            dataAboutWorkerPayment.InformationAboutWorker.Add(workerSalary);
                        }

                        dataAboutWorkerPayment.success = true;
                        dataAboutWorkerPayment.Summa = summa;
                        return dataAboutWorkerPayment;
                    }
                    else
                    {
                        return new DataAboutWorkerPayment { success = false ,description="Нет данных о выплатах!"};
                    }
                }
                else
                {
                    return new DataAboutWorkerPayment { success = false, description = "Нет данных о выплатах!" };
                }
            });
        }

        internal static object MakeListOfOrderPayment()
        {
            return Run((db) =>
            {
                var DataAboutPayment = db.OrderPayment.AsEnumerable();
                if (DataAboutPayment != null)
                {
                    if (DataAboutPayment.Any())
                    {
                        ListOfDataAboOrderPayment dataAboutOrderPayment = new ListOfDataAboOrderPayment();
                        dataAboutOrderPayment.InformationAboutOrderPay = new List<DataAboutOrderPay>();
                        double summa = 0;
                        foreach (var PaymentInf in DataAboutPayment)
                        {
                            DataAboutOrderPay OrderPayment = new DataAboutOrderPay();
                            OrderPayment.DateOfMake = PaymentInf.DateOfDoc?? default;
                            OrderPayment.FIOClient = $"{PaymentInf.OrderInformation.ClientDetails.User.LastName?.Trim()} {PaymentInf.OrderInformation.ClientDetails.User.Name?.Substring(0, 1).ToUpper()}.{PaymentInf.OrderInformation.ClientDetails.User.Patronymic?.Substring(0, 1).ToUpper()}. "; 
                            OrderPayment.FIOOfWorker = $"{PaymentInf.OrderInformation.WorkerDetails.User.LastName?.Trim()} {PaymentInf.OrderInformation.WorkerDetails.User.Name?.Substring(0, 1).ToUpper()}.{PaymentInf.OrderInformation.WorkerDetails.User.Patronymic?.Substring(0, 1).ToUpper()}. ";
                            OrderPayment.Summa = PaymentInf.Summa ?? default;
                            OrderPayment.Desc = PaymentInf.Description?.Trim();
                            summa += Convert.ToDouble(PaymentInf.Summa ?? default);
                            dataAboutOrderPayment.InformationAboutOrderPay.Add(OrderPayment);
                        }
                        dataAboutOrderPayment.success = true;
                        dataAboutOrderPayment.Summa = summa;
                        return dataAboutOrderPayment;
                    }
                    else
                    {
                        return new ListOfDataAboOrderPayment { success = false, description = "Нет данных о оплатах!" };
                    }
                }
                else
                {
                    return new ListOfDataAboOrderPayment { success = false, description = "Нет данных об оплатах!" };
                }
            });
        }
    }
}
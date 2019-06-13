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
                var DataAboutSalary = db.WorkersPayGive.Where(ee => ee.idGive != new Guid()).AsEnumerable();
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
    }
}
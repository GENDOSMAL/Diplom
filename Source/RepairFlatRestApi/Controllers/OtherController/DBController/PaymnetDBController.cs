using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using RepairFlatRestApi.Models.DescriptionJSON;
using static RepairFlatRestApi.Models.DescriptionJSON.DescMakePayment;

namespace RepairFlatRestApi.Controllers.OtherController
{
    public class PaymnetDBController : DBController
    {
        internal static object MakeDataAboutPyment()
        {
            return Run((db) =>
            {
                var inf = db.InformatioForPayment.OrderBy(i => i.DateOfInsert).FirstOrDefault();
                if (inf != null)
                {

                    DataAboutPayment dataAboutPayment = new DataAboutPayment
                    {
                        BankOfPayment = inf.BankOfPayment?.Trim(),
                        BIK = inf.BIK,
                        CheckingAcount = inf.CheckingAcount,
                        idInfPayment = inf.idInfPayment,
                        idWorkerMake = inf.idWorkerMake,
                        InnOfOrganization = inf.InnOfOrganization?.Trim(),
                        KppOfOrganization = inf.KppOfOrganization?.Trim(),
                        NameOfRecipient = inf.NameOfRecipient?.Trim(),
                        NameOfWorkerMake = $"{inf.WorkerDetails.User.LastName?.Trim()} {inf.WorkerDetails.User.Name?.Substring(0, 1)}.{inf.WorkerDetails.User.Patronymic?.Substring(0, 1)}.",
                        YIN = inf.YIN,
                        success = true,
                        DateOfMake = inf.DateOfInsert,
                    };
                    return dataAboutPayment;
                }
                else
                {
                    return new DataAboutPayment
                    {
                        success = false,
                    };
                }
            });
        }

        internal static object SelectDataAboutPayment(Guid idPayment)
        {
            return Run((db) =>
            {
                try
                {
                    var dd = db.OrderPayment.Where(ee => ee.IdPayment == idPayment).FirstOrDefault();
                    if (dd != null)
                    {
                        return new MakeDataAboutPayment
                        {
                            DateOfDoc = dd.DateOfDoc,
                            summa = dd.Summa,
                            idWorkerMake = dd.IdWorkerMake,
                            idPayment = dd.IdPayment,
                            idOrder = dd.IdOrder,
                            Desc = dd.Description,
                            idInfForPayment = dd.idInformatioForPayment,
                            success = true
                        };
                    }
                    else
                    {
                        return new MakeDataAboutPayment { success = false };
                    }                    
                }
                catch (Exception ex)
                {
                    return new MakeDataAboutPayment { success = false, description = ex.ToString() };
                }
            });
        }

        internal static object CreateNewPayment(MakeDataAboutPayment dataAboutPayment)
        {
            return Run((db) =>
            {
                try
                {
                    db.OrderPayment.AddOrUpdate(new Models.OrderPayment
                    {
                        DateOfDoc = dataAboutPayment.DateOfDoc,
                        Description = dataAboutPayment.Desc,
                        idInformatioForPayment = dataAboutPayment.idInfForPayment,
                        IdOrder = dataAboutPayment.idOrder,
                        IdPayment = dataAboutPayment.idPayment,
                        IdWorkerMake = dataAboutPayment.idWorkerMake,
                        Summa = dataAboutPayment.summa

                    });
                    db.SaveChanges();
                    return new BaseResult { success = true };
                }
                catch (Exception ex)
                {
                    return new BaseResult { success = false, description = ex.ToString() };
                }
            });

        }

        internal static object CreateDataAboutPaymentMethod(DataAboutPayment dataAboutPayment)
        {
            return Run((db) =>
            {
                Models.InformatioForPayment informatioForPayment = new Models.InformatioForPayment
                {
                    DateOfInsert = dataAboutPayment.DateOfMake,
                    BankOfPayment = dataAboutPayment.BankOfPayment,
                    BIK = dataAboutPayment.BIK,
                    CheckingAcount = dataAboutPayment.CheckingAcount,
                    idInfPayment = dataAboutPayment.idInfPayment,
                    idWorkerMake = dataAboutPayment.idWorkerMake,
                    InnOfOrganization = dataAboutPayment.InnOfOrganization,
                    KppOfOrganization = dataAboutPayment.KppOfOrganization,
                    NameOfRecipient = dataAboutPayment.NameOfRecipient,
                    YIN = dataAboutPayment.YIN
                };
                db.InformatioForPayment.AddOrUpdate(informatioForPayment);
                db.SaveChanges();
                return new BaseResult { success = true };
            });
        }
    }
}
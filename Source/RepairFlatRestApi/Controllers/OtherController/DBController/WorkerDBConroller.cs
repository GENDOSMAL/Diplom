using RepairFlatRestApi.Models;
using RepairFlatRestApi.Models.DescriptionJSON;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using static RepairFlatRestApi.Models.WorkWitthWorker;

namespace RepairFlatRestApi.Controllers.OtherController
{
    internal class WorkerDBConroller : DBController
    {
        internal static object CreateListAllWorker(bool canditate = false)
        {
            return Run((db) =>
            {
                IEnumerable<User> QueryOfListOfWorkers = default;
                if (canditate)
                {//Если нужно выбрать кандидатов
                    QueryOfListOfWorkers = db.User.Where(ee => ee.TypeOfUser == SomeEnums.TypeOfUser.KD.ToString()).AsEnumerable();
                }
                else
                {//Если всех рабочих для редактирования данных
                    QueryOfListOfWorkers = db.User.Where(ee => ee.TypeOfUser != SomeEnums.TypeOfUser.Cl.ToString()).AsEnumerable();
                }

                if (QueryOfListOfWorkers.Any())
                {
                    ListOfAllWorkers listOfAllWorkers = new ListOfAllWorkers();
                    listOfAllWorkers.Workers = new List<DescriptionOfWorker>();
                    foreach (var workerOfWor in QueryOfListOfWorkers)
                    {
                        DescriptionOfWorker descriptionOfWorker = new DescriptionOfWorker();
                        string female = "";
                        if (workerOfWor.Female.HasValue)
                        {
                            female = SomeEnums.FemaleType[workerOfWor.Female ?? default];
                        }
                        descriptionOfWorker.DateRozd = workerOfWor.BirstDay.Value.ToString("dd.MM.yyyy HH:mm");
                        descriptionOfWorker.Female = female;
                        descriptionOfWorker.idUser = workerOfWor.idUser;
                        descriptionOfWorker.LastName = workerOfWor.LastName?.Trim();
                        descriptionOfWorker.Name = workerOfWor.Name?.Trim();
                        descriptionOfWorker.Patronymic = workerOfWor.Patronymic?.Trim();
                        descriptionOfWorker.TypeOfUser = workerOfWor.TypeOfUser?.Trim();
                        listOfAllWorkers.Workers.Add(descriptionOfWorker);
                    }
                    listOfAllWorkers.success = true;
                    return listOfAllWorkers;
                }
                else
                {
                    return new ListOfAllWorkers { success = false };
                }
            });
        }

        internal static object SelectDataAboutClient(Guid idWorker)
        {
            return Run((db) =>
            {
                var user = db.User.Where(ee => ee.idUser == idWorker).FirstOrDefault();
                var worker = new MakeNewWorker();


                worker.idUser = user.idUser;
                worker.Birstday = user.BirstDay;
                if (user.WorkerDetails != null)
                {
                    worker.DescOfAdress = $"{user.WorkerDetails.AdressDescription.CiryName?.Trim()} {user.WorkerDetails.AdressDescription.Street?.Trim()} {user.WorkerDetails.AdressDescription.House?.Trim()} {user.WorkerDetails.AdressDescription.Entrance?.Trim()} {user.WorkerDetails.AdressDescription.NumberOfDelen?.Trim()}";
                    worker.idAdress = user.WorkerDetails.idAdress ?? default;
                }
                worker.Female = user.Female;
                worker.Lastname = user.LastName?.Trim();
                worker.Name = user.Name?.Trim();
                worker.Pasport = user.Pasport?.Trim();
                worker.Patronymic = user.Patronymic?.Trim();
                worker.TypeOfUser = user.TypeOfUser?.Trim();
                return worker;
            });

        }

        internal static object CreateListWorkerForMakeNewWorkOrUpdate(bool wokerMakeWork = false)
        {
            return Run((db) =>
            {
                IEnumerable<User> QueryOfListOfWorkers = default;
                if (wokerMakeWork)
                {//Выбрать рабочие профессии
                    QueryOfListOfWorkers = db.User.Where(ee => ee.WorkerDetails.EstabilismentPost.FirstOrDefault().WorkerPosts.MakeWork == true).AsEnumerable();
                }
                else
                {//Для Увольнения либо обновления типа
                    QueryOfListOfWorkers = db.User.Where(ee => ee.TypeOfUser != SomeEnums.TypeOfUser.KD.ToString() && ee.TypeOfUser != SomeEnums.TypeOfUser.Cl.ToString() && ee.TypeOfUser != null).AsEnumerable();
                }
                if (QueryOfListOfWorkers.Any())
                {
                    ListOfWorkingWorker ListOfWorkingRab = new ListOfWorkingWorker();
                    ListOfWorkingRab.Workers = new List<ListOfWorkersThatWork>();
                    foreach (var workerOfWor in QueryOfListOfWorkers)
                    {
                        string female = "";
                        if (workerOfWor.Female.HasValue)
                        {
                            female = SomeEnums.FemaleType[workerOfWor.Female ?? default];
                        }
                        ListOfWorkersThatWork descriptionOfWorker = new ListOfWorkersThatWork();
                        descriptionOfWorker.DateRozd = workerOfWor.BirstDay.Value.ToString("dd.MM.yyyy HH:mm");
                        descriptionOfWorker.Female = female;
                        descriptionOfWorker.idUser = workerOfWor.idUser;
                        descriptionOfWorker.LastName = workerOfWor.LastName?.Trim();
                        descriptionOfWorker.Name = workerOfWor.Name?.Trim();
                        descriptionOfWorker.Patronymic = workerOfWor.Patronymic?.Trim();
                        descriptionOfWorker.TypeOfUser = workerOfWor.TypeOfUser?.Trim();
                        if (workerOfWor.WorkerDetails != null)
                            if (workerOfWor.WorkerDetails.EstabilismentPost.Any())
                            {
                                descriptionOfWorker.idPost = workerOfWor.WorkerDetails.EstabilismentPost.FirstOrDefault().idPost;
                                descriptionOfWorker.DateOfEstanbilesnent = workerOfWor.WorkerDetails.WorkersOperats.FirstOrDefault().DateOfOperate.Value.ToString("dd.MM.yyyy HH:mm");
                                descriptionOfWorker.NameOfPost = workerOfWor.WorkerDetails.EstabilismentPost.FirstOrDefault().WorkerPosts.NameOfPost?.Trim();
                                descriptionOfWorker.Salary = workerOfWor.WorkerDetails.EstabilismentPost.FirstOrDefault().Salary;
                            }
                        ListOfWorkingRab.Workers.Add(descriptionOfWorker);
                    }
                    ListOfWorkingRab.success = true;
                    return ListOfWorkingRab;
                }
                else
                {
                    return new ListOfWorkingWorker { success = false };
                }
            });
        }


        internal static object CreateOrUpdateNewCanditate(MakeNewWorker makeNewWorker)
        {
            return Run((db) =>
            {
                try
                {
                    if (makeNewWorker != null)
                    {
                        var user = new User
                        {
                            BirstDay = makeNewWorker.Birstday,
                            Female = makeNewWorker.Female,
                            idUser = makeNewWorker.idUser,
                            Name = makeNewWorker.Name,
                            LastName = makeNewWorker.Lastname,
                            Pasport = makeNewWorker.Pasport,
                            Patronymic = makeNewWorker.Patronymic,
                            TypeOfUser = makeNewWorker.TypeOfUser,

                        };
                        db.User.AddOrUpdate(user);
                        var workDet = new WorkerDetails
                        {
                            idAdress = makeNewWorker.idAdress,
                            IdWorker = makeNewWorker.idUser
                        };
                        db.WorkerDetails.AddOrUpdate(workDet);
                        if (makeNewWorker.InformatioAboutContact.ListOfContact != null)
                        {
                            foreach (var Contact in makeNewWorker.InformatioAboutContact.ListOfContact)
                            {
                                var dd = db.UserContact.Where(ee => ee.id == Contact.idContact);
                                UserContact contact = new UserContact();
                                contact.id = Contact.idContact;
                                contact.Description = Contact.Desctription;
                                contact.idType = Contact.idTypeOfContact;
                                contact.Value = Contact.Value;
                                contact.idUser = Contact.idUser;

                                if (dd == null)
                                    contact.DateAdd = Contact.DateAdd;

                                db.UserContact.AddOrUpdate(contact);
                            }
                        }
                        if (makeNewWorker.InformatioAboutContact.ListForDelete != null)
                        {
                            foreach (var idDelete in makeNewWorker.InformatioAboutContact.ListForDelete)
                            {
                                var select = db.UserContact.Where(ee => ee.id == idDelete).FirstOrDefault();
                                db.UserContact.Remove(select);
                            }
                        }
                        db.SaveChanges();
                        return new BaseResult { success = true };
                    }
                    else
                    {
                        return new BaseResult { success = false, description = $"Сервер ничего не получил" };
                    }
                }
                catch (Exception ex)
                {

                    return new BaseResult { success = false, description = $"Ошибка при работе с данными {ex.ToString()}" };

                }

            });

        }



        internal static object CreateNewPostData(DataAboutPost dataAboutPost)
        {
            return Run((db) =>
            {
                try
                {
                    if (dataAboutPost != null)
                    {
                        if (!string.IsNullOrEmpty(dataAboutPost.TypeOfUser))
                        {
                            var selectDataAboutUser = db.User.Where(ee => ee.idUser == dataAboutPost.idWorker).FirstOrDefault();
                            selectDataAboutUser.TypeOfUser = dataAboutPost.TypeOfUser?.Trim();
                        }
                        db.SaveChanges();
                        db.EstabilismentPost.Add(new EstabilismentPost
                        {
                            Salary = dataAboutPost.Salary,
                            idPost = dataAboutPost.idPost,
                            idWorker = dataAboutPost.idWorker,

                            idEstabilisment = dataAboutPost.idEstabilisment
                        });
                        db.SaveChanges();
                        db.WorkersOperats.Add(new WorkersOperats
                        {
                            idEstabilisment = dataAboutPost.idEstabilisment,
                            idWorker = dataAboutPost.idWorker,
                            idOperate = Guid.NewGuid(),
                            TypeOfOperate = dataAboutPost.TypeOfOperation,
                            DateOfOperate = dataAboutPost.DateOfOperate,
                        });
                        db.SaveChanges();
                        return new BaseResult { success = true };
                    }
                    else
                    {
                        return new BaseResult { success = false, description = $"Сервер ничего не получил" };
                    }
                }
                catch (Exception ex)
                {
                    return new BaseResult { success = false, description = $"Ошибка при работе с данными {ex.ToString()}" };
                }
            });
        }

        internal static object CreateDataAboutPayWages(PayWagesM dataAboutGiveMoney)
        {
            return Run((db) =>
            {
                try
                {
                    db.WorkersPayGive.Add(new WorkersPayGive
                    {
                        Data = dataAboutGiveMoney.Data,
                        Descriptiom = dataAboutGiveMoney.Descriptiom,
                        idGive = dataAboutGiveMoney.idGive,
                        idWorkerAdresat = dataAboutGiveMoney.idAdressat,
                        idWorkerMake = dataAboutGiveMoney.idMakeWorker,
                        Size = dataAboutGiveMoney.SizeOfData,
                    });
                    db.SaveChanges();
                    return new BaseResult { success = true };
                }
                catch (Exception ex)
                {
                    return new BaseResult { success = false, description = $"Ошибка при работе с данными {ex.ToString()}" };
                }
            });
        }

    }
}
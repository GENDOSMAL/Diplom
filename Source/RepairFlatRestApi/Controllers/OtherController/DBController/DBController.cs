using RepairFlatRestApi.Models;
using RepairFlatRestApi.Models.DescriptionJSON;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using static RepairFlatRestApi.Models.AdressModel;

namespace RepairFlatRestApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class DBController
    {
        #region Обработки при работе с данными пользователя


        internal static WorkWithOrder.DataForRedact SelectAllDataAboutOrder(Guid idOrder)
        {
            return Run((db) =>
            {
                var informationAbOrder = db.OrderInformation.Where(e => e.IdOrder == idOrder).First();
                var selectuser = db.User.Where(e => e.idUser == informationAbOrder.idClient).FirstOrDefault();
                var selectContact = db.UserContact.Where(e => e.idUser == informationAbOrder.idClient).First();
                var adressDesc = db.AdressDescription.Where(e => e.idAdress == informationAbOrder.IdAdress).First();

                WorkWithOrder.DataForRedact dataForRedact = new WorkWithOrder.DataForRedact();
                dataForRedact.FioClient = $"{selectuser.LastName} {selectuser.Name.Substring(0, 1)}.{selectuser.Patronymic.Substring(0, 1)}.";
                dataForRedact.MainSS = $"{selectContact.TypeOfContact.Value} : {selectContact.Value}";
                dataForRedact.DescAboutAdress = $"{adressDesc.RegionName} {adressDesc.CiryName} {adressDesc.MicroAreaName} {adressDesc.Street} {adressDesc.House} {adressDesc.Entrance} {adressDesc.NumberOfDelen}";
                dataForRedact.IformationAboutOrder = new WorkWithOrder.BaseOrderInformation
                {
                    idAdress = informationAbOrder.IdAdress,
                    Allsumma = informationAbOrder.AllSumma,
                    DataStart = informationAbOrder.DateStart,
                    Desc = informationAbOrder.Description,
                    idClient = informationAbOrder.idClient,
                    idOrder = informationAbOrder.IdOrder,
                    idWorkerMake = informationAbOrder.IdWorkerMake,
                    MainContactID = informationAbOrder.MainContactID,
                    Status = informationAbOrder.Status
                };

                return dataForRedact;
            }, nameof(DBController), nameof(SelectAllDataAboutOrder));
        }



        internal static object GetDataAboutContact(Guid idAdress)
        {
            return Run((db) =>
            {
                var select = db.AdressDescription.Where(ee => ee.idAdress == idAdress).FirstOrDefault();
                return new AdressDesc
                {
                    AreaName= select.AreaName,
                    Desc= select.Description,
                    CityName = select.CiryName,
                    description = select.Description,
                    Entrance = select.Entrance,
                    House = select.House,
                    idAdress = select.idAdress,
                    MicroAreaName = select.MicroAreaName,
                    NumberOfDelen = select.NumberOfDelen,
                    RegionName = select.RegionName,
                    Street = select.Street,
                    success = true
                };

            });
        }

        internal static object CreateNewServis(WorkWithOrder.DataAboutServis newServisData)
        {
            throw new NotImplementedException();
        }



        #endregion

        #region Работа с адресом
        internal static object CreaNewAdress(AdressModel.AdressDesc newAdress)
        {
            return Run((db) =>
            {
                try
                {
                    db.AdressDescription.Add(new AdressDescription
                    {
                        AreaName = newAdress.AreaName,
                        CiryName = newAdress.CityName,
                        Description = newAdress.description,
                        Entrance = newAdress.Entrance,
                        House = newAdress.House,
                        idAdress = newAdress.idAdress,
                        MicroAreaName = newAdress.MicroAreaName,
                        NumberOfDelen = newAdress.NumberOfDelen,
                        RegionName = newAdress.RegionName,
                        Street = newAdress.Street                       
                        
                    });
                    db.SaveChanges();
                    return new BaseResult { success = true };
                }
                catch (Exception ex)
                {
                    return new BaseResult { success = false, description = ex.ToString() };
                }
            }, nameof(DBController), nameof(CreaNewAdress));
        }

        internal static object UpdateDataAboutAdress(AdressModel.AdressDesc newAdress)
        {
            return Run((db) =>
            {
                try
                {
                    var UpdatedAdress = db.AdressDescription.Where(e => e.idAdress == newAdress.idAdress).FirstOrDefault();
                    
                    UpdatedAdress.CiryName = newAdress.CityName;
                    UpdatedAdress.Description = newAdress.Desc;
                    UpdatedAdress.Entrance = newAdress.Entrance;
                    UpdatedAdress.House = newAdress.House;
                    UpdatedAdress.idAdress = newAdress.idAdress;
                    UpdatedAdress.MicroAreaName = newAdress.MicroAreaName;
                    UpdatedAdress.NumberOfDelen = newAdress.NumberOfDelen;
                    UpdatedAdress.RegionName = newAdress.RegionName;
                    UpdatedAdress.Street = newAdress.Street;
                    UpdatedAdress.AreaName = newAdress.AreaName;

                    db.SaveChanges();
                    return new BaseResult { success = true };
                }
                catch (Exception ex)
                {
                    return new BaseResult { success = false, description = ex.ToString() };
                }
            }, nameof(DBController), nameof(UpdateDataAboutAdress));
        }

        internal static object MakeUpdatePost(MakeSubs.MakeUpdOrInsPost listOfPost)
        {
            return Run((db) =>
            {
                try
                {
                    List<Guid?> ListOfDeleteCodes = new List<Guid?>();
                    DateTime DateOfInsert = new DateTime();
                    if (!DateTime.TryParseExact(listOfPost.DateOfMake, "dd.MM.yyyy HH:mm", CultureInfo.GetCultureInfo("ru-RU"), DateTimeStyles.None, out DateOfInsert))
                        return new MakeSubs.ServisesMake
                        {
                            success = false,
                            description = "Ошибка при преобразовании данных о дате"
                        };


                    if (listOfPost.ListOfPostInsert != null)
                    {
                        foreach (var WhatInsert in listOfPost.ListOfPostInsert)
                        {
                            if(db.WorkerPosts.Where(ee=>ee.idPost== WhatInsert.idPost).FirstOrDefault()==null)
                            {
                                var NewPost = new WorkerPosts
                                {
                                    BaseWage = WhatInsert.BaseWage,
                                    NameOfPost = WhatInsert.NameOfPost,
                                    idPost = WhatInsert.idPost,
                                    MakeWork= WhatInsert.MakeWork
                                };

                                var InformationAboutIsert = new PostsUpdate
                                {
                                    idUpdatePos = Guid.NewGuid(),
                                    idPost = WhatInsert.idPost,
                                    IdUpdateUser = listOfPost.idUser,
                                    DateUpdate = DateOfInsert,
                                    TypeOfUpdate = SomeEnums.TypeOfAction.AddOrUpdate.ToString()
                                };
                                db.WorkerPosts.Add(NewPost).PostsUpdate.Add(InformationAboutIsert);
                            }
                        }
                    }

                    if (listOfPost.listOfPostUpdate != null)
                    {
                        foreach (var WhatUpdate in listOfPost.listOfPostUpdate)
                        {
                            var UpdatedServis = db.WorkerPosts.Where(e => e.idPost == WhatUpdate.idPost).FirstOrDefault();
                            if (UpdatedServis != null)
                            {
                                UpdatedServis.NameOfPost = WhatUpdate.NameOfPost;
                                UpdatedServis.BaseWage = WhatUpdate.BaseWage;

                                var InformationAboutUpdate = new PostsUpdate
                                {
                                    idUpdatePos = Guid.NewGuid(),
                                    idPost = WhatUpdate.idPost,
                                    IdUpdateUser = listOfPost.idUser,
                                    DateUpdate = DateOfInsert,
                                    TypeOfUpdate = SomeEnums.TypeOfAction.Update.ToString()
                                };
                                UpdatedServis.PostsUpdate.Add(InformationAboutUpdate);
                            }
                        }
                    }

                    if (listOfPost.ListOfDeletePost != null)
                    {
                        foreach (var WhatDelete in listOfPost.ListOfDeletePost)
                        {
                            var DeleteThings = db.WorkerPosts.Where(e => e.idPost == WhatDelete.idGuid).FirstOrDefault();
                            if (DeleteThings != null)
                            {
                                db.Entry(DeleteThings).Collection(c => c.PostsUpdate).Load();
                                db.WorkerPosts.Remove(DeleteThings);
                                foreach(var delThink in DeleteThings.PostsUpdate)
                                {
                                    DeleteThings.PostsUpdate.Remove(delThink);
                                }
                                ListOfDeleteCodes.Add(WhatDelete.idGuid);
                            }
                        }
                    }
                    db.SaveChanges();
                    if (ListOfDeleteCodes.Count != 0)
                    {
                        db.DeletedSubStr.AddRange(MakeListAboutDelete(ListOfDeleteCodes, DateOfInsert, listOfPost.idUser, SomeEnums.TypeOfSubs.Servises));
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    return new BaseResult
                    {
                        success = false,
                        description = $"Ошибка при работе с данными {ex.ToString()}!"
                    };
                }
                return new BaseResult
                {
                    success = true,
                    description = "Операции над данными были произведены!"
                };
            }, nameof(DBController), nameof(MakeInserAndUpdateServises));
        }

        internal static MakeSubs.PostMake MakeDataAboutPost(string dateofclientlastupdate)
        {
            return Run((db) =>
            {
                if (string.IsNullOrEmpty(dateofclientlastupdate))
                {//Если строка пустая возвращаем все
                    return AllPostHave();
                }
                else
                {
                    DateTime DateOfLastUpdate = new DateTime();
                    if (DateTime.TryParseExact(dateofclientlastupdate, "dd.MM.yyyy HH:mm", CultureInfo.GetCultureInfo("ru-RU"), DateTimeStyles.None, out DateOfLastUpdate))
                    {//Если дату удалось распознать вернуть в соответствии с датой
                        var QueryWithOutDelete = db.WorkerPosts.Where((e) => e.PostsUpdate.FirstOrDefault().DateUpdate > DateOfLastUpdate);
                        var ListOfPost = QueryWithOutDelete.Select(e => new MakeSubs.ListOfPostUpd
                        {
                            idPost=e.idPost,
                            BaseWage=e.BaseWage,
                            NameOfPost=e.NameOfPost,
                            TypeOfUpdate=e.PostsUpdate.FirstOrDefault().TypeOfUpdate,
                            MakeWork=e.MakeWork

                        }).ToList();

                        var QueryForDelete = db.DeletedSubStr.Where(e => e.DateOfDelete > DateOfLastUpdate && e.TypeOfDeleted == SomeEnums.TypeOfSubs.Post.ToString());
                        var ListOfDelete = QueryForDelete.Select(e => new MakeSubs.ListOfGuid
                        {
                            idGuid = e.idThingsDelete
                        }).ToList();

                        return new MakeSubs.PostMake
                        {
                            success = true,
                            kol = ListOfPost.Count + ListOfDelete.Count,
                            DateOfMakeAnswer = DateTime.Now,                            
                            listOfPost = ListOfPost,
                            ListOfDeletePost = ListOfDelete
                        };
                    }
                    else
                    {//Если дату не удалось распознать
                        return new MakeSubs.PostMake
                        {
                            success = false,
                            description = "Ошибка при преобразовании данных о дате"
                        };
                    }
                }
            }, nameof(DBController), nameof(UpdateDataAboutAdress));
        }

        private static MakeSubs.PostMake AllPostHave()
        {
            return Run((db) =>
            {
                var listOfPost = db.WorkerPosts.Select(e => new MakeSubs.ListOfPostUpd
                {
                    idPost=e.idPost,
                    BaseWage=e.BaseWage,
                    NameOfPost=e.NameOfPost,
                    MakeWork=e.MakeWork
                }).ToList();
                return new MakeSubs.PostMake
                {
                    success = true,
                    kol = listOfPost.Count,
                    listOfPost=listOfPost,
                    DateOfMakeAnswer = DateTime.Now
                };
            });
        }

        #endregion

        #region Обработки при работе с авторизацией пользователей
        /// <summary>
        /// Метод авторизации пользователя в системе.
        /// </summary>
        /// <param name="asked"> Структура в которой представлена информация о логине и пароле</param>
        /// <returns></returns>
        public static AuthDescription.ResultOfInformation Logining(AuthDescription.AskedInformation asked)
        {
            return Run((db) =>
            {
                var InfrormationAboutLogin = db.LoginInformation.Where(e => e.Login == asked.login && e.Password == asked.password);
                if (InfrormationAboutLogin.FirstOrDefault() == null)
                {
                    return new AuthDescription.ResultOfInformation
                    {
                        success = false,
                        description = "Не корректный логин и пароль"
                    };
                }
                else
                {
                    var data = InfrormationAboutLogin.FirstOrDefault();
                    string dd = $"{data.User.LastName} {data.User.Name.Substring(0, 1).ToUpper()}.{data.User.Patronymic.Substring(0, 1)}.";
                    return new AuthDescription.ResultOfInformation
                    {
                        success = true,
                        idUser = data.IdLog,
                        typeofpolz = data.User.TypeOfUser,
                        LastNameAndIni = dd
                    };
                }
            }, nameof(DBController), nameof(Logining));
        }





        /// <summary>
        /// Метод создания новых данных о логине. В процессе создания данных происходит проверка на повторенность логина
        /// </summary>
        /// <param name="informationAboutNewPerson">Стурктура в которой предствлена информация о новых данных для пользователя</param>
        /// <returns></returns>
        internal static AuthDescription.SucessNewUser CreateLoginPerson(AuthDescription.RegisterLoginPerson informationAboutNewPerson)
        {
            return Run((db) =>
            {
                Guid idUser = informationAboutNewPerson.idUser;
                if (db.LoginInformation.Where(e => e.IdLog == idUser).FirstOrDefault() != null)
                {
                    return new AuthDescription.SucessNewUser
                    {
                        success = false,
                        description = "Для данного пользователя уже есть информация о логине и пароле!"
                    };
                }
                var InforMationAboutLoginInTable = db.LoginInformation.Select(e => e.Login == informationAboutNewPerson.login);
                if (db.LoginInformation.Where(e => e.Login == informationAboutNewPerson.login).FirstOrDefault() != null)
                {
                    return new AuthDescription.SucessNewUser
                    {
                        success = false,
                        description = "Логин уже исползуется"
                    };
                }
                var newLogInformation = new LoginInformation
                {
                    IdLog = idUser,
                    Login = informationAboutNewPerson.login,
                    Password = HelperUS.PasswordDesript(informationAboutNewPerson.password)
                };
                db.LoginInformation.Add(newLogInformation);
                db.SaveChanges();
                return new AuthDescription.SucessNewUser
                {
                    success = true,
                    idUser = idUser,
                    description = "Данные о логине и пароле добавлены"
                };
            }, nameof(DBController), nameof(CreateLoginPerson));
        }
        #endregion

        #region Что-то общее

        internal static DeletedSubStr[] MakeListAboutDelete(List<Guid?> ListOfDeleteCodes, DateTime DateOfDelete, Guid idUser, SomeEnums.TypeOfSubs typeOfSubs)
        {
            DeletedSubStr[] result = new DeletedSubStr[ListOfDeleteCodes.Count];
            int l = 0;
            foreach (var codes in ListOfDeleteCodes)
            {
                var InformationAboutDelete = new DeletedSubStr
                {
                    DateOfDelete = DateOfDelete,
                    idDeleted = Guid.NewGuid(),
                    idThingsDelete = codes,
                    idUserDelete = idUser,
                    TypeOfDeleted = typeOfSubs.ToString()
                };

                result[l] = InformationAboutDelete;
                l++;
            }
            return result;
        }

        #endregion

        #region Обработки при работе с подстовляемыми данными об услугах

        internal static MakeSubs.ServisesMake MakeDataAboutUpdateServises(string dateofclientlastupdate)
        {
            return Run((db) =>
            {
                if (string.IsNullOrEmpty(dateofclientlastupdate))
                {//Если строка пустая возвращаем все
                    return AllServisesHave();
                }
                else
                {
                    DateTime DateOfLastUpdate = new DateTime();
                    if (DateTime.TryParseExact(dateofclientlastupdate, "dd.MM.yyyy HH:mm", CultureInfo.GetCultureInfo("ru-RU"), DateTimeStyles.None, out DateOfLastUpdate))
                    {//Если дату удалось распознать вернуть в соответствии с датой
                        var QueryWithOutUpdate = db.OurServices.Where((e) => e.ServicesUpdate.FirstOrDefault().DateOfUpdate > DateOfLastUpdate);
                        var ListOfServises = QueryWithOutUpdate.Select(e => new MakeSubs.ListOfUpdatedServises
                        {
                            idServises = e.idServis,
                            UnitOfMeasue = e.UnitOfMeasue,
                            Nomination = e.Nomination,
                            TypeOfServises = e.TypeOfServices,
                            Description = e.Description,
                            Cost = e.Cost,
                            TypeOfUpdate = e.ServicesUpdate.FirstOrDefault().TypeOfUpdate,
                        }).ToArray();

                        var QueryForDelete = db.DeletedSubStr.Where(e => e.DateOfDelete > DateOfLastUpdate && e.TypeOfDeleted == SomeEnums.TypeOfSubs.Servises.ToString());
                        var ListOfDelete = QueryForDelete.Select(e => new MakeSubs.ListOfGuid
                        {
                            idGuid = e.idThingsDelete
                        }).ToArray();


                        return new MakeSubs.ServisesMake
                        {
                            success = true,
                            kol = ListOfServises.Length + ListOfDelete.Length,
                            DateOfMakeAnswer = DateTime.Now,
                            ListOfServises = ListOfServises,
                            ListOfDeleteServises = ListOfDelete
                        };
                    }
                    else
                    {//Если дату не удалось распознать
                        return new MakeSubs.ServisesMake
                        {
                            success = false,
                            description = "Ошибка при преобразовании данных о дате"
                        };
                    }
                }
            }, nameof(DBController), nameof(MakeDataAboutUpdateServises));
        }


        internal static object MakeInserAndUpdateServises(MakeSubs.MakeUpdOrInsServises makeUpdateOrInserNew)
        {
            return Run((db) =>
            {
                try
                {
                    List<Guid?> ListOfDeleteCodes = new List<Guid?>();
                    DateTime DateOfInsert = new DateTime();
                    if (!DateTime.TryParseExact(makeUpdateOrInserNew.DateOfMake, "dd.MM.yyyy HH:mm", CultureInfo.GetCultureInfo("ru-RU"), DateTimeStyles.None, out DateOfInsert))
                        return new MakeSubs.ServisesMake
                        {
                            success = false,
                            description = "Ошибка при преобразовании данных о дате"
                        };


                    if (makeUpdateOrInserNew.ListOfServises != null)
                    {
                        foreach (var WhatInsert in makeUpdateOrInserNew.ListOfServises)
                        {
                            var NewServises = new OurServices
                            {
                                Cost = WhatInsert.Cost,
                                Description = WhatInsert.Description,
                                idServis = WhatInsert.idServises,
                                Nomination = WhatInsert.Nomination,
                                TypeOfServices = WhatInsert.TypeOfServises,
                                UnitOfMeasue = WhatInsert.UnitOfMeasue
                            };

                            var InformationAboutIsert = new ServicesUpdate
                            {
                                idServUpdate = Guid.NewGuid(),
                                IdUser = makeUpdateOrInserNew.idUser,
                                IdServices = WhatInsert.idServises,
                                DateOfUpdate = DateOfInsert,
                                TypeOfUpdate = SomeEnums.TypeOfAction.AddOrUpdate.ToString()
                            };

                            db.OurServices.AddOrUpdate(NewServises);
                            db.ServicesUpdate.Add(InformationAboutIsert);
                        }
                    }                   

                    if (makeUpdateOrInserNew.ListOfDeleteServises != null)
                    {
                        foreach (var WhatDelete in makeUpdateOrInserNew.ListOfDeleteServises)
                        {
                            var DeleteThings = db.OurServices.Where(e => e.idServis == WhatDelete.idGuid).FirstOrDefault();
                            if (DeleteThings != null)
                            {
                                db.Entry(DeleteThings).Collection(c => c.ServicesUpdate).Load();
                                db.OurServices.Remove(DeleteThings);
                                db.ServicesUpdate.RemoveRange(db.ServicesUpdate.Where(c => c.IdServices == WhatDelete.idGuid).ToArray());

                                ListOfDeleteCodes.Add(WhatDelete.idGuid);
                            }
                        }
                    }
                    db.SaveChanges();
                    if (ListOfDeleteCodes.Count != 0)
                    {
                        db.DeletedSubStr.AddRange(MakeListAboutDelete(ListOfDeleteCodes, DateOfInsert, makeUpdateOrInserNew.idUser, SomeEnums.TypeOfSubs.Servises));
                        db.SaveChanges();
                    }


                }
                catch (Exception ex)
                {
                    return new BaseResult
                    {
                        success = false,
                        description = $"Ошибка при работе с данными {ex.ToString()}!"
                    };
                }
                return new BaseResult
                {
                    success = true,
                    description = "Операции над данными были произведены!"
                };
            }, nameof(DBController), nameof(MakeInserAndUpdateServises));
        }

        internal static MakeSubs.ServisesMake AllServisesHave()
        {
            return Run((db) =>
            {
                var listOfServise = db.OurServices.Select(e => new MakeSubs.ListOfUpdatedServises
                {
                    idServises = e.idServis,
                    UnitOfMeasue = e.UnitOfMeasue,
                    Nomination = e.Nomination,
                    TypeOfServises = e.TypeOfServices,
                    Description = e.Description,
                    Cost = e.Cost,
                }).ToArray();
                return new MakeSubs.ServisesMake
                {
                    success = true,
                    kol = listOfServise.Length,
                    ListOfServises = listOfServise,
                    DateOfMakeAnswer = DateTime.Now
                };
            }, nameof(DBController), nameof(AllServisesHave));
        }

        #endregion

        #region Обработки при работе с подстовляемыми данными об типах помещений
        internal static object MakeDataAboutUpdatePremises(MakeSubs.MakeUpdOrInsPremises ListOfNewPremises)
        {
            return Run((db) =>
            {
                List<Guid?> ListOfDeleteCodes = new List<Guid?>();
                DateTime DateOfAction = new DateTime();
                if (!DateTime.TryParseExact(ListOfNewPremises.DateOfMake, "dd.MM.yyyy HH:mm", CultureInfo.GetCultureInfo("ru-RU"), DateTimeStyles.None, out DateOfAction))
                    return new MakeSubs.ServisesMake
                    {
                        success = false,
                        description = "Ошибка при преобразовании данных о дате"
                    };

                try
                {
                    if (ListOfNewPremises.ListOfPremises != null)
                    {

                        foreach (var InsertThing in ListOfNewPremises.ListOfPremises)
                        {

                            var newPremise = new PremisesType
                            {
                                Descriprtion = InsertThing.Description,
                                idPremises = InsertThing.idPremises,
                                NameOfPremises = InsertThing.Name
                            };
                            db.PremisesType.AddOrUpdate(newPremise);

                            var NewPremisesHistory = new PremisesUpdate
                            {
                                DateOfUpdate = DateOfAction,
                                TypeOfUpdate = SomeEnums.TypeOfAction.AddOrUpdate.ToString(),
                                IdUser = ListOfNewPremises.idUser,
                                idPremises = InsertThing.idPremises,
                                idPremisesUpdate = Guid.NewGuid()
                            };
                            db.PremisesUpdate.AddOrUpdate(NewPremisesHistory);
                        }
                    }

                    if (ListOfNewPremises.ListOfDeletePremises != null)
                    {
                        foreach (var whatDelete in ListOfNewPremises.ListOfDeletePremises)
                        {
                            var deleteThings = db.PremisesType.Where(e => e.idPremises == whatDelete.idGuid).FirstOrDefault();
                            if (deleteThings != null)
                            {
                                db.Entry(deleteThings).Collection(c => c.PremisesUpdate).Load();
                                db.PremisesType.Remove(deleteThings);
                                db.PremisesUpdate.RemoveRange(db.PremisesUpdate.Where(c => c.idPremises == whatDelete.idGuid).ToArray());
                                ListOfDeleteCodes.Add(whatDelete.idGuid);
                            }
                        }

                    }
                    db.SaveChanges();
                    if (ListOfDeleteCodes.Count != 0)
                    {
                        //TODO Проверить необходимость и если надо добавить
                        //db.PremisesUpdate.RemoveRange(db.PremisesUpdate.Where(e => e.idPremises == null).ToArray());
                        db.DeletedSubStr.AddRange(MakeListAboutDelete(ListOfDeleteCodes, DateOfAction, ListOfNewPremises.idUser, SomeEnums.TypeOfSubs.Premises));
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    return new BaseResult
                    {
                        success = false,
                        description = $"Ошибка при работе с данными {ex.ToString()}!"
                    };
                }
                return new BaseResult
                {
                    success = true,
                    description = "Операции над данными были произведены!"
                };
            }, nameof(DBController), nameof(MakeDataAboutUpdatePremises));
        }

        internal static MakeSubs.PremisesMake GetAllPremises(string dateofclientlastupdate)
        {
            return Run((db) =>
            {
                if (string.IsNullOrEmpty(dateofclientlastupdate))
                {//Если строка пустая возвращаем все
                    return AllPremisesHave();
                }
                else
                {
                    DateTime DateOfLastUpdate = new DateTime();
                    if (DateTime.TryParseExact(dateofclientlastupdate, "dd.MM.yyyy HH:mm", CultureInfo.GetCultureInfo("ru-RU"), DateTimeStyles.None, out DateOfLastUpdate))
                    {//Если дату удалось распознать вернуть в соответствии с датой
                        var QueryWithOutDelete = db.PremisesType.Where((e) => e.PremisesUpdate.FirstOrDefault().DateOfUpdate > DateOfLastUpdate);
                        var ListPremises = QueryWithOutDelete.Select(e => new MakeSubs.ListOfPremisesUpd
                        {
                            idPremises = e.idPremises,
                            Name = e.NameOfPremises,
                            Description = e.Descriprtion,
                            TypeOfUpdate = e.PremisesUpdate.FirstOrDefault().TypeOfUpdate,
                        }).ToArray();

                        var QueryForDelete = db.DeletedSubStr.Where(e => e.DateOfDelete > DateOfLastUpdate && e.TypeOfDeleted == SomeEnums.TypeOfSubs.Premises.ToString());
                        var ListOfDelete = QueryForDelete.Select(e => new MakeSubs.ListOfGuid
                        {
                            idGuid = e.idThingsDelete
                        }).ToArray();

                        return new MakeSubs.PremisesMake
                        {
                            success = true,
                            kol = ListPremises.Length + ListOfDelete.Length,
                            DateOfMakeAnswer = DateTime.Now,
                            listOfPremises = ListPremises,
                            ListOfDeletePremises = ListOfDelete
                        };
                    }
                    else
                    {//Если дату не удалось распознать
                        return new MakeSubs.PremisesMake
                        {
                            success = false,
                            description = "Ошибка при преобразовании данных о дате"
                        };
                    }
                }
            }, nameof(DBController), nameof(GetAllPremises));
        }

        private static MakeSubs.PremisesMake AllPremisesHave()
        {
            return Run((db) =>
            {
                var listOfServise = db.PremisesType.Select(e => new MakeSubs.ListOfPremisesUpd
                {
                    idPremises = e.idPremises,
                    Name = e.NameOfPremises,
                    Description = e.Descriprtion
                }).ToArray();
                return new MakeSubs.PremisesMake
                {
                    success = true,
                    kol = listOfServise.Length,
                    listOfPremises = listOfServise,
                    DateOfMakeAnswer = DateTime.Now
                };
            }, nameof(DBController), nameof(AllServisesHave));
        }
        #endregion

        #region Обработки при работе с подстовляемыми данными о материалах

        internal static MakeSubs.MaterialsMake MakeAllDataAboutContackt()
        {
            return Run((db) =>
            {
                var ListOfMaterial = db.OurMaterials.Select(e => new MakeSubs.ListOfMaterialsUpd
                {
                    idMaterials = e.idMaterials,
                    NameOfMaterial = e.NameOfMaterial,
                    UnitOfMeasue = e.UnitOfMeasue,
                    Cost = e.Cost,
                    Description = e.Description
                }).ToArray();
                return new MakeSubs.MaterialsMake
                {
                    success = true,
                    kol = ListOfMaterial.Length,
                    listOfMaterials = ListOfMaterial,
                    DateOfMakeAnswer = DateTime.Now
                };
            }, nameof(DBController), nameof(MakeAllDataAboutContackt));
        }

        internal static object MakeUpdateMaterials(MakeSubs.MakeUpdOrInsMaterials ListOfMaterials)
        {
            return Run((db) =>
            {
                List<Guid?> ListOfDeleteCodes = new List<Guid?>();
                DateTime DateOfAction = new DateTime();
                if (!DateTime.TryParseExact(ListOfMaterials.DateOfMake, "dd.MM.yyyy HH:mm", CultureInfo.GetCultureInfo("ru-RU"), DateTimeStyles.None, out DateOfAction))
                    return new MakeSubs.ServisesMake
                    {
                        success = false,
                        description = "Ошибка при преобразовании данных о дате"
                    };

                try
                {
                    if (ListOfMaterials.ListOfMaterials != null)
                    {
                        foreach (var NewMaterial in ListOfMaterials.ListOfMaterials)
                        {
                            var NMat = new OurMaterials
                            {
                                Cost = NewMaterial.Cost,
                                Description = NewMaterial.Description,
                                idMaterials = NewMaterial.idMaterials,
                                NameOfMaterial = NewMaterial.NameOfMaterial,
                                TypeOfMaterial = NewMaterial.TypeOfMaterial,
                                UnitOfMeasue = NewMaterial.UnitOfMeasue
                            };

                            var NMatHistory = new MaterialsUpdate
                            {
                                DateOfUpdate = DateOfAction,
                                IdUser = ListOfMaterials.idUser,
                                TypeOfUpdate = SomeEnums.TypeOfAction.AddOrUpdate.ToString(),
                                idMaterials = NewMaterial.idMaterials,
                                idMaterialUpdate = Guid.NewGuid()
                            };
                            db.OurMaterials.AddOrUpdate(NMat);
                            db.MaterialsUpdate.AddOrUpdate(NMatHistory);
                        }
                    }

                    if (ListOfMaterials.ListOfDeleteMaterials != null)
                    {
                        foreach (var DelMaterila in ListOfMaterials.ListOfDeleteMaterials)
                        {
                            var DeleteThing = db.OurMaterials.Where(e => e.idMaterials == DelMaterila.idGuid).FirstOrDefault();
                            if (DeleteThing != null)
                            {
                                db.Entry(DeleteThing).Collection(c => c.MaterialsUpdate).Load();
                                db.OurMaterials.Remove(DeleteThing);
                                db.MaterialsUpdate.RemoveRange(db.MaterialsUpdate.Where(c => c.idMaterials == DelMaterila.idGuid).ToArray());
                                ListOfDeleteCodes.Add(DelMaterila.idGuid);
                            }
                        }
                    }
                    db.SaveChanges();
                    if (ListOfDeleteCodes.Count != 0)
                    {
                        db.DeletedSubStr.AddRange(MakeListAboutDelete(ListOfDeleteCodes, DateOfAction, ListOfMaterials.idUser, SomeEnums.TypeOfSubs.Materials));
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    return new BaseResult
                    {
                        success = false,
                        description = $"Ошибка при работе с данными {ex.ToString()}!"
                    };
                }
                return new BaseResult
                {
                    success = true,
                    description = "Операции над данными были произведены!"
                };
            }, nameof(DBController), nameof(MakeUpdateMaterials));
        }

        internal static object MakeListOfMaterials(string dateofclientlastupdate)
        {
            return Run((db) =>
            {
                if (string.IsNullOrEmpty(dateofclientlastupdate))
                {//Если строка пустая возвращаем все
                    return MakeAllDataAboutContackt();
                }
                else
                {
                    DateTime DateOfLastAction = new DateTime();
                    if (DateTime.TryParseExact(dateofclientlastupdate, "dd.MM.yyyy HH:mm", CultureInfo.GetCultureInfo("ru-RU"), DateTimeStyles.None, out DateOfLastAction))
                    {//Если дату удалось распознать вернуть в соответствии с датой
                        var QueryWithOutDelete = db.OurMaterials.Where((e) => e.MaterialsUpdate.FirstOrDefault().DateOfUpdate > DateOfLastAction);
                        var ListPremises = QueryWithOutDelete.Select(e => new MakeSubs.ListOfMaterialsUpd
                        {
                            idMaterials = e.idMaterials,
                            NameOfMaterial = e.NameOfMaterial,
                            UnitOfMeasue = e.UnitOfMeasue,
                            Cost = e.Cost,
                            Description = e.Description,
                            TypeOfUpdate = e.MaterialsUpdate.FirstOrDefault().TypeOfUpdate

                        }).ToArray();

                        var QueryForDelete = db.DeletedSubStr.Where(e => e.DateOfDelete > DateOfLastAction && e.TypeOfDeleted == SomeEnums.TypeOfSubs.Materials.ToString());
                        var ListOfDelete = QueryForDelete.Select(e => new MakeSubs.ListOfGuid
                        {
                            idGuid = e.idThingsDelete
                        }).ToArray();

                        return new MakeSubs.MaterialsMake
                        {
                            success = true,
                            kol = ListPremises.Length + ListOfDelete.Length,
                            DateOfMakeAnswer = DateTime.Now,
                            listOfMaterials = ListPremises,
                            ListOfDeleteMaterials = ListOfDelete
                        };
                    }
                    else
                    {//Если дату не удалось распознать
                        return new MakeSubs.MaterialsMake
                        {
                            success = false,
                            description = "Ошибка при преобразовании данных о дате"
                        };
                    }
                }
            }, nameof(DBController), nameof(MakeListOfMaterials));
        }

        #endregion

        #region Обработки при работе с подстовляемыми данными о контактах

        internal static MakeSubs.ContactsMake MakeAllDataAboutContacts()
        {
            return Run((db) =>
            {
                var ListOfContacts = db.TypeOfContact.Select(e => new MakeSubs.ListOfContactsUpd
                {
                    Description = e.Description,
                    Value = e.Value,
                    idContact = e.idContact
                }).ToList();

                return new MakeSubs.ContactsMake
                {
                    success = true,
                    kol = ListOfContacts.Count,
                    listOfContacts = ListOfContacts,
                    DateOfMakeAnswer = DateTime.Now
                };
            }, nameof(DBController), nameof(MakeAllDataAboutContackt));
        }

        internal static object MakeUpdateContacts(MakeSubs.MakeUpdOrInsContacts ListOfContacts)
        {
            return Run((db) =>
            {
                List<Guid?> ListOfDeleteCodes = new List<Guid?>();
                DateTime DateOfAction = new DateTime();
                if (!DateTime.TryParseExact(ListOfContacts.DateOfMake, "dd.MM.yyyy HH:mm", CultureInfo.GetCultureInfo("ru-RU"), DateTimeStyles.None, out DateOfAction))
                    return new MakeSubs.ServisesMake
                    {
                        success = false,
                        description = "Ошибка при преобразовании данных о дате"
                    };
                try
                {
                    if (ListOfContacts.ListOfContacts != null)
                    {
                        foreach (var ListOfInsContacts in ListOfContacts.ListOfContacts)
                        {
                            var NTContact = new TypeOfContact
                            {
                                idContact = ListOfInsContacts.idContact ?? default(Guid),
                                Description = ListOfInsContacts.Description,
                                Value = ListOfInsContacts.Value
                            };

                            var HistOfInsContact = new ContactUpdate
                            {
                                idUser = ListOfContacts.idUser,
                                idContact = ListOfInsContacts.idContact,
                                DataOfUpdate = DateOfAction,
                                idContactUpdate = Guid.NewGuid(),
                                TypeOfUpdate = SomeEnums.TypeOfAction.AddOrUpdate.ToString()
                            };
                            db.TypeOfContact.AddOrUpdate(NTContact);
                            db.ContactUpdate.AddOrUpdate(HistOfInsContact);
                        }
                    }

                    if (ListOfContacts.ListOfDeleteContacts != null)
                    {
                        foreach (var DeleteContact in ListOfContacts.ListOfDeleteContacts)
                        {
                            var Query = db.TypeOfContact.Where(e => e.idContact == DeleteContact.idGuid).FirstOrDefault();
                            if (Query != null)
                            {
                                db.Entry(Query).Collection(c => c.ContactUpdate).Load();
                                db.TypeOfContact.Remove(Query);
                                db.ContactUpdate.RemoveRange(db.ContactUpdate.Where(c => c.idContactUpdate == DeleteContact.idGuid).ToArray());
                                ListOfDeleteCodes.Add(DeleteContact.idGuid);
                            }
                        }
                    }
                    db.SaveChanges();
                    if (ListOfDeleteCodes.Count != 0)
                    {
                        db.DeletedSubStr.AddRange(MakeListAboutDelete(ListOfDeleteCodes, DateOfAction, ListOfContacts.idUser, SomeEnums.TypeOfSubs.Contact));
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    return new BaseResult
                    {
                        success = false,
                        description = $"Ошибка при работе с данными {ex.ToString()}!"
                    };
                }
                return new BaseResult
                {
                    success = true,
                    description = "Операции над данными были произведены!"
                };
            }, nameof(DBController), nameof(MakeUpdateContacts));
        }

        internal static object MakeListOfContacts(string dateOfLastClientAction)
        {
            return Run((db) =>
            {
                if (string.IsNullOrEmpty(dateOfLastClientAction))
                {//Если строка пустая возвращаем все
                    return MakeAllDataAboutContacts();
                }
                else
                {
                    DateTime DateOfLastAction = new DateTime();
                    if (DateTime.TryParseExact(dateOfLastClientAction, "dd.MM.yyyy HH:mm", CultureInfo.GetCultureInfo("ru-RU"), DateTimeStyles.None, out DateOfLastAction))
                    {//Если дату удалось распознать вернуть в соответствии с датой
                        var QueryWithOutDelete = db.ContactUpdate.Where((e) => e.DataOfUpdate > DateOfLastAction);
                        var ListPremises = QueryWithOutDelete.Select(e => new MakeSubs.ListOfContactsUpd
                        {
                            Description = e.TypeOfContact.Description,
                            idContact = e.idContact,
                            Value = e.TypeOfContact.Value,
                            TypeOfUpdate = e.TypeOfUpdate
                        }).ToList();

                        var QueryForDelete = db.DeletedSubStr.Where(e => e.DateOfDelete > DateOfLastAction && e.TypeOfDeleted == SomeEnums.TypeOfSubs.Contact.ToString());
                        var ListOfDelete = QueryForDelete.Select(e => new MakeSubs.ListOfGuid
                        {
                            idGuid = e.idThingsDelete
                        }).ToList();

                        return new MakeSubs.ContactsMake
                        {
                            success = true,
                            kol = ListPremises.Count + ListOfDelete.Count,
                            DateOfMakeAnswer = DateTime.Now,
                            listOfContacts = ListPremises,
                            ListOfDeleteContacts = ListOfDelete
                        };
                    }
                    else
                    {//Если дату не удалось распознать
                        return new MakeSubs.ContactsMake
                        {
                            success = false,
                            description = "Ошибка при преобразовании данных о дате"
                        };
                    }
                }
            }, nameof(DBController), nameof(MakeUpdateContacts));
        }
        #endregion


        #region Работа с заказами

        internal static WorkWithOrder.DataAboutAllOrder MakeDataAboutAllOrder()
        {
            return Run((db) =>
            {
                List<WorkWithOrder.OrderInTable> orders = new List<WorkWithOrder.OrderInTable>();
                foreach (var order in db.OrderInformation.AsEnumerable())
                {//TODO Оптимизировать
                    string F = db.User.Where(e => e.idUser == order.idClient).Select(e => e.LastName).FirstOrDefault();
                    string I = db.User.Where(e => e.idUser == order.idClient).Select(e => e.Name.Substring(0, 1)).FirstOrDefault();
                    string O = db.User.Where(e => e.idUser == order.idClient).Select(e => e.Patronymic.Substring(0, 1)).FirstOrDefault();
                    string NameOfCleint = $"{F} {I}.{O}.";
                    string TypeOfContact = db.TypeOfContact.Where((e) => e.UserContact.First().id == order.MainContactID).Select(e => e.Value).FirstOrDefault();
                    string ValuesOfContact = db.UserContact.Where(e => e.id == order.MainContactID).Select(e => e.Value).First();
                    string ContactInformatiom = $"{TypeOfContact} : {ValuesOfContact}";

                    var informationAboutAdress = db.AdressDescription.Where(e => e.idAdress == order.IdAdress).Select(e => new AdressDescription { idAdress = e.idAdress, AreaName = e.AreaName, Entrance = e.Entrance, CiryName = e.CiryName, Description = e.Description, House = e.House, MicroAreaName = e.MicroAreaName, NumberOfDelen = e.NumberOfDelen, RegionName = e.RegionName, Street = e.Street }).First();

                    string Adressinformation = $"{informationAboutAdress.RegionName} {informationAboutAdress.CiryName} {informationAboutAdress.MicroAreaName} {informationAboutAdress.Street} {informationAboutAdress.House} {informationAboutAdress.Entrance} {informationAboutAdress.NumberOfDelen}";
                    orders.Add(new WorkWithOrder.OrderInTable
                    {
                        DataAboutContact = ContactInformatiom,
                        DataStart = order.DateStart,
                        FIOCLient = NameOfCleint,
                        idOrder = order.IdOrder,
                        InformationAboutAdress = Adressinformation,
                        money = order.AllSumma,
                        number = order.Number,
                        Status = order.Status
                    });

                }
                return new WorkWithOrder.DataAboutAllOrder() { success = true, orders = orders, Count = orders.Count };
            }, nameof(DBController), nameof(MakeDataAboutAllOrder));
        }

        internal static object CreateNewOrder(WorkWithOrder.BaseOrderInformation newOrderData)
        {
            return Run((db) =>
            {
                try
                {
                    db.OrderInformation.Add(new OrderInformation
                    {
                        Status = newOrderData.Status,
                        AllSumma = newOrderData.Allsumma,
                        IdAdress = newOrderData.idAdress,
                        idClient = newOrderData.idClient,
                        MainContactID = newOrderData.MainContactID,
                        IdOrder = newOrderData.idOrder,
                        DateStart = newOrderData.DataStart,
                        IdWorkerMake = newOrderData.idWorkerMake,
                        Description = newOrderData.Desc,
                    });
                    db.SaveChanges();
                    return new BaseResult { success = true };
                }
                catch (Exception ex)
                {
                    return new BaseResult { success = false, description = ex.Message };
                }
            }, nameof(DBController), nameof(CreateNewOrder));
        }
        internal static object UpdateDataAboutOrder(WorkWithOrder.BaseOrderInformation updateDataAbOrder)
        {
            return Run((db) =>
            {
                try
                {
                    var selectWorkersOrder = db.OrderInformation.Where(e1 => e1.IdOrder == updateDataAbOrder.idOrder).First();
                    if (selectWorkersOrder != null)
                    {
                        
                        selectWorkersOrder.AllSumma = updateDataAbOrder.Allsumma;
                        selectWorkersOrder.IdAdress = updateDataAbOrder.idAdress;
                        selectWorkersOrder.idClient = updateDataAbOrder.idClient;
                        selectWorkersOrder.MainContactID = updateDataAbOrder.MainContactID;
                        selectWorkersOrder.Description = updateDataAbOrder.Desc;
                        selectWorkersOrder.Status = updateDataAbOrder.Status;
                        db.SaveChanges();
                        return new BaseResult { success = true };
                    }
                    else
                    {
                        return new BaseResult { success = false, description = "Заказ не найден"};
                    }
                }
                catch (Exception ex)
                {
                    return new BaseResult { success = false, description = ex.Message };
                }
            }, nameof(DBController), nameof(CreateNewOrder));
        }


        #endregion

        #region Базовые вещи
        /// <summary>
        /// Базовый метод обработки запросов который также обрабатывает ошибки
        /// </summary>
        /// <param name="dbAction">Событие работы с базой данных</param>
        /// <param name="nameOfMethod">Наименование метода, необходимо для логирования</param>
        //public static void Run(Action<RepairFlatDB> dbAction, string nameOfMethod)
        //{
        //    using (var db = new RepairFlatDB())
        //    {
        //        try
        //        {
        //            dbAction(db);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception($"В методе {nameof(DBController)}::{nameOfMethod} произошла ошибка: <{ex.ToString()}>");
        //        }
        //    }
        //}
        /// <summary>
        /// Метод обработки работы с базой данных, который также может возвращать некоторые данные
        /// </summary>
        /// <typeparam name="TResult">Тип данных который будет вернут от метода</typeparam>
        /// <param name="dbFunction">событие работы базы данных</param>
        /// <param name="NameOfClass">Наименование класса в котором происходит обработка метода, необходимо для логирования</param>
        /// <param name="nameOfMethod">Наименование метода, который вызывает данный метод, необходимо для ведения логов</param>
        /// <returns></returns>
        public static TResult Run<TResult>(Func<RepairFlatDB, TResult> dbFunction, string NameOfClass, string nameOfMethod)
        {
            using (var db = new RepairFlatDB())
            {
                try
                {
                    return dbFunction(db);
                }
                catch (Exception ex)
                {
                    throw new Exception($"В методе {NameOfClass}::{nameOfMethod} произошла ошибка: <{ex.ToString()}>");
                }
            }
        }

        public static TResult Run<TResult>(Func<RepairFlatDB, TResult> dbFunction)
        {
            using (var db = new RepairFlatDB())
            {
                try
                {
                    return dbFunction(db);
                }
                catch (Exception ex)
                {
                    throw new Exception($"В методе произошла ошибка: <{ex.ToString()}>");
                }
            }
        }
        #endregion
    }
}
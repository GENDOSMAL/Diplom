﻿using RepairFlatRestApi.Models;
using RepairFlatRestApi.Models.DescriptionJSON;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace RepairFlatRestApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public  class DBController
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
                dataForRedact.FioClient =$"{selectuser.LastName} {selectuser.Name.Substring(0,1)}.{selectuser.Patronymic.Substring(0, 1)}.";
                dataForRedact.MainSS = $"{selectContact.TypeOfContact.Value} : {selectContact.Value}";
                dataForRedact.DescAboutAdress = $"{adressDesc.RegionName} {adressDesc.CiryName} {adressDesc.MicroAreaName} {adressDesc.Street} {adressDesc.House} {adressDesc.Entrance} {adressDesc.NumberOfDelen}";
                dataForRedact.IformationAboutOrder = new WorkWithOrder.BaseOrderInformation
                {
                    idAdress = informationAbOrder.IdAdress,
                    Allsumma = informationAbOrder.AllSumma,
                    DataStart= informationAbOrder.DateStart,
                    DateEnd= informationAbOrder.DateEnd,
                    Desc= informationAbOrder.Description,
                    idClient= informationAbOrder.idClient,
                    idColoboration= informationAbOrder.IdColoboration,
                    idOrder= informationAbOrder.IdOrder,
                    idWorkerMake= informationAbOrder.IdWorkerMake,
                    MainContactID= informationAbOrder.MainContactID,
                    Status= informationAbOrder.Status
                };

                return dataForRedact;
            }, nameof(DBController), nameof(SelectAllDataAboutOrder));
        }

        internal static object CreateNewServis(WorkWithOrder.DataAboutServis newServisData)
        {
            throw new NotImplementedException();
        }



        #endregion

        #region Работа с адресом
        internal static object CreaNewAdress(AdressDescription newAdress)
        {
            return Run((db) => 
            {
                try
                {
                    db.AdressDescription.Add(new AdressDescription
                    {
                        AreaName=newAdress.AreaName,
                        CiryName=newAdress.CiryName,
                        Description=newAdress.Description,
                        Entrance=newAdress.Entrance,
                        House=newAdress.House,
                        idAdress=newAdress.idAdress,
                        MicroAreaName=newAdress.MicroAreaName,
                        NumberOfDelen=newAdress.NumberOfDelen,
                        RegionName=newAdress.RegionName,
                        Street=newAdress.Street
                    });
                    db.SaveChanges();
                    return new BaseResult { success = true};
                }
                catch(Exception ex)
                {
                    return new BaseResult { success = false, description=ex.ToString()};
                }
            }, nameof(DBController), nameof(CreateNewClient));
        }

        internal static object UpdateDataAboutAdress(AdressDescription newAdress)
        {
            return Run((db) => 
            {
                try
                {
                    var UpdatedAdress = db.AdressDescription.Where(e => e.idAdress == newAdress.idAdress).FirstOrDefault();
                    UpdatedAdress.AreaName = newAdress.AreaName;
                    UpdatedAdress.CiryName = newAdress.CiryName;
                    UpdatedAdress.Description = newAdress.Description;
                    UpdatedAdress.Entrance = newAdress.Entrance;
                    UpdatedAdress.House = newAdress.House;
                    UpdatedAdress.idAdress = newAdress.idAdress;
                    UpdatedAdress.MicroAreaName = newAdress.MicroAreaName;
                    UpdatedAdress.NumberOfDelen = newAdress.NumberOfDelen;
                    UpdatedAdress.RegionName = newAdress.RegionName;
                    UpdatedAdress.Street = newAdress.Street;

                    db.SaveChanges();
                    return new BaseResult { success = true};
                }
                catch (Exception ex)
                {
                    return new BaseResult { success = false, description = ex.ToString() };
                }
            }, nameof(DBController), nameof(CreateNewClient)); 
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
                        sucess = false,
                        description = "Не корректный логин и пароль"
                    };
                }
                else
                {
                    return InfrormationAboutLogin.Select(e1 => new AuthDescription.ResultOfInformation
                    {
                        sucess = true,
                        idUser = e1.IdLog,
                        typeofpolz = e1.User.TypeOfUser,
                        LastNameAndIni = $"{e1.User.LastName} {e1.User.Name.Substring(0, 1).ToUpper()}.{e1.User.Patronymic.Substring(0, 1)}."
                    }).FirstOrDefault();
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


                    if (makeUpdateOrInserNew.ListOfServisesInsert != null)
                    {
                        foreach (var WhatInsert in makeUpdateOrInserNew.ListOfServisesInsert)
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
                                TypeOfUpdate = SomeEnums.TypeOfAction.Add.ToString()
                            };

                            db.OurServices.Add(NewServises);
                            db.ServicesUpdate.Add(InformationAboutIsert);
                        }
                    }

                    if (makeUpdateOrInserNew.listOfServisesUpdate != null)
                    {
                        foreach (var WhatUpdate in makeUpdateOrInserNew.listOfServisesUpdate)
                        {
                            var UpdatedServis = db.OurServices.Where(e => e.idServis == WhatUpdate.idServises).FirstOrDefault();
                            if (UpdatedServis != null)
                            {
                                UpdatedServis.Nomination = WhatUpdate.Nomination;
                                UpdatedServis.TypeOfServices = WhatUpdate.TypeOfServises;
                                UpdatedServis.Description = WhatUpdate.Description;
                                UpdatedServis.Cost = WhatUpdate.Cost;
                                UpdatedServis.UnitOfMeasue = WhatUpdate.UnitOfMeasue;

                                var InformationAboutIsert = new ServicesUpdate
                                {
                                    idServUpdate = Guid.NewGuid(),
                                    IdUser = makeUpdateOrInserNew.idUser,
                                    IdServices = WhatUpdate.idServises,
                                    DateOfUpdate = DateOfInsert,
                                    TypeOfUpdate = SomeEnums.TypeOfAction.Update.ToString()
                                };
                                db.ServicesUpdate.Add(InformationAboutIsert);
                            }
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
            throw new NotImplementedException();
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
                    if (ListOfNewPremises.ListOfPremisesInsert != null)
                    {

                        foreach (var InsertThing in ListOfNewPremises.ListOfPremisesInsert)
                        {

                            var newPremise = new PremisesType
                            {
                                Descriprtion = InsertThing.Description,
                                idPremises = InsertThing.idPremises,
                                NameOfPremises = InsertThing.Name
                            };
                            db.PremisesType.Add(newPremise);

                            var NewPremisesHistory = new PremisesUpdate
                            {
                                DateOfUpdate = DateOfAction,
                                TypeOfUpdate = SomeEnums.TypeOfAction.Add.ToString(),
                                IdUser = ListOfNewPremises.idUser,
                                idPremises = InsertThing.idPremises,
                                idPremisesUpdate = Guid.NewGuid()
                            };
                            db.PremisesUpdate.Add(NewPremisesHistory);
                        }
                    }

                    if (ListOfNewPremises.listOfPremisesUpdate != null)
                    {
                        foreach (var whatUpdate in ListOfNewPremises.listOfPremisesUpdate)
                        {
                            var updatePremises = db.PremisesType.Where(e => e.idPremises == whatUpdate.idPremises).FirstOrDefault();
                            if (updatePremises != null)
                            {
                                updatePremises.NameOfPremises = whatUpdate.Name;
                                updatePremises.Descriprtion = whatUpdate.Description;

                                var NewPremisesHistory = new PremisesUpdate
                                {
                                    DateOfUpdate = DateOfAction,
                                    TypeOfUpdate = SomeEnums.TypeOfAction.Update.ToString(),
                                    IdUser = ListOfNewPremises.idUser,
                                    idPremises = whatUpdate.idPremises,
                                    idPremisesUpdate = Guid.NewGuid()
                                };
                                db.PremisesUpdate.Add(NewPremisesHistory);
                            }
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
                        var QueryWithOutDelete = db.PremisesType.Where((e) => e.PremisesUpdate.FirstOrDefault().DateOfUpdate > DateOfLastUpdate );
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
                    if (ListOfMaterials.ListOfMaterialsInsert != null)
                    {
                        foreach (var NewMaterial in ListOfMaterials.ListOfMaterialsInsert)
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
                                TypeOfUpdate = SomeEnums.TypeOfAction.Add.ToString(),
                                idMaterials = NewMaterial.idMaterials,
                                idMaterialUpdate = Guid.NewGuid()
                            };
                            db.OurMaterials.Add(NMat);
                            db.MaterialsUpdate.Add(NMatHistory);
                        }
                    }

                    if (ListOfMaterials.listOfMaterialsUpdate != null)
                    {
                        foreach (var MatUpdate in ListOfMaterials.listOfMaterialsUpdate)
                        {
                            var MaterialUpdate = db.OurMaterials.Where(e => e.idMaterials == MatUpdate.idMaterials).FirstOrDefault();
                            if (MaterialUpdate != null)
                            {
                                MaterialUpdate.NameOfMaterial = MatUpdate.NameOfMaterial;
                                MaterialUpdate.UnitOfMeasue = MatUpdate.UnitOfMeasue;
                                MaterialUpdate.Cost = MatUpdate.Cost;
                                MaterialUpdate.Description = MatUpdate.Description;
                                MaterialUpdate.TypeOfMaterial = MatUpdate.TypeOfMaterial;

                                var HistOfMatUpd = new MaterialsUpdate
                                {
                                    DateOfUpdate = DateOfAction,
                                    idMaterials = MatUpdate.idMaterials,
                                    idMaterialUpdate = Guid.NewGuid(),
                                    IdUser = ListOfMaterials.idUser,
                                    TypeOfUpdate = SomeEnums.TypeOfAction.Update.ToString()
                                };
                                db.MaterialsUpdate.Add(HistOfMatUpd);
                            }
                        }
                    }

                    if (ListOfMaterials.ListOfDeleteMaterials != null)
                    {
                        foreach(var DelMaterila in ListOfMaterials.ListOfDeleteMaterials)
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
                }).ToArray();

                return new MakeSubs.ContactsMake
                {
                    success = true,
                    kol = ListOfContacts.Length,
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
                    if (ListOfContacts.ListOfContactsInsert != null)
                    {
                        foreach(var ListOfInsContacts in ListOfContacts.ListOfContactsInsert)
                        {
                            var NTContact = new TypeOfContact
                            {
                                idContact = ListOfInsContacts.idContact,
                                Description = ListOfInsContacts.Description,
                                Value = ListOfInsContacts.Value
                            };

                            var HistOfInsContact = new ContactUpdate
                            {
                                idUser = ListOfContacts.idUser,
                                idContact = ListOfInsContacts.idContact,
                                DataOfUpdate = DateOfAction,
                                idContactUpdate = Guid.NewGuid(),
                                TypeOfUpdate = SomeEnums.TypeOfAction.Add.ToString()
                            };
                            db.TypeOfContact.Add(NTContact);
                            db.ContactUpdate.Add(HistOfInsContact);
                        }
                    }

                    if (ListOfContacts.listOfContactsUpdate != null)
                    {
                        foreach(var ContactUpdate in ListOfContacts.listOfContactsUpdate)
                        {
                            var UpdatedContact = db.TypeOfContact.Where(e => e.idContact == ContactUpdate.idContact).FirstOrDefault();
                            if (UpdatedContact != null)
                            {
                                UpdatedContact.Value = ContactUpdate.Value;
                                UpdatedContact.Description = ContactUpdate.Description;

                                var HistOfUpdate = new ContactUpdate
                                {
                                    DataOfUpdate = DateOfAction,
                                    idContact = ContactUpdate.idContact,
                                    idUser = ListOfContacts.idUser,
                                    TypeOfUpdate = SomeEnums.TypeOfAction.Update.ToString(),
                                    idContactUpdate = Guid.NewGuid()

                                };
                                db.ContactUpdate.Add(HistOfUpdate);
                            }
                        }
                    }

                    if (ListOfContacts.ListOfDeleteContacts != null)
                    {
                        foreach(var DeleteContact in ListOfContacts.ListOfDeleteContacts)
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
                            Description=e.TypeOfContact.Description,
                            idContact=e.idContact,
                            Value=e.TypeOfContact.Value,
                            TypeOfUpdate = e.TypeOfUpdate
                        }).ToArray();

                        var QueryForDelete = db.DeletedSubStr.Where(e => e.DateOfDelete > DateOfLastAction && e.TypeOfDeleted == SomeEnums.TypeOfSubs.Contact.ToString());
                        var ListOfDelete = QueryForDelete.Select(e => new MakeSubs.ListOfGuid
                        {
                            idGuid = e.idThingsDelete
                        }).ToArray();

                        return new MakeSubs.ContactsMake
                        {
                            success = true,
                            kol = ListPremises.Length + ListOfDelete.Length,
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
                    string TypeOfContact = db.TypeOfContact.Where((e) => e.UserContact.First().id== order.MainContactID).Select(e => e.Value).FirstOrDefault();
                    string ValuesOfContact = db.UserContact.Where(e => e.id == order.MainContactID).Select(e => e.Value).First();
                    string ContactInformatiom = $"{TypeOfContact} : {ValuesOfContact}";
                    string NameOfBrigade = db.ColoborationOfBrigade.Where(e => e.IdColoboration == order.IdColoboration).Select(e => e.Name).First();

                    var informationAboutAdress = db.AdressDescription.Where(e => e.idAdress == order.IdAdress).Select(e => new AdressDescription { idAdress = e.idAdress, AreaName = e.AreaName, Entrance = e.Entrance, CiryName = e.CiryName, Description = e.Description, House = e.House, MicroAreaName = e.MicroAreaName, NumberOfDelen = e.NumberOfDelen, RegionName = e.RegionName, Street = e.Street}).First();

                    string Adressinformation = $"{informationAboutAdress.RegionName} {informationAboutAdress.CiryName} {informationAboutAdress.MicroAreaName} {informationAboutAdress.Street} {informationAboutAdress.House} {informationAboutAdress.Entrance} {informationAboutAdress.NumberOfDelen}";
                    orders.Add(new WorkWithOrder.OrderInTable
                    {
                        BrigadeInformation = NameOfBrigade,
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
                return new WorkWithOrder.DataAboutAllOrder() { success = true,orders=orders,Count=orders.Count };
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
                        IdAdress = newOrderData.idClient,
                        idClient = newOrderData.idClient,
                        DateEnd = newOrderData.DateEnd,
                        IdColoboration = newOrderData.idColoboration,
                        Number = newOrderData.Number,
                        MainContactID = newOrderData.MainContactID,
                        IdOrder = newOrderData.idOrder,
                        DateStart = newOrderData.DataStart,
                        IdWorkerMake = newOrderData.idWorkerMake,
                        Description = newOrderData.Desc,
                    });
                    db.SaveChanges();
                    return new BaseResult { success = true};
                }
                catch(Exception ex)
                {
                    return new BaseResult { success=false, description=ex.Message};
                }                
            }, nameof(DBController), nameof(CreateNewOrder));
        }
        internal static object UpdateDataAboutOrder(WorkWithOrder.BaseOrderInformation updateDataAbOrder)
        {
            return Run((db) =>
            {
                try
                {
                    var selectWorkersOrder = db.OrderInformation.Where(e => e.IdOrder == updateDataAbOrder.idOrder).FirstOrDefault();
                    selectWorkersOrder.Status = updateDataAbOrder.Status;
                    selectWorkersOrder.AllSumma = updateDataAbOrder.Allsumma;
                    selectWorkersOrder.IdAdress = updateDataAbOrder.idClient;
                    selectWorkersOrder.idClient = updateDataAbOrder.idClient;
                    selectWorkersOrder.DateEnd = updateDataAbOrder.DateEnd;
                    selectWorkersOrder.IdColoboration = updateDataAbOrder.idColoboration;
                    selectWorkersOrder.Number = updateDataAbOrder.Number;
                    selectWorkersOrder.MainContactID = updateDataAbOrder.MainContactID;
                    selectWorkersOrder.DateStart = updateDataAbOrder.DataStart;
                    selectWorkersOrder.IdWorkerMake = updateDataAbOrder.idWorkerMake;
                    selectWorkersOrder.Description = updateDataAbOrder.Desc;
                    db.SaveChanges();
                    return new BaseResult { success = true };
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
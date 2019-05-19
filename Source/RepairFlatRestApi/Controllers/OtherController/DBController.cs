using RepairFlatRestApi.Models;
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
    public class DBController
    {
        #region Обработки при работе с данными пользователя
        /// <summary>
        /// Создание нового пользователя системы
        /// </summary>
        /// <param name="descriptionUser">Струкутура с информацией о данных новго пользователя</param>
        /// <returns></returns>
        internal static PersonDesctiption.ResultDescription CreateUser(PersonDesctiption.DescriptionOfNewUser descriptionUser)
        {
            return Run((db) =>
            {
                Guid IdNewUser = Guid.NewGuid();
                var user = new User
                {
                    idUser = IdNewUser,
                    Name = descriptionUser.Name,
                    LastName = descriptionUser.Lastname,
                    Patronymic = descriptionUser.Patronymic,
                    Pasport = descriptionUser.Pasport,
                    Female = descriptionUser.Female,
                    BirstDay = descriptionUser.Birstday,
                    TypeOfUser = descriptionUser.TypeOfUser
                };
                db.User.Add(user);
                db.SaveChanges();
                return new PersonDesctiption.ResultDescription()
                {
                    description = "Пользователь создан",
                    idUser = IdNewUser,
                    success = true,
                };
            }, nameof(DBController), nameof(CreateUser));
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

        #region Обработки при работе с подстовляемыми данными

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
                        var QueryWithOutUpdate = db.ServicesUpdate.Where((e) => e.DateOfUpdate > DateOfLastUpdate && e.TypeOfUpdate != SomeEnums.TypeOfUpdate.Delete.ToString());
                        var ListOfServises = QueryWithOutUpdate.Select(e => new MakeSubs.ListOfServisesWithTypeOfUpdate
                        {                            
                            idServises = e.OurServices.idServis,
                            UnitOfMeasue = e.OurServices.UnitOfMeasue,
                            Nomination = e.OurServices.Nomination,
                            TypeOfServises = e.OurServices.TypeOfServices,
                            Description = e.OurServices.Description,
                            Cost = e.OurServices.Cost,
                            TypeOfUpdate = e.TypeOfUpdate,
                        }).ToArray();
                        var QueryForDelete = db.ServicesUpdate.Where((e) => e.DateOfUpdate > DateOfLastUpdate && e.TypeOfUpdate == SomeEnums.TypeOfUpdate.Delete.ToString());
                        var ListOfDelete = QueryForDelete.Select(e => new MakeSubs.ListOfGuid
                        {//TODO Переписать
                            //idServises = e.IdDelete
                        }).ToArray();


                        return new MakeSubs.ServisesMake
                        {
                            success = true,
                            kol = ListOfServises.Length + ListOfDelete.Length,
                            DateOfMakeAnswer = DateTime.Now,
                            listOfServises = ListOfServises,
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


        internal static object MakeInserAndUpdateServises(MakeSubs.MakeUpdateOrInserNew makeUpdateOrInserNew)
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
                                TypeOfUpdate = SomeEnums.TypeOfUpdate.Add.ToString()                                
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
                                    TypeOfUpdate = SomeEnums.TypeOfUpdate.Update.ToString()
                                };
                                db.ServicesUpdate.Add(InformationAboutIsert);
                            }                             
                        }
                    }

                    if (makeUpdateOrInserNew.ListOfDeleteServises != null)
                    {
                        foreach (var WhatDelete in makeUpdateOrInserNew.ListOfDeleteServises)
                        {
                            var DeleteThings = db.OurServices.Where(e => e.idServis == WhatDelete.idServises).FirstOrDefault();
                            if (DeleteThings != null)
                            {
                                db.Entry(DeleteThings).Collection(c => c.ServicesUpdate).Load();
                                db.OurServices.Remove(DeleteThings);
                                db.ServicesUpdate.RemoveRange(db.ServicesUpdate.Where(c => c.IdServices == WhatDelete.idServises).ToArray());

                                ListOfDeleteCodes.Add(WhatDelete.idServises);
                            }
                        }
                    }
                    db.SaveChanges();
                    if (ListOfDeleteCodes.Count != 0)
                    {
                        foreach (var codes in ListOfDeleteCodes)
                        {
                            var InformationAboutIsert = new ServicesUpdate
                            {
                                //TODO  Переписать
                                //idServUpdate = Guid.NewGuid(),
                                //IdUser = makeUpdateOrInserNew.idUser,
                                //IdDelete = codes,
                                //DateOfUpdate = DateOfInsert,
                                //TypeOfUpdate = SomeEnums.TypeOfUpdate.Delete.ToString()
                            };
                            db.ServicesUpdate.Add(InformationAboutIsert);
                        }
                    }
                    db.SaveChanges();

                }
                catch(Exception ex)
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
                var listOfServise = db.OurServices.Select(e => new MakeSubs.ListOfServisesWithTypeOfUpdate
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
                    listOfServises = listOfServise,
                    DateOfMakeAnswer = DateTime.Now
                };
            }, nameof(DBController), nameof(AllServisesHave));
        }

        #endregion






        /// <summary>
        /// Базовый метод обработки запросов который также обрабатывает ошибки
        /// </summary>
        /// <param name="dbAction">Событие работы с базой данных</param>
        /// <param name="nameOfMethod">Наименование метода, необходимо для логирования</param>
        static void Run(Action<RepairFlatDB> dbAction, string nameOfMethod)
        {
            using (var db = new RepairFlatDB())
            {
                try
                {
                    dbAction(db);
                }
                catch (Exception ex)
                {
                    throw new Exception($"В методе {nameof(DBController)}::{nameOfMethod} произошла ошибка: <{ex.ToString()}>");
                }
            }
        }
        /// <summary>
        /// Метод обработки работы с базой данных, который также может возвращать некоторые данные
        /// </summary>
        /// <typeparam name="TResult">Тип данных который будет вернут от метода</typeparam>
        /// <param name="dbFunction">событие работы базы данных</param>
        /// <param name="NameOfClass">Наименование класса в котором происходит обработка метода, необходимо для логирования</param>
        /// <param name="nameOfMethod">Наименование метода, который вызывает данный метод, необходимо для ведения логов</param>
        /// <returns></returns>
        static TResult Run<TResult>(Func<RepairFlatDB, TResult> dbFunction, string NameOfClass, string nameOfMethod)
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
    }
}
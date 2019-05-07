﻿using RepairFlatRestApi.Models;
using RepairFlatRestApi.Models.DescriptionJSON;
using System;
using System.Linq;

namespace RepairFlatRestApi.Controllers.OtherController
{
    public class DBController
    {
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
                        typeofpolz = e1.User.TypeOfUser
                    }).FirstOrDefault();
                }
            }, nameof(DBController),nameof(Logining));
        }

        internal static BaseResult CreateLoginPerson(AuthDescription.RegisterLoginPerson informationAboutNewPerson)
        {
            return Run((db) =>
            {
                Guid idUser = informationAboutNewPerson.idUser;
                if (db.LoginInformation.Where(e => e.IdLog == idUser).FirstOrDefault() != null)
                {
                    return new BaseResult
                    {
                        success = false,
                        description="Для данного пользователя уже есть информация о логине и пароле!"
                    };
                }
                var InforMationAboutLoginInTable = db.LoginInformation.Select(e => e.Login == informationAboutNewPerson.login);
                if(db.LoginInformation.Where(e => e.Login == informationAboutNewPerson.login).FirstOrDefault() != null)
                {
                    return new BaseResult
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
                return new BaseResult
                {
                    success = true,
                    description = "Данные о логине и пароле добавлены"
                };
            }, nameof(DBController), nameof(CreateLoginPerson));
        }

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
                    Pasport=descriptionUser.Pasport,
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

        static TResult Run<TResult>(Func<RepairFlatDB, TResult> dbFunction, string NameOfClass,string nameOfMethod)
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
using RepairFlatRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RepairFlatRestApi.Models.DescriptionJSON;

namespace RepairFlatRestApi.Controllers.OtherController
{
    public class DBController
    {
        public static AuthDescription.ResultOfInformation Logining(AuthDescription.AskedInformation asked)
        {
            return Run((db) =>
            {
                string pass = HelperUS.PasswordDesript(asked.password);
                var InfrormationAboutLogin = db.LoginingInformation.Where(e => e.Login == asked.login && e.Password == pass);
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
                        idUser = e1.idUser,
                        typeofpolz = e1.User.TypeOfUser
                    }).FirstOrDefault();
                }
            }, nameof(Logining));
        }



        static void Run(Action<RepairFlatEntities> dbAction,string nameOfMethod)
        {
            using (var db = new RepairFlatEntities())
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

        static TResult Run<TResult>(Func<RepairFlatEntities, TResult> dbFunction, string nameOfMethod)
        {
            using (var db = new RepairFlatEntities())
            {
                try
                {
                    return dbFunction(db);
                }
                catch (Exception ex)
                {
                    throw new Exception($"В методе {nameof(DBController)}::{nameOfMethod} произошла ошибка: <{ex.ToString()}>");
                }
            }
        }

    }
}
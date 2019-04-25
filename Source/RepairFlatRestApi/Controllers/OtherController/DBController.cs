using RepairFlatRestApi.Models;
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
            }, nameof(DBController),nameof(Logining));
        }

        internal static BaseResult CreateLoginPerson(AuthDescription.RegisterLoginPerson informationAboutNewPerson)
        {
            return Run((db) =>
            {
                int idUser = Convert.ToInt32(informationAboutNewPerson.idPolz);
                if (db.LoginingInformation.Where(e => e.idUser == idUser).FirstOrDefault() != null)
                {
                    return new BaseResult
                    {
                        success = false,
                        description="Для данного пользователя уже есть информация о логине и пароле!"
                    };
                }
                var InforMationAboutLoginInTable = db.LoginingInformation.Select(e => e.Login == informationAboutNewPerson.login);
                if(db.LoginingInformation.Where(e => e.Login == informationAboutNewPerson.login).FirstOrDefault() != null)
                {
                    return new BaseResult
                    {
                        success = false,
                        description = "Логин уже исползуется"
                    };
                }
                var newLog = new LoginingInformation
                {
                    idUser = idUser,
                    Login = informationAboutNewPerson.login,
                    Password = HelperUS.PasswordDesript(informationAboutNewPerson.password)
                };
                db.LoginingInformation.Add(newLog);
                db.SaveChanges();
                return new BaseResult
                {
                    success = true,
                    description = "Данные о логине и пароле добавлены"
                };
            }, nameof(DBController), nameof(CreateLoginPerson));
        }

        internal static PersonDesctiption.ResultDescription CreateUser(PersonDesctiption.DescriptionOfNewPerson descriptionPerson)
        {
            return Run((db) =>
            {
                var user = new User
                {
                    Birstday = descriptionPerson.Birstday,
                    LastName = descriptionPerson.Lastname,
                    Name = descriptionPerson.Name,
                    Otchestv = descriptionPerson.Otchestv,
                    TypeOfUser = descriptionPerson.Type
                };
                db.User.Add(user);
                db.SaveChanges();
                var id = db.User.OrderBy(e => e.idUser)
                    .Where(e => e.Name == descriptionPerson.Name &&
                    e.Otchestv == descriptionPerson.Otchestv && e.LastName == descriptionPerson.Lastname).FirstOrDefault().idUser;
                return new PersonDesctiption.ResultDescription()
                {
                    description = "Пользолватель создан",
                    idPerson = id,
                    success = true,
                };
            }, nameof(DBController), nameof(CreateUser));
        }

        static void Run(Action<RepairFlatEntities> dbAction, string nameOfMethod)
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

        static TResult Run<TResult>(Func<RepairFlatEntities, TResult> dbFunction, string NameOfClass,string nameOfMethod)
        {
            using (var db = new RepairFlatEntities())
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
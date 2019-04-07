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
            using(var db= new RepairFlatEntities())
            {
                string pass = HelperUS.PasswordDesript(asked.password);
                var InfrormationAboutLogin = db.LoginingInformation.Where(e=>e.Login==asked.login&& e.Password== pass);
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
            }
        }

   
    }
}
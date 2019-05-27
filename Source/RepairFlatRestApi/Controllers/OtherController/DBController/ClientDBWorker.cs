using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RepairFlatRestApi.Models;
using RepairFlatRestApi.Models.DescriptionJSON;

namespace RepairFlatRestApi.Controllers.OtherController
{
    public class ClientDBWorker : DBController
    {
        internal static object CreateNewClient(PersonDesctiption.CreateNewClient descriptionPerson)
        {
            return Run((db) =>
            {
                try
                {
                    db.User.Add(new User
                    {
                        idUser = descriptionPerson.idUser,
                        Name = descriptionPerson.Name,
                        LastName = descriptionPerson.Lastname,
                        Patronymic = descriptionPerson.Patronymic,
                        Pasport = descriptionPerson.Pasport,
                        Female = descriptionPerson.Female,
                        BirstDay = descriptionPerson.Birstday,
                        TypeOfUser = descriptionPerson.TypeOfUser
                    });
                    db.ClientDetails.Add(new ClientDetails
                    {
                        IdClient = descriptionPerson.idUser,
                        Description = descriptionPerson.Desc,
                    });

                    if (descriptionPerson.ListOfContact.Count != 0)
                    {
                        foreach (var contact in descriptionPerson.ListOfContact)
                        {
                            db.UserContact.Add(new UserContact
                            {
                                DateAdd = contact.DateAdd,
                                Description = contact.Desctription,
                                id = contact.idContact,
                                idType = contact.idTypeOfContact,
                                idUser = contact.idUser,
                                Value = contact.Value
                            });
                        }
                    }

                    db.SaveChanges();
                    return new BaseResult() { success = true };
                }
                catch (Exception ex)
                {
                    return new BaseResult()
                    {
                        success = true,
                        description = ex.Message
                    };
                }
            });
        }

    }
}
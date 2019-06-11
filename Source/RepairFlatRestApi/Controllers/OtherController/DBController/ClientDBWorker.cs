using RepairFlatRestApi.Models;
using RepairFlatRestApi.Models.DescriptionJSON;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

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
                    db.User.AddOrUpdate(new User
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
                    db.ClientDetails.AddOrUpdate(new ClientDetails
                    {
                        IdClient = descriptionPerson.idUser,
                        Description = descriptionPerson.Desc,
                    });
                    if (descriptionPerson.ListOfContact != null)
                    {
                        if (descriptionPerson.ListOfContact.Any())
                        {

                            foreach (var contact in descriptionPerson.ListOfContact)
                            {
                                db.UserContact.AddOrUpdate(new UserContact
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
                    }
                    db.SaveChanges();
                    return new BaseResult() { success = true };
                }
                catch (Exception ex)
                {
                    return new BaseResult()
                    {
                        success = false,
                        description = ex.Message
                    };
                }
            });
        }

        internal static object GetDataAboutUser(Guid idUser)
        {
            return Run((db) =>
            {
                var data = db.ClientDetails.Where(ee => ee.IdClient == idUser).FirstOrDefault();
                if (data != null)
                {
                    PersonDesctiption.CreateNewClient client = new PersonDesctiption.CreateNewClient
                    {
                        idUser = data.IdClient,
                        Birstday = data.User.BirstDay,
                        Desc = data.Description,
                        Female = data.User.Female,
                        Lastname = data.User.LastName,
                        Name = data.User.Name,
                        Pasport = data.User.Pasport,
                        Patronymic = data.User.Patronymic
                    };
                    return client;
                }
                else
                {
                    return new PersonDesctiption.CreateNewClient { idUser = new Guid() };
                }
            });
        }

        internal static object UpdateDataAboutClient(PersonDesctiption.CreateNewClient descriptionPerson)
        {
            return Run((db) =>
            {
                try
                {
                    var DataToUpdate = db.User.Where(e1 => e1.idUser == descriptionPerson.idUser).First();
                    DataToUpdate.LastName = descriptionPerson.Lastname;
                    DataToUpdate.Name = descriptionPerson.Name;
                    DataToUpdate.Patronymic = descriptionPerson.Patronymic;
                    DataToUpdate.Pasport = descriptionPerson.Pasport;
                    DataToUpdate.ClientDetails.Description = descriptionPerson.Desc;
                    DataToUpdate.BirstDay = descriptionPerson.Birstday;
                    DataToUpdate.Female = descriptionPerson.Female;
                    if (descriptionPerson.ListOfContact != null)
                    {
                        foreach (var contact in descriptionPerson.ListOfContact)
                        {
                            db.UserContact.AddOrUpdate(new UserContact
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
                    return new BaseResult()
                    {
                        success = true
                    };

                }
                catch (Exception ex)
                {
                    return new BaseResult()
                    {
                        success = false,
                        description = ex.Message
                    };
                }
            });
        }

        internal static PersonDesctiption.ListOfClient CreateListOfClient()
        {
            return Run((db) =>
            {
                try
                {
                    var DataFromServer = db.User.Where(ee => ee.TypeOfUser == SomeEnums.TypeOfUser.Cl.ToString()).AsEnumerable();
                    if (DataFromServer != null)
                    {
                        List<PersonDesctiption.DataAboutClient> dataAboutClient = new List<PersonDesctiption.DataAboutClient>();
                        foreach (var clientInformation in DataFromServer)
                        {
                            PersonDesctiption.DataAboutClient clientInf = new PersonDesctiption.DataAboutClient
                            {
                                Birstday = clientInformation.BirstDay,
                                Name = clientInformation.Name,
                                idUser = clientInformation.idUser,
                                Female = clientInformation.Female,
                                Description = clientInformation.ClientDetails.Description,
                                Lastname = clientInformation.LastName,
                                Pasport = clientInformation.Pasport,
                                Patronymic = clientInformation.Patronymic
                            };
                            dataAboutClient.Add(clientInf);
                        }
                        return new PersonDesctiption.ListOfClient()
                        {
                            success = true,
                            description = "",
                            listOfClient = dataAboutClient
                        };
                    }
                    else
                    {
                        return new PersonDesctiption.ListOfClient()
                        {
                            success = false
                        };
                    }

                }
                catch (Exception ex)
                {
                    return new PersonDesctiption.ListOfClient()
                    {
                        success = false,
                        description = ex.Message
                    };
                }
            });
        }
    }
}
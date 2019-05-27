using RepairFlatRestApi.Models;
using RepairFlatRestApi.Models.DescriptionJSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static RepairFlatRestApi.Models.ContactModel;

namespace RepairFlatRestApi.Controllers.OtherController
{
    public  class WorkWithContactDBController: DBController
    {
        internal static BaseResult CreaNewContact(InformationAboutContact newContact)
        {
            return Run((db) =>
            {
                try
                {
                    db.UserContact.Add(new UserContact
                    {
                        DateAdd= newContact.DateAdd,
                        Description= newContact.Desctription,
                        id= newContact.idContact,
                        idType= newContact.idTypeOfContact,
                        idUser= newContact.idUser,
                        Value= newContact.Value
                    });
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
                        description = ex.ToString()
                    };
                }                
            });
        }

        internal static object UpdateContactData(InformationAboutContact updateContact)
        {
            return Run((db) => 
            {
                try
                {
                    var updatedContact = db.UserContact.Where(e => e.id == updateContact.idContact).First();
                    if (updateContact != null)
                    {
                        updatedContact.idType = updateContact.idTypeOfContact;
                        updatedContact.idUser = updateContact.idUser;
                        updatedContact.Value = updateContact.Value;
                        updatedContact.Description = updateContact.Desctription;
                        
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
                        description = ex.ToString()
                    };
                }
            });
        }       

        internal static object DeleteContact(Guid idContact)
        {
            return Run((db) => 
            {
                try
                {
                    var contactInf = db.UserContact.Where(e => e.id == idContact).First();
                    var select = db.OrderInformation.Where(e => e.MainContactID == idContact).First();
                    if (select == null)
                    {
                        db.UserContact.Remove(contactInf);
                        return new BaseResult { success = true };
                    }
                    else
                    {
                        return new BaseResult { success = false,description="Необходимо удалить связь с заказами данного контакта!" };
                    }
                }
                catch(Exception ex)
                {
                    return new BaseResult { success = false, description = $"Ошибка при работе {ex.Message}" };

                }
            });
        }

        internal static ListOfUserContactInf CreateListOfContactUser(Guid idUser)
        {
            return Run((db) =>
            {
                var que = db.UserContact.Where(e => e.idUser == idUser).AsEnumerable();
                if (que != null)
                {
                    List<ListOfContactUser> ListOfContact = new List<ListOfContactUser>();
                    foreach (var Contact in que)
                    {

                        ListOfContactUser contactUser = new ListOfContactUser
                        {
                            DateAdd = Contact.DateAdd,
                            idUser = Contact.idUser,
                            idContact = Contact.idUser,
                            Desctription = Contact.Description,
                            Value = Contact.Value,
                            idTypeOfContact = Contact.idType,
                            ValueTypeOfContact = Contact.TypeOfContact.Value
                        };
                        ListOfContact.Add(contactUser);

                    }
                    return new ListOfUserContactInf
                    {
                        success = true,
                        idUser = idUser,
                        listOfContact = ListOfContact
                    };
                }
                else
                {
                    return new ListOfUserContactInf
                    {
                        success = false,
                        idUser = idUser,
                        description = "Данные о контактах не найдены"
                    };
                }
            });
        }
    }
}
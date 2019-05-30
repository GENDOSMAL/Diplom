using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using RepairFlatRestApi.Models;
using RepairFlatRestApi.Models.DescriptionJSON;
using static RepairFlatRestApi.Models.MeasurmentModel;

namespace RepairFlatRestApi.Controllers.OtherController
{
    public class MeasurmentDBController : DBController
    {
        internal static object MakeDataAbouMeas(Guid idOrder)
        {
            return Run((db) =>
            {
                var LstOfMeas = db.OrderMeasurements.Where(ee => ee.IdOrder == idOrder).AsEnumerable();

                AllDataAbMeas allMeas = new AllDataAbMeas();
                foreach (var mesa in LstOfMeas)
                {
                    DataAboutMeasForTable data = new DataAboutMeasForTable
                    {
                        Description = mesa.Description,
                        Height = mesa.Height,
                        idMeasurment = mesa.idMeasurements,
                        Lenght = mesa.Lenght,
                        NameOfPremises = mesa.PremisesType.NameOfPremises,
                        PCelling = mesa.PCelling,
                        Pwalls = mesa.Pwalls,
                        Sfloor = mesa.Sfloor,
                        Swalls = mesa.Swalls,
                        Width = mesa.Width
                    };
                    allMeas.listofmeas.Add(data);

                }
                allMeas.success = true;
                return allMeas;
            });
        }

        internal static object MakeNewDataAboutMeas(DataAboutMeassFromDB newMeas)
        {
            return Run((db) =>
            {
                try
                {
                    OrderMeasurements measurements = new OrderMeasurements
                    {
                        Description = newMeas.Description,
                        Height = newMeas.Height,
                        idMeasurements = newMeas.idMeasurements,
                        IdOrder = newMeas.IdOrder,
                        idPremisesType = newMeas.idPremisesType,
                        Lenght = newMeas.Lenght,
                        PCelling = newMeas.PCelling,
                        Pwalls = newMeas.Pwalls,
                        Sfloor = newMeas.Sfloor,
                        Swalls = newMeas.Swalls,
                        Width = newMeas.Width,
                        
                    };
                    db.OrderMeasurements.Add(measurements);
                    if (newMeas.elementOfMeasurments != null)
                    {
                        foreach (var element in newMeas.elementOfMeasurments)
                        {
                            OrderElementOfMeasurments elementOfMeasurment = new OrderElementOfMeasurments
                            {
                                Description = element.Description,
                                Height = element.Height,
                                idElement = element.idElement,
                                idMeasurements = element.idMeasurements,
                                Lenght = element.Lenght,
                                POfElement = element.POfElement,
                                Width = element.Width,
                                WidthOfSlope = element.WidthOfSlope,
                                TypeOfElement = element.TypeOfElement
                            };
                            db.OrderElementOfMeasurments.Add(elementOfMeasurment);
                        }
                    }
                    db.SaveChanges();
                    return new BaseResult { success = true };
                }
                catch (Exception ex)
                {
                    return new BaseResult { success = false, description = ex.Message };
                }
            });
        }

        internal static object UpdateDataAboutMeas(DataAboutMeassFromDB updatedMeas)
        {
            return Run((db) =>
            {
                try
                {
                    var selectMeas = db.OrderMeasurements.Where(ee => ee.idMeasurements == updatedMeas.idMeasurements).First();
                    if (selectMeas != null)
                    {
                        selectMeas.Description = updatedMeas.Description;
                        selectMeas.Height = updatedMeas.Height;
                        selectMeas.IdOrder = updatedMeas.IdOrder;
                        selectMeas.idPremisesType = updatedMeas.idPremisesType;
                        selectMeas.Lenght = updatedMeas.Lenght;
                        selectMeas.PCelling = updatedMeas.PCelling;
                        selectMeas.Pwalls = updatedMeas.Pwalls;
                        selectMeas.Sfloor = updatedMeas.Sfloor;
                        selectMeas.Swalls = updatedMeas.Swalls;
                        selectMeas.Width = updatedMeas.Width;

                        if (selectMeas.OrderElementOfMeasurments != null)
                        {
                            foreach(var element in updatedMeas.elementOfMeasurments)
                            {
                                OrderElementOfMeasurments elementOfMeasurment = new OrderElementOfMeasurments
                                {
                                    Description = element.Description,
                                    Height = element.Height,
                                    idElement = element.idElement,
                                    idMeasurements = element.idMeasurements,
                                    Lenght = element.Lenght,
                                    POfElement = element.POfElement,
                                    Width = element.Width,
                                    WidthOfSlope = element.WidthOfSlope,
                                    TypeOfElement = element.TypeOfElement
                                };
                                db.OrderElementOfMeasurments.AddOrUpdate(elementOfMeasurment);
                            }
                        }
                        db.SaveChanges();
                        return new BaseResult { success = true};

                    }
                    else
                    {
                        return new BaseResult { success = false, description = "Данные не найдены" };
                    }
                }
                catch (Exception ex)
                {
                    return new BaseResult { success = false, description = ex.Message };
                }
            });
        }

        internal static object SelectDataAboutMeasurment(Guid idMeas)
        {
            return Run((db) =>
            {
                var selectData = db.OrderMeasurements.Where(ee => ee.idMeasurements == idMeas).First();
                if (selectData != null)
                {
                    List<ElementOfMeasurment> listOFElement = new List<ElementOfMeasurment>();
                    foreach (var element in selectData.OrderElementOfMeasurments)
                    {
                        ElementOfMeasurment elementOf = new ElementOfMeasurment
                        {
                            Description = element.Description,
                            Height = element.Height,
                            idElement = element.idElement,
                            idMeasurements = element.idMeasurements,
                            Lenght = element.Lenght,
                            POfElement = element.POfElement,
                            TypeOfElement = element.TypeOfElement,
                            Width = element.Width,
                            WidthOfSlope = element.WidthOfSlope
                        };
                        listOFElement.Add(elementOf);
                    }

                    return new DataAboutMeassFromDB
                    {
                        idMeasurements = selectData.idMeasurements,
                        Description = selectData.Description,
                        Height = selectData.Height,
                        IdOrder = selectData.IdOrder,
                        idPremisesType = selectData.idPremisesType,
                        Lenght = selectData.Lenght,
                        PCelling = selectData.PCelling,
                        Pwalls = selectData.Pwalls,
                        Sfloor = selectData.Sfloor,
                        Width = selectData.Width,
                        Swalls = selectData.Swalls,
                        elementOfMeasurments = listOFElement,
                        success = true
                    };
                }
                else
                {
                    return new DataAboutMeassFromDB { success = false };
                }

            });
        }
    }
}
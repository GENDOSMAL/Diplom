using RepairFlatRestApi.Models.DescriptionJSON;
using System;
using System.Collections.Generic;

namespace RepairFlatRestApi.Models
{
    public class MeasurmentModel
    {
        public class DataAboutMeasForTable
        {
            public Guid? idMeasurment;
            public string NameOfPremises;
            public string Description;
            public double? Lenght;
            public double? Width;
            public double? Height;
            public double? Pwalls;
            public double? PCelling;
            public double? Swalls;
            public double? Sfloor;
        }

        public class AllDataAbMeas : BaseResult
        {
            public List<DataAboutMeasForTable> listofmeas;
        }
        public class AllDataBaseData : BaseResult
        {
            public List<DataAboutMeassFromDB> DataAboutMeas;
        }


        public class DataAboutMeassFromDB : BaseResult
        {
            public System.Guid idMeasurements;
            public System.Guid? IdOrder;
            public Guid? idPremisesType;
            public string Description;
            public double? Lenght;
            public double? Width;
            public double? Height;
            public double? Pwalls;
            public double? PCelling;
            public double? Swalls;
            public double? Sfloor;
            public List<ElementOfMeasurment> elementOfMeasurments;
            public List<Guid> DeletedElement;
        }


        public class ElementOfMeasurment
        {
            public Guid idElement;
            public System.Guid? idMeasurements;
            public string TypeOfElement;
            public double? Lenght;
            public double? Width;
            public double? POfElement;
            public double? WidthOfSlope;
            public string Description;
            public double? Height;
        }
    }
}
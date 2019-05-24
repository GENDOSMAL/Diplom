using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairFlatRestApi.Models.DescriptionJSON
{
    public class AdressModel
    {
        public class AdressDesc:BaseResult
        {
            public Guid? IdAdress;
            public string RegionName;
            public string CityName;
            public string MicroAreaName;
            public string Street;
            public string House;
            public string Entrance;
            public string NumberOfDelen;
        }
    }
}
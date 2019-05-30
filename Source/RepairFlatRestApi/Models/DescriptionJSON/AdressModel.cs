using RepairFlatRestApi.Models.DescriptionJSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairFlatRestApi.Models
{
    public class AdressModel
    {
        public class AdressDesc:BaseResult
        {
            public Guid idAdress;
            public string RegionName;
            public string CityName;
            public string MicroAreaName;
            public string Street;
            public string House;
            public string Entrance;
            public string NumberOfDelen;
            public string AreaName;
            public string Desc;
        }
    }
}
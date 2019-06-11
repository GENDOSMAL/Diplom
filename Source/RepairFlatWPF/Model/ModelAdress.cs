using RepairFlat.Model;
using System;

namespace RepairFlatWPF
{
    class ModelAdress
    {
        public class DataAboutAdress : BaseResult
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

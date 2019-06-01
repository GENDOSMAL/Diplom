using RepairFlat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairFlatWPF
{
    class ModelAdress
    {
        public class DataAboutAdress:BaseResult
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

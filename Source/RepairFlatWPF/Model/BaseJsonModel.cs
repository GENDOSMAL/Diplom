using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairFlatWPF
{
    public class BaseJsonModel
    {
        public class BaseResult
        {
            public bool success;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string description;
        }
    }
}

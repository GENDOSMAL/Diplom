using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairFlatRestApi.Models.DescriptionJSON
{
    public class PersonDesctiption
    {
        public class DescriptionOfNewPerson
        {
            public string Name;
            public string Lastname;
            public string Otchestv;
            public DateTime Birstday;
            public string Type;
        }

        public class ResultDescription:BaseResult
        {
            public int idPerson;
        }
    }
}
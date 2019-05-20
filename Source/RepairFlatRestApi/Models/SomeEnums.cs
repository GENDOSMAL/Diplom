using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairFlatRestApi.Models
{
    public class SomeEnums
    {
        public enum TypeOfAction
        {
            Add,
            Update,
            Delete
        }

        public enum TypeOfUser
        {
            Cl,
            AD,
            MG,
            KW,
            BW,
            BB
        }

        public enum TypeOfSubs
        {
            Servises,
            Premises,
            Materials,
            Contact
        }

    }
}
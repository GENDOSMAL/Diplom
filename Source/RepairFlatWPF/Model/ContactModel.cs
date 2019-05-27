using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairFlat.Model
{
    public class ContactModel
    {
        public class InformationAboutContact
        {
            public Guid idContact;
            public Guid idUser;
            public Guid? idTypeOfContact;
            public string Value;
            public string Desctription;
            public DateTime? DateAdd;
        }

        public class ListOfContactUser: InformationAboutContact
        {
            
            public string ValueTypeOfContact;
        }

        public class ListOfUserContactInf:BaseResult
        {
            public Guid idUser;
            public List<ListOfContactUser> listOfContact;
        }
    }
}
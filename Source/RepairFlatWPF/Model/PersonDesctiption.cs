using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace RepairFlat.Model
{
    public class PersonDesctiption
    {
        public class DescriptionOfUser
        {
            public Guid? idUser;
            public string Name;
            public string Lastname;
            public string Patronymic;
            [JsonConverter(typeof(CustomDateTimeConverter))]
            public DateTime? Birstday;
            public string TypeOfUser;
            public string Pasport;
            public int? Female;
        }

        public class DataAboutClient : DescriptionOfUser
        {
            public string Description;
        }

        public class ListOfClient : BaseResult
        {
            public List<DataAboutClient> listOfClient;
        }


        public class CreateNewClient : DescriptionOfUser
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string Desc;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public List<ContactModel.InformationAboutContact> ListOfContact;
        }
        public class ResultDescription : BaseResult
        {
            public Guid idUser;
        }
        public class InformationAboutContact
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public List<ContactModel.InformationAboutContact> ListOfContact;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public List<Guid?> ListForDelete;
        }

        class CustomDateTimeConverter : IsoDateTimeConverter
        {
            public CustomDateTimeConverter()
            {
                base.DateTimeFormat = "dd.MM.yyyy";
            }
        }
    }
}
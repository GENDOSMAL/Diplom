using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace RepairFlatRestApi.Models.DescriptionJSON
{
    public class PersonDesctiption
    {
        /// <summary>
        /// Описание того, что будет принято о новом пользователе при его регистрации
        /// </summary>
        public class DescriptionOfUser
        {
            public Guid idUser;
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

        public class InformationAboutContact
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public List<ContactModel.InformationAboutContact> ListOfContact;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public List<Guid?> ListForDelete;
        }


        /// <summary>
        /// Описание того, что будет вернуто на запрос о создании нового пользователя
        /// </summary>
        public class ResultDescription : BaseResult
        {
            public Guid idUser;
        }
        /// <summary>
        /// Описание того, как будет выглядеть дата
        /// </summary>
        class CustomDateTimeConverter : IsoDateTimeConverter
        {
            public CustomDateTimeConverter()
            {
                base.DateTimeFormat = "dd.MM.yyyy";
            }
        }
    }
}
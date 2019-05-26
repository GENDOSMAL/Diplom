using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairFlatRestApi.Models.DescriptionJSON
{
    public class PersonDesctiption
    {
        /// <summary>
        /// Описание того, что будет принято о новом пользователе при его регистрации
        /// </summary>
        public class DescriptionOfNewUser
        {
            public Guid? idUser;
            public string Name;
            public string Lastname;
            public string Patronymic;
            [JsonConverter(typeof(CustomDateTimeConverter))]
            public DateTime Birstday;
            public string TypeOfUser;
            public string Pasport;
            public int? Female;
        }

        public class CreateNewClient
        {
            public Guid idUser;
            public string Name;
            public string Lastname;
            public string Patronymic;
            [JsonConverter(typeof(CustomDateTimeConverter))]
            public DateTime Birstday;
            public string TypeOfUser;
            public string Pasport;
            public int? Female;
            public string Desc;
        }
        /// <summary>
        /// Описание того, что будет вернуто на запрос о создании нового пользователя
        /// </summary>
        public class ResultDescription:BaseResult
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
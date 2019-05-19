using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairFlatRestApi.Models.DescriptionJSON
{
    public class MakeSubs : BaseResult
    {
        #region Make work with servises
        public class ServisesMake : BaseResult
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [JsonConverter(typeof(CustomDateTimeConverter))]
            public DateTime DateOfMakeAnswer;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public int kol;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public ListOfServisesWithTypeOfUpdate[] listOfServises;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public ListOfGuid[] ListOfDeleteServises;
        }

        public class ListOfServisesWithTypeOfUpdate
        {
            public Guid idServises;
            public string Nomination;
            public string TypeOfServises;
            public string UnitOfMeasue;
            public Nullable<decimal> Cost;
            public string Description;
            public string TypeOfUpdate;
        }

        public class ListOfServises
        {
            public Guid idServises;
            public string Nomination;
            public string TypeOfServises;
            public string UnitOfMeasue;
            public Nullable<decimal> Cost;
            public string Description;
        }

        public class MakeUpdateOrInserNew
        {
            public string DateOfMake;
            public Guid idUser;
            public ListOfServises[] listOfServisesUpdate;
            public ListOfServises[] ListOfServisesInsert;
            public ListOfGuid[] ListOfDeleteServises;
        }

        public class ListOfGuid
        {
            public Guid? idServises;
        }
        #endregion




        class CustomDateTimeConverter : IsoDateTimeConverter
        {
            public CustomDateTimeConverter()
            {
                base.DateTimeFormat = "dd.MM.yyyy HH:mm";
            }
        }
    }
}
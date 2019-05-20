using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairFlatWPF
{
    public class BaseResult
    {
        public bool success;
        public string description;
    }

    public class MakeSubs : BaseResult
    {
        #region Make work with servises
        /// <summary>
        /// То что будет вернуто на запрос клинента
        /// </summary>
        public class ServisesMake : BaseResult
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [JsonConverter(typeof(CustomDateTimeConverter))]
            public DateTime DateOfMakeAnswer;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public int kol;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public ListOfUpdatedServises[] ListOfServises;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public ListOfGuid[] ListOfDeleteServises;
        }
        /// <summary>
        /// Список обновленных/добавленных сервисов. Необходимо для отправки на клиент
        /// </summary>
        public class ListOfUpdatedServises: ListOfServises
        {
            public string TypeOfUpdate;
        }
        /// <summary>
        /// Список сервисов для вставки в бд
        /// </summary>
        public class ListOfServises
        {
            public Guid idServises;
            public string Nomination;
            public string TypeOfServises;
            public string UnitOfMeasue;
            public Nullable<decimal> Cost;
            public string Description;
        }
        /// <summary>
        /// То что присылает пользователь при необходимости каких либо действий с данными
        /// </summary>
        public class MakeUpdOrInsServises
        {
            public string DateOfMake;
            public Guid idUser;
            public ListOfServises[] listOfServisesUpdate;
            public ListOfServises[] ListOfServisesInsert;
            public ListOfGuid[] ListOfDeleteServises;
        }

        public class ListOfGuid
        {
            public Guid? idGuid;
        }
        #endregion

        #region Make work with premises

        public class PremisesMake : BaseResult
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [JsonConverter(typeof(CustomDateTimeConverter))]
            public DateTime DateOfMakeAnswer;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public int kol;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public ListOfPremisesUpd[] listOfPremises;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public ListOfGuid[] ListOfDeletePremises;
        }

        public class MakeUpdOrInsPremises
        {
            public string DateOfMake;
            public Guid idUser;
            public ListOfPremises[] listOfPremisesUpdate;
            public ListOfPremises[] ListOfPremisesInsert;
            public ListOfGuid[] ListOfDeletePremises;
        }

        public class ListOfPremises
        {
            public Guid? idPremises;
            public string Name;
            public string Description;
        }
        public class ListOfPremisesUpd: ListOfPremises
        {
            public string TypeOfUpdate;
        }
        #endregion

        #region Make work with materials

        public class MaterialsMake : BaseResult
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [JsonConverter(typeof(CustomDateTimeConverter))]
            public DateTime DateOfMakeAnswer;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public int kol;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public ListOfMaterialsUpd[] listOfMaterials;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public ListOfGuid[] ListOfDeleteMaterials;
        }

        public class MakeUpdOrInsMaterials
        {
            public string DateOfMake;
            public Guid idUser;
            public ListOfMaterials[] listOfMaterialsUpdate;
            public ListOfMaterials[] ListOfMaterialsInsert;
            public ListOfGuid[] ListOfDeletePremises;
        }

        public class ListOfMaterials
        {
            public Guid? idMaterials;
            public string NameOfMaterial;
            public string Description;
            public string UnitOfMeasue;
            public Nullable<decimal> Cost;
            public string TypeOfMaterial;
        }
        public class ListOfMaterialsUpd: ListOfMaterials
        {
            public string TypeOfUpdate;
        }
        #endregion

        #region Make work with Contacts
        public class ContactsMake : BaseResult
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            [JsonConverter(typeof(CustomDateTimeConverter))]
            public DateTime DateOfMakeAnswer;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public int kol;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public ListOfContactsUpd[] listOfContacts;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public ListOfGuid[] ListOfDeleteContacts;
        }

        public class MakeUpdOrInsContacts
        {
            public string DateOfMake;
            public Guid idUser;
            public ListOfContacts[] listOfContactsUpdate;
            public ListOfContacts[] ListOfContactsInsert;
            public ListOfGuid[] ListOfDeleteContacts;
        }

        public class ListOfContacts
        {
            public Guid? idContact;
            public string Value;
            public string Description;
        }

        public class ListOfContactsUpd : ListOfContacts
        {
            public string TypeOfUpdate;
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
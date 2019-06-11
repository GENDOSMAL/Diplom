using Newtonsoft.Json.Converters;
using RepairFlatRestApi.Models.DescriptionJSON;
using System;
using System.Collections.Generic;

namespace RepairFlatRestApi.Models
{
    public class OrderData
    {
        public class AllDataAboutOrder
        {
            public Guid? idOrder;
            public DateTime? DataStart;
            public int? Status;
            public string FIOClient;
            public string DataAboutAdress;
            public string Desc;
            public string ContactData;
            public decimal? AllSumma;
        }

        public class AllOrder : BaseResult
        {
            public List<AllDataAboutOrder> listOfOrders;
        }

        public class DataForUpdate : AllDataAboutOrder
        {
            public Guid? idAdress;
            public Guid? idContact;
            public Guid? idUser;
        }

        class CustomDateTimeConverter : IsoDateTimeConverter
        {
            public CustomDateTimeConverter()
            {
                base.DateTimeFormat = "dd.MM.yyyy HH:mm";
            }
        }

    }
}
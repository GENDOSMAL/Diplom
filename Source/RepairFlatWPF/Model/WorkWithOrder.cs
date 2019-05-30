using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairFlatWPF
{
    public class WorkWithOrder
    {
        #region Данные о заказе

        public class DataForRedact
        {
            public string FioClient;
            public string MainSS;
            public string DescAboutAdress;
            public string NameOfBrigade;
            public BaseOrderInformation IformationAboutOrder;

        }
        public class DataAboutAllOrder : BaseResult
        {
            public List<OrderInTable> orders;
            public int Count;
        }

        public class OrderInTable
        {
            public Guid? idOrder;
            public int? number;
            [JsonConverter(typeof(CustomDateTimeConverter))]
            public DateTime? DataStart;
            public int? Status;
            public string DataAboutContact;
            public string InformationAboutAdress;
            public string BrigadeInformation;
            public string FIOCLient;
            public string Desc;
            public decimal? money;
        }
        public class BaseOrderInformation : BaseResult
        {
            public Guid idOrder;
            public Guid? idAdress;
            public Guid? idWorkerMake;
            public Guid? idClient;
            public Guid? MainContactID;
            public Guid? idColoboration;
            //[JsonConverter(typeof(CustomDateTimeConverter))]
            public DateTime? DataStart;
            public int? Status;
            public int? Number;
            public decimal? Allsumma;
            public string Desc;
        }
        #endregion

        public class SelectAllDataForTable : BaseResult
        {
            public int Kol;
            public List<AllServis> listOfServises;
            public List<AllMaterials> listOfMaterials;
            public List<AllPremises> listOfPremises;
            public List<AllPremises> listOfPayment;

        }

        #region Данные об услугах
        public class AllServis
        {
            public Guid? idServis;
            public int? number;
            public string NameOfServis;
            public DateTime? DateStart;
            public double? Count;
            public double? Cost;
            public double? Summa;
            public string Desc;
        }

        public class DataAboutServis : BaseResult
        {
            public Guid? idServises;
            public Guid? idOrder;
            public Guid? idServis;
            public double? count;
            public double? cost;
            public string Description;
            public DateTime? DatePlaneStart;
        }


        #endregion

        #region Данные о материалах
        public class AllMaterials
        {
            public Guid? idMaterial;
            public int? number;
            public string NameOfMaterial;
            public double? Count;
            public double? Cost;
            public double? Summa;
            public string Desc;
            public string UnitOfMeasue;
        }

        public class NewMaterialInOrder
        {
            public Guid? idMaterialInTable;
            public Guid? idOrder;
            public Guid? idMaterial;
            public double? Count;
            public double? Cost;
            public string Desc;
        }
        #endregion

        #region Данные о помещениях
        public class AllPremises
        {
            public Guid? idMeasurment;
            public int? number;
            public string NameOf;
            public string Desc;
            public double? lenght;
            public double? Width;
            public double? Height;
            public double? PWalls;
            public double? PCelling;
            public double? SWalls;
            public double? PFloor;
        }

        public class NewMeasurment
        {

        }


        #endregion

        #region Данные об оплате

        public class AllPayment
        {

        }
        #endregion

        class CustomDateTimeConverter : IsoDateTimeConverter
        {
            public CustomDateTimeConverter()
            {
                base.DateTimeFormat = "dd.MM.yyyy";
            }
        }
    }
}

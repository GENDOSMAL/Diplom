using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace RepairFlatRestApi.Models.DescriptionJSON
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

        public class SelectAllDataForTable:BaseResult
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

        public class DataAboutServis:BaseResult
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

        public class SummaOfOrder : BaseResult
        {
            public Guid IdOrder;
            public decimal summaOfOrder;
            public decimal NeedPay;
        }

        #region Данные об оплате

        public class DataAboutPaymentInOrder:BaseResult
        {
            public List<PaymentInf> InfPayment;            
        }

        public class PaymentInf
        {
            public Guid idPayment;
            public string FioMake;
            public decimal? Summa;
            public string Description;
            public DateTime? DateOfMake;
        }
        #endregion

        #region Данные о заданиях

        public class DataAboutTaskInOrder : BaseResult
        {
            public List<TaskInf> InfTask;
            public decimal? Ostatok;
        }

        public class TaskInf
        {
            public Guid idTask;
            public DateTime? DateStart;
            public DateTime? DateEnd;
            public decimal? Summa;
            public string Description;
        }

        public class InformationAboutTask: BaseResult
        {
            public Guid idTask;
            public Guid idOrder;
            public DateTime? DateStart;
            public DateTime? DateEnd;
            public decimal? Summa;
            public string Description;
            public MaterialInfTask InfAbMat;
            public ServisesInfTask InfAbServ;
            public WorkersInfTask InfAbWorkers;
        }


        public class MaterialInfTask
        {
            public List<TaskMaterial> materialsInf;
            public List<Guid> DeleteMaterials;
        }

        public class ServisesInfTask
        {
            public List<TaskServises> ServisInf;
            public List<Guid> DeleteServises;
        }
        public class WorkersInfTask
        {
            public List<TaskWorker> WorkerInf;
            public List<Guid> DeleteWorkers;
        }

        public class TaskMaterial
        {
            public Guid idTaskMaterial;
            public Guid idMaterial;
            public double? count;
            public decimal? cost;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string NameOfMaterials;
        }

        public class TaskServises
        {
            public Guid idTaskServises;
            public Guid idServis;
            public double? count;
            public decimal? cost;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string NameOfServises;
        }

        public class TaskWorker
        {
            public Guid idTaskWorker;
            public Guid idWorker;
            public string Role;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string FioOfWorker;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string NameOfPost;
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
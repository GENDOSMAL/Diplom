using Newtonsoft.Json;
using RepairFlat.Model;
using System;
using System.Collections.Generic;
using static RepairFlatWPF.WorkWithOrder;

namespace RepairFlatWPF.Model
{
    class OrderDesc
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

        #region Данные об оплате

        public class DataAboutPaymentInOrder : BaseResult
        {
            public List<PaymentInf> InfPayment;
            public decimal summaOfOrder;
            public decimal NeedPay;
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

        public class InformationAboutTask : BaseResult
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
            public double? cost;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string NameOfMaterials;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public double? summa;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public int numb;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string desc;
        }

        public class TaskServises
        {
            public Guid idTaskServises;
            public Guid idServis;
            public double? count;
            public double? cost;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string NameOfServises;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public double? summa;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public int numb;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string desc;
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
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public int numb;
        }

        public class SummaOfOrder : BaseResult
        {
            public Guid IdOrder;
            public decimal summaOfOrder;
            public decimal NeedPay;
        }
        #endregion

        #region Справочная информация 
        public class MakeDataForSpravka : BaseResult
        {
            public string AreaName;
            public string CityName;
            public DateTime? DateMakeOrder;
            public DateTime? DateRozd;
            public string DescContact;
            public string Description;
            public string Entrance;
            public string House;
            public string LastName;
            public string MicroAreaName;
            public string Name;
            public string NumberOfDelen;
            public string RegionName;
            public int? StatusOfOrder;
            public string Street;
            public decimal? SummaOfOrder;
            public string Patronymic;
            public string TypeOfcontact;
            public string Value;
            public List<AllPremises> Premises;
        }

        public class MakeDogovor : BaseResult
        {
            public string Adress;
            public string ContactInf;
            public string FIOSmall;
            public string FullFIO;
            public string Inn;
            public string KPP;
            public string NameOfOrganization;
            public decimal? Summa;
        }
        public class MakeSmetaAll : BaseResult
        {
            public List<TaskMaterial> materialsInf;
            public List<TaskServises> ServisInf;
            public string AdressOfWork;
            public string Contact;
            public string FIO;
            public double SummaMat;
            public double SummaServ;

        }

        public class MakeDataAboutAllTaskInOrder : BaseResult
        {
            public string AdressOfWork;
            public string FIO;
            public string Contact;
            public List<DateAboutTask> TaskInf;
        }

        public class DateAboutTask
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
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public int numb;
        }

        #endregion
    }
}

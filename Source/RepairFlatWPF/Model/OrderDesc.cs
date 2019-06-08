using Newtonsoft.Json;
using RepairFlat.Model;
using System;
using System.Collections.Generic;

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
    }
}

using Newtonsoft.Json;
using RepairFlat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RepairFlat.Model.ContactModel;
using static RepairFlat.Model.PersonDesctiption;

namespace RepairFlatWPF.Model
{
    class WorkerDescriptiom
    {
        public class DescriptionOfWorker
        {
            public Guid idUser;
            public string LastName;
            public string Name;
            public string Patronymic;
            public string DateRozd;
            public string Female;
            public string TypeOfUser;
        }
        public class ListOfWorkersThatWork : DescriptionOfWorker
        {
            public Guid? idPost;
            public string NameOfPost;
            public decimal? Salary;
            public string DateOfEstanbilesnent;
        }

        public class ListOfAllWorkers : BaseResult
        {
            public List<DescriptionOfWorker> Workers;
        }


        public class ListOfWorkingWorker : BaseResult
        {
            public List<ListOfWorkersThatWork> Workers;
        }



        public class MakeNewWorker : DescriptionOfUser
        {
            public Guid idAdress;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public PersonDesctiption.InformationAboutContact InformatioAboutContact;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string DescOfAdress;
        }

        public class DataAboutPost
        {
            public Guid idEstabilisment;
            public Guid? idPost;
            public Guid idWorker;
            public decimal? Salary;
            public DateTime? DateOfOperate;
            public string TypeOfOperation;
            public string TypeOfUser;
        }

        public class PayWagesM
        {
            public Guid idGive;
            public Guid? idAdressat;
            public Guid idMakeWorker;
            public decimal SizeOfData;
            public DateTime Data;
            public string Descriptiom;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairFlatRestApi.Models.DescriptionJSON
{
    public class StatModel
    {
        public class DataAboutWorkerSalary
        {
            public string FIOWorker;
            public string NameOfPost;
            public decimal? SalaryOfWork;
            public DateTime? DateOfOperation;            
        }

        public class DataAboutWorkerPayment:BaseResult
        {
            public List<DataAboutWorkerSalary> InformationAboutWorker;
            public double Summa;

        }
    }
}
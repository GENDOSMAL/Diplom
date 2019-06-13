using RepairFlat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairFlatWPF.Model
{
    class StatModel
    {
        public class DataAboutWorkerSalary
        {
            public string FIOWorker;
            public string NameOfPost;
            public decimal? SalaryOfWork;
            public DateTime? DateOfOperation;
        }

        public class DataAboutWorkerPayment : BaseResult
        {
            public List<DataAboutWorkerSalary> InformationAboutWorker;
            public double Summa;

        }
    }
}

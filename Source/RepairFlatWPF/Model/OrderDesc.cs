using RepairFlat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        #endregion
    }
}

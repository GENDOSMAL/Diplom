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
    }
}

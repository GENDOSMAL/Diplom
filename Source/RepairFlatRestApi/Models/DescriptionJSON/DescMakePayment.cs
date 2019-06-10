using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairFlatRestApi.Models.DescriptionJSON
{
    public class DescMakePayment
    {
        public class DataAboutPayment:BaseResult
        {
            public System.Guid idInfPayment;
            public Nullable<System.Guid> idWorkerMake;
            public string NameOfWorkerMake;
            public string NameOfRecipient;
            public string InnOfOrganization;
            public string KppOfOrganization;
            public string BankOfPayment;
            public string CheckingAcount;
            public string BIK;
            public string YIN;
            public DateTime? DateOfMake;

        }
        public class MakeDataAboutPayment : BaseResult
        {
            public Guid idPayment;
            public Guid? idOrder;
            public Guid? idWorkerMake;
            public Guid? idInfForPayment;
            public decimal? summa;
            public DateTime? DateOfDoc;
            public string Desc;

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairFlatWPF.Model
{
    public static class SaveSomeData
    {
        public static Guid? IdUser;
        public static string TypeOfUser = "";
        public static string LastNameAndIni = "";
        public static object SomeObject { get; set; } = new object();
        public static bool MakeSomeOperation { get; set; } = false;
    }
}

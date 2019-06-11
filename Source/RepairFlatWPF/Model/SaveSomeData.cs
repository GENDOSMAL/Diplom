using System;

namespace RepairFlatWPF.Model
{
    public static class SaveSomeData
    {
        public static Guid? IdUser;
        public static string TypeOfUser = "";
        public static string LastNameAndIni = "";
        public static object SomeObject { get; set; } = new object();
        public static bool MakeSomeOperation { get; set; } = false;

        public static Guid idSubs { get; set; } = new Guid();
    }
}

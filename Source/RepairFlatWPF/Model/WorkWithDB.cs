using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairFlatWPF
{
    public class WorkWithDB
    {
        public class ColumnOfTable
        {
            public string NameOfCol { get; set; }
            public string TypeOfCol { get; set; }
            public bool IsPk { get; set; }
        }

        public class Tableses
        {
            public List<Table> Tables { get; set; }
        }


        public class Table
        {
            public string NameOfTable { get; set; }
            public List<ColumnOfTable> ColumnOfTable { get; set; }
        }

        public class RootObject
        {
            public List<Table> Tables { get; set; }
        }
    }
}

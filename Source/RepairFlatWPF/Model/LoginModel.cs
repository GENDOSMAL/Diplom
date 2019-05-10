using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairFlatWPF
{
    public class LoginModel: BaseJsonModel.BaseResult
    {
        public class MakeAuth
        {
            public string login;
            public string password;
        }
    }
}

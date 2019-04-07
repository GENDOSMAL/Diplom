using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace RepairFlatRestApi.Controllers.OtherController
{
    public class HelperUS
    {
        public static string PasswordDesript(string passwordFromCkient)
        {
            string firstBase64 = Encoding.UTF8.GetString(Convert.FromBase64String(passwordFromCkient));
            string result = Encoding.UTF8.GetString(Convert.FromBase64String(firstBase64));
            return result;
        }
    }
}
using Newtonsoft.Json;
using RepairFlatWPF.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RepairFlatWPF
{
    public class LoginWork
    {

        public  object MakeAuth(string Login, string Password)
        {

            string UrlSend = "http://repairflat.somee.com/api/main/auth";
            string Json = JsonConvert.SerializeObject(new LoginModel.MakeAuth() { login = Login, password = Password });
           
            var d = BaseWorkWithServer.CatchErrorWithPost(UrlSend, "POST", Json, nameof(LoginWork), nameof(MakeAuth));
            MessageBox.Show(d.ToString());
            //object deserializedProduct = JsonConvert.DeserializeObject<LoginModel>(d);
            return d;

        }

    }
}

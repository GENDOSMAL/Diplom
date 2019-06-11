using Newtonsoft.Json;
using System;

namespace RepairFlatWPF
{
    public class LoginModel : BaseJSONModel.BaseResult
    {
        public class MakeAuth
        {
            public string login;
            public string password;
        }

        public class WhatReturn : BaseJSONModel.BaseResult
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public Guid? idUser;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string typeofpolz;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string LastNameAndIni;
        }
    }
}

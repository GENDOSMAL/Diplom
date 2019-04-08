﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairFlatRestApi.Models.DescriptionJSON
{
    public class AuthDescription
    {
        public class AskedInformation
        {
            public string login;
            public string password;
        }

        public class ResultOfInformation
        {
            public bool sucess;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public int? idUser;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string typeofpolz;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string description;
        }

        public class RegisterLoginPerson
        {
            public string login;
            public string password;
            public string idPolz;
            public string typeofpolz;
        }

        public class BaseResult
        {
            public bool success;
            public string description;
        }
    }
}
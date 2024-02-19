﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BachelorOppgave.Controllers.PersonsModels
{
    public class UpdatePerson
    {
        [JsonIgnore]
        public int personID { get; set; }

        public string email { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string postnr { get; set; }
        public string address { get; set; }
        public string username { get; set; }
    }
}
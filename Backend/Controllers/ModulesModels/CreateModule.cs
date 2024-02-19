using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BachelorOppgave.Controllers.ModulesModels
{
    public class CreateModule
    {
        //integers
        public int teacherID { get; set; }
        public int adminID { get; set; }

        //strings
        public string institution { get; set; }
        public string description { get; set; }
        public string language { get; set; }
        public string title { get; set; }
        public string picture { get; set; }
        public string subject { get; set; }

        //etc
        public float price { get; set; }
        public float duration { get; set; }
    }
}

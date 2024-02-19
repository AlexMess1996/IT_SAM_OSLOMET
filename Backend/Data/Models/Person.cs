using System.Text.Json.Serialization;

namespace BachelorOppgave.Data.Models
{
    public class Person
    {
        //integers
        public int personID { get; set; }

        //strings
        public string email { get; set; }

        [JsonIgnore]
        public string password { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string postnr { get; set; }
        public string address { get; set; }
        public string username { get; set; }
        public string Token { get; set; }

        public int? adminID { get; set; }
        public int? teacherID { get; set; }

    }
}

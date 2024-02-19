using System.Text.Json.Serialization;

namespace BachelorOppgave.Controllers.AdminModels
{
    public class UpdateAdmin
    {
        //integers
        [JsonIgnore]
        public int adminID { get; set; }

        //strings
        public string birthnumber { get; set; }
        public string telNr { get; set; }
    }
}

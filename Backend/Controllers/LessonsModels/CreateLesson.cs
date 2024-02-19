using System.Text.Json.Serialization;

namespace BachelorOppgave.Controllers.LessonsModels
{
    public class CreateLesson
    {
        //integers
        [JsonIgnore]
        public int moduleID { get; set; }

        //strings
        public string type { get; set; }
        public string name { get; set; }
        public string details { get; set; }
    }
}

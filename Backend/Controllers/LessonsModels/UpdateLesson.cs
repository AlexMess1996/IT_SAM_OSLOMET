using System.Text.Json.Serialization;

namespace BachelorOppgave.Controllers.LessonsModels
{
    public class UpdateLesson
    {
        //integers
        [JsonIgnore]
        public int lessonID { get; set; }
        [JsonIgnore]
        public int moduleID { get; set; }

        //strings
        public string type { get; set; }
        public string name { get; set; }
        public string details { get; set; }
    }
}

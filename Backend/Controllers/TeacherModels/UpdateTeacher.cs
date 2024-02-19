using System.Text.Json.Serialization;

namespace BachelorOppgave.Controllers.TeacherModels
{
    public class UpdateTeacher
    {
        //integers
        [JsonIgnore]
        public int teacherID { get; set; }

        //strings
        public string title { get; set; }
        public string institution { get; set; }
        public string image { get; set; } //save as images url or file directory
    }
}

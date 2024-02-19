namespace BachelorOppgave.Data.Models
{
    public class Lesson
    {
        //integers
        public int lessonID { get; set; }
        public int moduleID { get; set; }

        //strings
        public string type { get; set; }
        public string name { get; set; }
        public string details { get; set; }
    }
}
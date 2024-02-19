namespace BachelorOppgave.Data.Models
{
    public class Teacher
    {
        //integers
        public int teacherID { get; set; }
        public int personID { get; set; }

        //strings
        public string title { get; set; }
        public string institution { get; set; }
        public string image { get; set; } //save as images url or file directory
    }
}

namespace LMSWebApi.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string Section { get; set; }
        public int AcademicYear { get; set; }

        public ICollection<Mark> Marks { get; set; }
    }
}

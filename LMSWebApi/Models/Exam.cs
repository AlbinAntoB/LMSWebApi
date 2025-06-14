namespace LMSWebApi.Models
{
    public class Exam
    {
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public ICollection<Subject> Subjects { get; set; }
        public ICollection<Mark> Marks { get; set; }
    }
}

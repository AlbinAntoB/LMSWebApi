namespace LMSWebApi.Models
{
    public class Mark
    {
        public int MarkId { get; set; }
        public int StudentId { get; set; }
        public int ExamId { get; set; }
        public int SubjectId { get; set; }
        public double MarksObtained { get; set; }
        public double MaximumMarks { get; set; }

        public Student Student { get; set; }
        public Exam Exam { get; set; }
        public Subject Subject { get; set; }
    }
}

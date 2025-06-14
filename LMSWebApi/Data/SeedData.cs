using LMSWebApi.Models;

namespace LMSWebApi.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (!context.Students.Any())
                {
                    var students = new List<Student>
                {
                    new Student { Name = "John Doe", ClassName = "10th", Section = "A", AcademicYear = 2025 },
                    new Student { Name = "Jane Smith", ClassName = "10th", Section = "B", AcademicYear = 2025 }
                };

                    context.Students.AddRange(students);
                    context.SaveChanges();
                }

                if (!context.Exams.Any())
                {
                    var exams = new List<Exam>
                {
                    new Exam { ExamName = "Mid Term" },
                    new Exam { ExamName = "Final Exam" }
                };

                    context.Exams.AddRange(exams);
                    context.SaveChanges();
                }

                if (!context.Subjects.Any())
                {
                    var subjects = new List<Subject>
                {
                    new Subject { SubjectName = "Math" },
                    new Subject { SubjectName = "Science" },
                    new Subject { SubjectName = "English" }
                };

                    context.Subjects.AddRange(subjects);
                    context.SaveChanges();
                }

                if (!context.Marks.Any())
                {
                    var marks = new List<Mark>
                {
                    new Mark { StudentId = 1, ExamId = 1, SubjectId = 1, MarksObtained = 80, MaximumMarks = 100 },
                    new Mark { StudentId = 1, ExamId = 1, SubjectId = 2, MarksObtained = 75, MaximumMarks = 100 },
                    new Mark { StudentId = 1, ExamId = 2, SubjectId = 1, MarksObtained = 90, MaximumMarks = 100 },
                    new Mark { StudentId = 1, ExamId = 2, SubjectId = 3, MarksObtained = 85, MaximumMarks = 100 },

                };

                    context.Marks.AddRange(marks);
                    context.SaveChanges();
                }
            }
        }
    }
}

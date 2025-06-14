using System.ComponentModel.DataAnnotations;

namespace LMSWebApi.Models.DTO
{
    public record SaveMarksDto
    {
        [Required(ErrorMessage = "StudentId is required.")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Marks are required.")]
        public List<MarkDto> Marks { get; set; }
    }

    public record MarkDto
    {
        [Required(ErrorMessage = "ExamId is required.")]
        public int ExamId { get; set; }

        [Required(ErrorMessage = "SubjectId is required.")]
        public int SubjectId { get; set; }

        [Required(ErrorMessage = "MarksObtained is required.")]
        public int MarksObtained { get; set; }

        [Required(ErrorMessage = "MaximumMarks is required.")]
        public int MaximumMarks { get; set; }
    }
}

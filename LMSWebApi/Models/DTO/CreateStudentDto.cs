using System.ComponentModel.DataAnnotations;

namespace LMSWebApi.Models.DTO
{
    public record CreateStudentDto
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Class name is required.")]
        public string ClassName { get; set; }

        [Required(ErrorMessage = "Section is required.")]
        public string Section { get; set; }

        [Required(ErrorMessage = "Academic year is required.")]
        public int AcademicYear { get; set; }
    }
}

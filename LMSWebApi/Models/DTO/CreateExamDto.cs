using System.ComponentModel.DataAnnotations;

namespace LMSWebApi.Models.DTO
{
    public record CreateExamDto
    {
        [Required]
        public string ExamName { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace LMSWebApi.Models.DTO
{
    public record CreateSubjectDto
    {
        [Required]
        public string SubjectName { get; set; }
    }
}

using LMSWebApi.Data;
using LMSWebApi.Models;
using LMSWebApi.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentMarkController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentMarkController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("SaveStudentMarks")]
        public async Task<IActionResult> SaveStudentMarks([FromBody] SaveMarksDto saveMarksDto)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var student = await _context.Students
                    .FirstOrDefaultAsync(s => s.StudentId == saveMarksDto.StudentId);

                if (student == null)
                {
                    return NotFound($"Student with ID {saveMarksDto.StudentId} not found.");
                }

                var marksToSave = new List<Mark>();

                foreach (var markDto in saveMarksDto.Marks)
                {
                    var mark = new Mark
                    {
                        StudentId = saveMarksDto.StudentId,
                        ExamId = markDto.ExamId,
                        SubjectId = markDto.SubjectId,
                        MarksObtained = markDto.MarksObtained,
                        MaximumMarks = markDto.MaximumMarks
                    };

                    marksToSave.Add(mark);
                }

                _context.Marks.AddRange(marksToSave);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Marks saved successfully!" });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

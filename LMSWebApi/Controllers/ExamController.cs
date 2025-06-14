using LMSWebApi.Data;
using LMSWebApi.Models;
using LMSWebApi.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExamController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("SaveExam")]
        public async Task<IActionResult> SaveExam([FromBody] CreateExamDto createExamDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var exam = new Exam
                {
                    ExamName = createExamDto.ExamName,
                };

                _context.Exams.Add(exam);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Exam saved successfully!", examId = exam.ExamId });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("GetExams")]
        public async Task<IActionResult> GetExams()
        {
            try
            {
                var exams = await _context.Exams.Select(s => new { s.ExamId, s.ExamName }).ToListAsync();

                if (exams.Any())
                {
                    return Ok(exams);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("GetExamById/{id}")]
        public async Task<IActionResult> GetExamById([FromRoute, Required] int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid exam ID.");
                }

                var exam = await _context.Exams.Select(s => new { s.ExamId, s.ExamName }).FirstOrDefaultAsync(e => e.ExamId == id);

                if (exam == null)
                {
                    return NotFound($"Exam with ID {id} not found.");
                }

                return Ok(exam);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

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
    public class SubjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SubjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("SaveSubject")]
        public async Task<IActionResult> SaveSubject([FromBody] CreateSubjectDto createSubjectDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var subject = new Subject
                {
                    SubjectName = createSubjectDto.SubjectName,
                };

                _context.Subjects.Add(subject);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Subject saved successfully!", subjectId = subject.SubjectId });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("GetSubjects")]
        public async Task<IActionResult> GetSubjects()
        {
            try
            {
                var subjects = await _context.Subjects.Select(s => new { s.SubjectId, s.SubjectName }).ToListAsync();

                if (subjects.Any())
                {
                    return Ok(subjects);
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

        [HttpPost("GetSubjectById/{id}")]
        public async Task<IActionResult> GetSubjectById([FromRoute, Required] int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid subject ID.");
                }

                var subject = await _context.Subjects.Select(s => new { s.SubjectId, s.SubjectName }).FirstOrDefaultAsync(s => s.SubjectId == id);

                if (subject == null)
                {
                    return NotFound($"Subject with ID {id} not found.");
                }

                return Ok(subject);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

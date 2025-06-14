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
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("SaveStudent")]
        public async Task<IActionResult> SaveStudent([FromBody] CreateStudentDto createStudentDto)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var student = new Student
                {
                    Name = createStudentDto.Name,
                    ClassName = createStudentDto.ClassName,
                    Section = createStudentDto.Section
                };

                _context.Students.Add(student);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Student saved successfully!", studentId = student.StudentId });

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("GetStudent")]
        public async Task<IActionResult> GetStudent([FromQuery, Required] CommonPrams commonPrams)
        {
            try
            {
                var students = await _context.Students
                               .Skip(commonPrams.skip)
                               .Take(commonPrams.take)
                               .Select(s => new { s.StudentId, s.Name, s.ClassName, s.Section, s.AcademicYear }).ToListAsync();

                if (students.Any())
                {
                    return Ok(students);
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

        [HttpPost("GetStudentById/{id}")]
        public async Task<IActionResult> GetStudentById([FromRoute, Required] int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid student ID.");
                }

                var student = await _context.Students.Select(s => new { s.StudentId, s.Name, s.ClassName, s.Section, s.AcademicYear }).FirstOrDefaultAsync(s => s.StudentId == id);

                if (student == null)
                {
                    return NotFound($"Student with ID {id} not found.");
                }

                return Ok(student);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}

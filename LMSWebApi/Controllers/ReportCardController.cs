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
    public class ReportCardController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReportCardController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllReportCards")]
        public async Task<IActionResult> GetAllReportCards([FromQuery, Required] CommonPrams commonPrams)
        {
            try
            {
                var reportCards = await _context.Students
                                 .Skip(commonPrams.skip)
                                 .Take(commonPrams.take)
                                 .Select(s => new
                                 {
                                     Id = s.StudentId,
                                     StudentName = s.Name,
                                     s.ClassName,
                                     s.Section,
                                     s.AcademicYear,
                                     Exams = s.Marks
                                         .GroupBy(m => m.ExamId)
                                         .Select(g => new
                                         {
                                             ExamId = g.Key,
                                             g.First().Exam.ExamName,
                                             Subjects = g.Select(m => new
                                             {
                                                 m.SubjectId,
                                                 m.Subject.SubjectName,
                                                 m.MarksObtained,
                                                 m.MaximumMarks
                                             }).ToList(),
                                             TotalMarksObtained = g.Sum(m => m.MarksObtained),
                                             TotalMaximumMarks = g.Sum(m => m.MaximumMarks),
                                             PercentageScore = (g.Sum(m => m.MarksObtained) / g.Sum(m => m.MaximumMarks) * 100).ToString("F2")
                                         })
                                         .ToList()
                                 })
                                 .ToListAsync();

                if (reportCards.Any())
                {
                    return Ok(reportCards);
                }
                else
                {
                    return NotFound("No report card found for the student.");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetReportCardByStudentId")]
        public async Task<IActionResult> GetReportCardByStudentId([FromQuery, Required] int studentId)
        {
            try
            {
                if (studentId <= 0)
                {
                    return BadRequest("Invalid student ID.");
                }

                var reportCards = await _context.Students
                    .Where(s => s.StudentId == studentId)
                    .Select(s => new
                    {
                        Id = s.StudentId,
                        StudentName = s.Name,
                        s.ClassName,
                        s.Section,
                        s.AcademicYear,
                        Exams = s.Marks
                            .GroupBy(m => m.ExamId)
                            .Select(g => new
                            {
                                ExamId = g.Key,
                                g.First().Exam.ExamName,
                                Subjects = g.Select(m => new
                                {
                                    m.SubjectId,
                                    m.Subject.SubjectName,
                                    m.MarksObtained,
                                    m.MaximumMarks
                                }).ToList(),
                                TotalMarksObtained = g.Sum(m => m.MarksObtained),
                                TotalMaximumMarks = g.Sum(m => m.MaximumMarks),
                                PercentageScore = (g.Sum(m => m.MarksObtained) / g.Sum(m => m.MaximumMarks) * 100).ToString("F2")
                            })
                            .ToList()
                    })
                    .ToListAsync();

                if (reportCards.Any())
                {
                    return Ok(reportCards);
                }
                else
                {
                    return NotFound("No report card found for the student.");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

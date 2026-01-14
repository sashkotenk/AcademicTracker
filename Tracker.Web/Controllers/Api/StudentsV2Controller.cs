using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tracker.Web.Data;

namespace Tracker.Web.Controllers.Api;

[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/students")]
public class StudentsV2Controller : ControllerBase
{
    private readonly AppDbContext _context;

    public StudentsV2Controller(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        // V2 повертає розширені дані (наприклад, середній бал)
        var students = await _context.Students
            .Include(s => s.Group)
            .Include(s => s.Submissions)
            .Select(s => new
            {
                s.Id,
                s.FullName,
                Group = s.Group.Name,
                Email = s.Email,
                AverageScore = s.Submissions.Any() ? s.Submissions.Average(sub => sub.Score) : 0
            })
            .ToListAsync();

        return Ok(students);
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tracker.Web.Data;
using Tracker.Core;

namespace Tracker.Web.Controllers.Api;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/students")]
public class StudentsV1Controller : ControllerBase
{
    private readonly AppDbContext _context;

    public StudentsV1Controller(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var students = await _context.Students
            .Include(s => s.Group)
            .Select(s => new
            {
                s.Id,
                s.FullName,
                Group = s.Group.Name
            })
            .ToListAsync();

        return Ok(students);
    }
}
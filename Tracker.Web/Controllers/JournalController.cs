using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Tracker.Web.Data;
using Tracker.Core;

namespace Tracker.Web.Controllers;

[Authorize] // Доступ тільки для авторизованих (через Auth0)
public class JournalController : Controller
{
    private readonly AppDbContext _context;

    // ЗАМІСТЬ AcademicJournal ми тепер використовуємо AppDbContext
    public JournalController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Беремо студентів з бази даних + їхні групи та оцінки
        var students = await _context.Students
            .Include(s => s.Group)
            .Include(s => s.Submissions)
            .ToListAsync();

        return View(students);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(string name, string email, int grade)
    {
        if (ModelState.IsValid)
        {
            // 1. Створюємо нового студента
            // (Для спрощення прив'язуємо до групи з ID=1, бо вона була створена автоматично)
            var newStudent = new Student
            {
                FullName = name,
                Email = email,
                GroupId = 1
            };

            _context.Students.Add(newStudent);
            await _context.SaveChangesAsync(); // Зберігаємо, щоб база видала ID студенту

            // 2. Якщо оцінка > 0, створюємо запис про здачу (Submission)
            if (grade > 0)
            {
                var submission = new Submission
                {
                    StudentId = newStudent.Id,
                    CourseWorkTitle = "Initial Work",
                    Score = grade,
                    SubmissionDate = DateTime.Now
                };
                _context.Submissions.Add(submission);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
        return View();
    }

    public async Task<IActionResult> Stats()
    {
        // Рахуємо середній бал напряму з бази даних
        // Беремо всі оцінки (Submissions)
        var allSubmissions = await _context.Submissions.ToListAsync();

        double average = 0;
        if (allSubmissions.Any())
        {
            average = allSubmissions.Average(s => s.Score);
        }

        return View(average);
    }
}
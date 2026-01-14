using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tracker.Core;

namespace Tracker.Web.Controllers;

[Authorize] // Доступ тільки для авторизованих (Вимога 1d)
public class JournalController : Controller
{
    private readonly AcademicJournal _journal;

    public JournalController(AcademicJournal journal)
    {
        _journal = journal;
    }

    // 1. Сторінка списку
    public IActionResult Index()
    {
        var entries = _journal.GetEntries();
        return View(entries);
    }

    // 2. Сторінка додавання
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(string name, string email, int grade)
    {
        if (grade < 0 || grade > 100)
        {
            ModelState.AddModelError("grade", "Grade must be 0-100");
            return View();
        }

        var learner = new Learner(name, email);
        _journal.RegisterGrade(learner, grade);
        return RedirectToAction("Index");
    }

    // 3. Сторінка статистики
    public IActionResult Stats()
    {
        double avg = _journal.CalculateGroupAverage();
        return View(avg);
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tracker.Web.Data;
using Tracker.Web.Models; // Підключаємо наші ViewModel
using Tracker.Core;

namespace Tracker.Web.Controllers;

public class SearchController : Controller
{
    private readonly AppDbContext _context;

    public SearchController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string? searchText, DateTime? dateFrom, DateTime? dateTo, int? groupId)
    {
        // 1. Починаємо запит до бази даних (Submissions - центральна таблиця)
        var query = _context.Submissions.AsQueryable();

        // 2. Виконуємо JOIN (Вимога 2.iv: "at least two JOIN operations")
        // Приєднуємо Студента, а до Студента приєднуємо Групу
        query = query
            .Include(s => s.Student)
            .ThenInclude(st => st!.Group);

        // 3. Фільтрація по тексту (Вимога 2.iii: "beginning of the value and the end")
        if (!string.IsNullOrEmpty(searchText))
        {
            query = query.Where(s =>
                s.Student!.FullName.StartsWith(searchText) || // Ім'я починається з...
                s.CourseWorkTitle.EndsWith(searchText));      // Або назва роботи закінчується на...
        }

        // 4. Фільтрація по даті (Вимога 2.i: "search by date")
        if (dateFrom.HasValue)
            query = query.Where(s => s.SubmissionDate >= dateFrom.Value);

        if (dateTo.HasValue)
            query = query.Where(s => s.SubmissionDate <= dateTo.Value);

        // 5. Фільтрація по списку (Вимога 2.ii: "search by list")
        if (groupId.HasValue)
        {
            query = query.Where(s => s.Student!.GroupId == groupId.Value);
        }

        // Виконуємо запит
        var results = await query.ToListAsync();

        // Завантажуємо список груп для випадаючого меню
        var groups = await _context.Groups.ToListAsync();

        // Формуємо модель для відображення
        var model = new SearchViewModel
        {
            Results = results,
            Groups = groups,
            SearchText = searchText,
            DateFrom = dateFrom,
            DateTo = dateTo,
            SelectedGroupId = groupId
        };

        return View(model);
    }
}
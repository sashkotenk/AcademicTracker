using Tracker.Core;

namespace Tracker.Web.Models;

public class SearchViewModel
{
    // Поля для пошуку (фільтри)
    public string? SearchText { get; set; } // Для пошуку по імені/роботі
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public int? SelectedGroupId { get; set; }

    // Дані для випадаючого списку груп
    public List<Group> Groups { get; set; } = new();

    // Результати пошуку (те, що покажемо в таблиці)
    public List<Submission> Results { get; set; } = new();
}
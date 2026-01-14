using System.ComponentModel.DataAnnotations;

namespace Tracker.Core;

public class Group
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty; // Наприклад "IPZ-31"

    // Зв'язок: Одна група має багато студентів
    public List<Student> Students { get; set; } = new();
}
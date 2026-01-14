using System.ComponentModel.DataAnnotations;

namespace Tracker.Core;

public class Student
{
    public int Id { get; set; }

    [Required]
    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    // Зв'язок: Студент належить до однієї Групи
    public int GroupId { get; set; }
    public Group? Group { get; set; }

    // Зв'язок: Студент має багато зданих робіт
    public List<Submission> Submissions { get; set; } = new();
}
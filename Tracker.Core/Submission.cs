namespace Tracker.Core;

public class Submission
{
    public int Id { get; set; }
    public string CourseWorkTitle { get; set; } = string.Empty; // Назва роботи (напр. "Lab 1")
    public int Score { get; set; }
    public DateTime SubmissionDate { get; set; } // Дата здачі

    // Зв'язок: Робота належить одному студенту
    public int StudentId { get; set; }
    public Student? Student { get; set; }
}
namespace Tracker.Mobile.Models;

public class Student
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public int GroupId { get; set; }
    // Для відображення в списку
    public string DisplayInfo => $"{FullName} (Group: {GroupId})";
}
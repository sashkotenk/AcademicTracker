using Microsoft.EntityFrameworkCore;
using Tracker.Core;

namespace Tracker.Web.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Реєструємо таблиці
    public DbSet<Group> Groups { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Submission> Submissions { get; set; }

    // Заповнення початковими даними (Вимога лаби)
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 1. Створюємо групи
        modelBuilder.Entity<Group>().HasData(
            new Group { Id = 1, Name = "IPZ-31" },
            new Group { Id = 2, Name = "IPZ-32" }
        );

        // 2. Створюємо студентів
        modelBuilder.Entity<Student>().HasData(
            new Student { Id = 1, FullName = "Ivan Petrenko", Email = "ivan@knu.ua", GroupId = 1 },
            new Student { Id = 2, FullName = "Maria Sydor", Email = "maria@knu.ua", GroupId = 1 },
            new Student { Id = 3, FullName = "Petro Boiko", Email = "petro@knu.ua", GroupId = 2 }
        );

        // 3. Створюємо оцінки
        modelBuilder.Entity<Submission>().HasData(
            new Submission { Id = 1, StudentId = 1, CourseWorkTitle = "Lab 1", Score = 95, SubmissionDate = DateTime.Now.AddDays(-10) },
            new Submission { Id = 2, StudentId = 1, CourseWorkTitle = "Lab 2", Score = 90, SubmissionDate = DateTime.Now.AddDays(-5) },
            new Submission { Id = 3, StudentId = 2, CourseWorkTitle = "Lab 1", Score = 88, SubmissionDate = DateTime.Now.AddDays(-8) }
        );
    }
}
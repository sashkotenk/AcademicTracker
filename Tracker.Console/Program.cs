<<<<<<< HEAD
﻿using System;
using Tracker.Core;

namespace Tracker.ConsoleApp
{
    class Program
    {
        // Створюємо екземпляр нашого журналу (з Tracker.Core)
        private static AcademicJournal _journal = new AcademicJournal();

        static void Main(string[] args)
        {
            bool isRunning = true;

            while (isRunning)
            {
                // Очищуємо консоль для охайності
                Console.Clear();
                PrintHeader();

                Console.WriteLine("1. Add New Student Grade");
                Console.WriteLine("2. Show All Records");
                Console.WriteLine("3. Show Group Statistics");
                Console.WriteLine("0. Exit");
                Console.Write("\nSelect an option: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddNewRecord();
                        break;
                    case "2":
                        ShowAllRecords();
                        break;
                    case "3":
                        ShowStatistics();
                        break;
                    case "0":
                        isRunning = false;
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        PrintMessage("Invalid option. Please try again.", ConsoleColor.Red);
                        break;
                }
            }
        }

        private static void AddNewRecord()
        {
            Console.WriteLine("\n--- Add New Record ---");

            try
            {
                // Ввід імені
                Console.Write("Enter Student Name: ");
                string name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    PrintMessage("Name cannot be empty!", ConsoleColor.Red);
                    return;
                }

                // Ввід пошти
                Console.Write("Enter Email: ");
                string email = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(email))
                {
                    PrintMessage("Email cannot be empty!", ConsoleColor.Red);
                    return;
                }

                // Ввід оцінки з перевіркою
                Console.Write("Enter Grade (0-100): ");
                if (!int.TryParse(Console.ReadLine(), out int grade))
                {
                    PrintMessage("Invalid grade format. Please enter a number.", ConsoleColor.Red);
                    return;
                }

                // Створення об'єкта та збереження (використовуємо логіку з Lab 1)
                var learner = new Learner(name, email);
                _journal.RegisterGrade(learner, grade);

                PrintMessage("✅ Student added successfully!", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                // Ловимо помилки валідації з Tracker.Core
                PrintMessage($"Error: {ex.Message}", ConsoleColor.Red);
            }
        }

        private static void ShowAllRecords()
        {
            Console.WriteLine("\n--- Class Register ---");
            var entries = _journal.GetEntries();

            if (entries.Count == 0)
            {
                Console.WriteLine("No records found.");
            }
            else
            {
                Console.WriteLine($"{"Name",-20} | {"Email",-25} | {"Grade"}");
                Console.WriteLine(new string('-', 55));

                foreach (var entry in entries)
                {
                    Console.WriteLine($"{entry.Key.FullName,-20} | {entry.Key.Email,-25} | {entry.Value}");
                }
            }

            Pause();
        }

        private static void ShowStatistics()
        {
            Console.WriteLine("\n--- Statistics ---");
            try
            {
                double average = _journal.CalculateGroupAverage();

                Console.Write("Class Average Score: ");

                // Змінюємо колір залежно від результату
                if (average >= 90) Console.ForegroundColor = ConsoleColor.Green;
                else if (average < 60) Console.ForegroundColor = ConsoleColor.Red;
                else Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine($"{average:F2}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.WriteLine("N/A (No grades yet)");
            }

            Pause();
        }

        // Допоміжні методи для краси
        private static void PrintHeader()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("========================================");
            Console.WriteLine("   ACADEMIC TRACKER PRO (LINUX CLI)     ");
            Console.WriteLine("========================================");
            Console.ResetColor();
        }

        private static void PrintMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
            Pause();
        }

        private static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
=======
﻿using System;
using Tracker.Core;

namespace Tracker.ConsoleApp
{
    class Program
    {
        // Створюємо екземпляр нашого журналу (з Tracker.Core)
        private static AcademicJournal _journal = new AcademicJournal();

        static void Main(string[] args)
        {
            bool isRunning = true;

            while (isRunning)
            {
                // Очищуємо консоль для охайності
                Console.Clear();
                PrintHeader();

                Console.WriteLine("1. Add New Student Grade");
                Console.WriteLine("2. Show All Records");
                Console.WriteLine("3. Show Group Statistics");
                Console.WriteLine("0. Exit");
                Console.Write("\nSelect an option: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddNewRecord();
                        break;
                    case "2":
                        ShowAllRecords();
                        break;
                    case "3":
                        ShowStatistics();
                        break;
                    case "0":
                        isRunning = false;
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        PrintMessage("Invalid option. Please try again.", ConsoleColor.Red);
                        break;
                }
            }
        }

        private static void AddNewRecord()
        {
            Console.WriteLine("\n--- Add New Record ---");

            try
            {
                // Ввід імені
                Console.Write("Enter Student Name: ");
                string name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    PrintMessage("Name cannot be empty!", ConsoleColor.Red);
                    return;
                }

                // Ввід пошти
                Console.Write("Enter Email: ");
                string email = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(email))
                {
                    PrintMessage("Email cannot be empty!", ConsoleColor.Red);
                    return;
                }

                // Ввід оцінки з перевіркою
                Console.Write("Enter Grade (0-100): ");
                if (!int.TryParse(Console.ReadLine(), out int grade))
                {
                    PrintMessage("Invalid grade format. Please enter a number.", ConsoleColor.Red);
                    return;
                }

                // Створення об'єкта та збереження (використовуємо логіку з Lab 1)
                var learner = new Learner(name, email);
                _journal.RegisterGrade(learner, grade);

                PrintMessage("✅ Student added successfully!", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                // Ловимо помилки валідації з Tracker.Core
                PrintMessage($"Error: {ex.Message}", ConsoleColor.Red);
            }
        }

        private static void ShowAllRecords()
        {
            Console.WriteLine("\n--- Class Register ---");
            var entries = _journal.GetEntries();

            if (entries.Count == 0)
            {
                Console.WriteLine("No records found.");
            }
            else
            {
                Console.WriteLine($"{"Name",-20} | {"Email",-25} | {"Grade"}");
                Console.WriteLine(new string('-', 55));

                foreach (var entry in entries)
                {
                    Console.WriteLine($"{entry.Key.FullName,-20} | {entry.Key.Email,-25} | {entry.Value}");
                }
            }

            Pause();
        }

        private static void ShowStatistics()
        {
            Console.WriteLine("\n--- Statistics ---");
            try
            {
                double average = _journal.CalculateGroupAverage();

                Console.Write("Class Average Score: ");

                // Змінюємо колір залежно від результату
                if (average >= 90) Console.ForegroundColor = ConsoleColor.Green;
                else if (average < 60) Console.ForegroundColor = ConsoleColor.Red;
                else Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine($"{average:F2}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.WriteLine("N/A (No grades yet)");
            }

            Pause();
        }

        // Допоміжні методи для краси
        private static void PrintHeader()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("========================================");
            Console.WriteLine("   ACADEMIC TRACKER PRO (LINUX CLI)     ");
            Console.WriteLine("========================================");
            Console.ResetColor();
        }

        private static void PrintMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
            Pause();
        }

        private static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
>>>>>>> 27d15c0 (Lab 3)
}
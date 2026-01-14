using System;
using System.Windows.Forms;

namespace Tracker.Desktop
{
    // Статичний клас програми.
    static class Program
    {
        // Атрибут [STAThread] потрібен для коректної роботи COM-компонентів Windows.
        // Без нього WinForms може працювати некоректно.
        [STAThread]
        static void Main()
        {
            // Налаштування для коректного відображення на екранах з високим DPI (High DPI Support).
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Запуск головного циклу повідомлень (Message Loop) з нашою формою.
            Application.Run(new MainDashboard());
        }
    }
}
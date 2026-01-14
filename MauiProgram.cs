using Microsoft.Extensions.Logging;
using Microcharts.Maui; // <--- 1. ДОДАЙТЕ ЦЕЙ USING

namespace Tracker.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMicrocharts() // <--- 2. ОБОВ'ЯЗКОВО ДОДАЙТЕ ЦЕЙ РЯДОК
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Реєстрація ваших сервісів (якщо ви їх додавали раніше)
        builder.Services.AddSingleton<Services.ApiService>();
        builder.Services.AddSingleton<ViewModels.StudentsViewModel>();
        builder.Services.AddTransient<Views.StudentsPage>();

        return builder.Build();
    }
}
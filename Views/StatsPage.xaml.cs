using Microcharts;
using Microcharts.Maui;
using SkiaSharp;

// ВАЖЛИВО: Цей рядок має співпадати з тим, що в x:Class у XAML файлі
namespace Tracker.Mobile.Views;

public partial class StatsPage : ContentPage
{
    public StatsPage()
    {
        InitializeComponent();

        // Хардкод даних для демонстрації
        var entries = new[]
        {
            new ChartEntry(200) { Label = "IPZ-31", ValueLabel = "200", Color = SKColor.Parse("#2c3e50") },
            new ChartEntry(400) { Label = "IPZ-32", ValueLabel = "400", Color = SKColor.Parse("#77d065") },
            new ChartEntry(100) { Label = "IPZ-33", ValueLabel = "100", Color = SKColor.Parse("#b455b6") }
        };

        chartView.Chart = new BarChart { Entries = entries };
    }
}
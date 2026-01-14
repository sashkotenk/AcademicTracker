using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
namespace Tracker.Web.Controllers;

public class SimulationController : Controller
{
    private static readonly ActivitySource ActivitySource = new("AcademicTracker");

    public async Task<IActionResult> SlowOperation()
    {
        using (var activity = ActivitySource.StartActivity("HeavyWork"))
        {
            activity?.SetTag("custom.info", "Lab6 Demo");
            await Task.Delay(2000); // Імітація затримки 2 сек
        }
        return Content("Done!");
    }
}
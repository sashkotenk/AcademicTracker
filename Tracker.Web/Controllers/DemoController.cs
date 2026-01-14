using Microsoft.AspNetCore.Mvc;
namespace Tracker.Web.Controllers;
public class DemoController : Controller
{
    public IActionResult Index() => View();
}
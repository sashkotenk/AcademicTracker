using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Auth0.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace Tracker.Web.Controllers;

public class AccountController : Controller
{
    // Вхід через Auth0
    public async Task Login(string returnUrl = "/")
    {
        var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
            .WithRedirectUri(returnUrl)
            .Build();

        await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
    }

    // Вихід
    [Authorize]
    public async Task Logout()
    {
        // Додаємо ?? "/", щоб гарантувати, що результат не буде null
        var returnUrl = Url.Action("Index", "Home") ?? "/";

        var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
            .WithRedirectUri(returnUrl)
            .Build();

        await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    // Профіль користувача (Вимога №2 - Валідація полів)
    [Authorize]
    [HttpGet]
    public IActionResult Profile()
    {
        return View(new UserProfileViewModel
        {
            Username = User.Identity?.Name ?? "Unknown",
            Email = User.Claims.FirstOrDefault(c => c.Type == "name")?.Value ?? ""
        });
    }

    [Authorize]
    [HttpPost]
    public IActionResult Profile(UserProfileViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        // Тут ми б зберігали дані у БД. Для лаби - просто покажемо успіх.
        TempData["Message"] = "Profile updated successfully!";
        return View(model);
    }
}

// Модель для валідації (Вимога №2)
public class UserProfileViewModel
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(500, ErrorMessage = "Name is too long")]
    [Display(Name = "Full Name")]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"^\+380\d{9}$", ErrorMessage = "Phone must be in format +380xxxxxxxxx")]
    [Display(Name = "Phone Number")]
    public string Phone { get; set; } = string.Empty;
}
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Tracker.Core;

var builder = WebApplication.CreateBuilder(args);

// 1. Налаштування OAuth2 (Auth0)
builder.Services
    .AddAuth0WebAppAuthentication(options => {
        options.Domain = builder.Configuration["Auth0:Domain"]!;
        options.ClientId = builder.Configuration["Auth0:ClientId"]!;
        // ДОДАЙ ЦЕЙ РЯДОК:
        options.CallbackPath = new PathString("/callback");
    });

// 2. Підключення MVC
builder.Services.AddControllersWithViews();

// 3. Dependency Injection для нашої логіки (Singleton, щоб дані зберігалися у пам'яті поки сервер працює)
// Див. Лекція 5 стор 48 [cite: 1321]
builder.Services.AddSingleton<AcademicJournal>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 4. Включаємо Аутентифікацію та Авторизацію
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
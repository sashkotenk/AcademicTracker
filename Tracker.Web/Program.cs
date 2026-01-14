using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Tracker.Core;
using Tracker.Web.Data;

var builder = WebApplication.CreateBuilder(args);

var provider = builder.Configuration["Database:Provider"];
var connectionStrings = builder.Configuration.GetSection("Database:ConnectionStrings");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    switch (provider)
    {
        case "Sqlite":
            options.UseSqlite(connectionStrings["Sqlite"]);
            break;
        case "InMemory":
            options.UseInMemoryDatabase(connectionStrings["InMemory"]!);
            break;
        case "SqlServer":
            options.UseSqlServer(connectionStrings["SqlServer"]);
            break;
        case "Postgres":
            options.UseNpgsql(connectionStrings["Postgres"]);
            break;
        default:
            throw new Exception($"Невідомий провайдер БД: {provider}");
    }
});

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

// Автоматичне створення БД
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

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
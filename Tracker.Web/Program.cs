using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Tracker.Web.Data;
using Auth0.AspNetCore.Authentication;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
// Для Auth0

var builder = WebApplication.CreateBuilder(args);

// 1. Налаштування Auth0 (з Лаби 3)
builder.Services.AddAuth0WebAppAuthentication(options => {
    options.Domain = builder.Configuration["Auth0:Domain"]!;
    options.ClientId = builder.Configuration["Auth0:ClientId"]!;
    options.CallbackPath = new PathString("/callback"); // Твій короткий шлях
});

// 2. Налаштування Бази Даних (з Лаби 4)
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
            // Якщо не вказано, використовуємо InMemory, щоб не падало
            options.UseInMemoryDatabase("TrackerDbMem");
            break;
    }
});

// 3. Налаштування Версійності API (з Лаби 5)
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// 4. Налаштування Swagger (з Лаби 5)
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Academic Tracker API V1", Version = "v1" });
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "Academic Tracker API V2", Version = "v2" });
});

builder.Services.AddControllersWithViews();

builder.Services.AddOpenTelemetry()
    .WithMetrics(metrics => metrics
        .AddAspNetCoreInstrumentation()
        .AddRuntimeInstrumentation()
        .AddPrometheusExporter())
    .WithTracing(tracing => tracing
        .AddSource("AcademicTracker") // Важливо! Це ім'я джерела
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddZipkinExporter(o => o.Endpoint = new Uri("http://localhost:9411/api/v2/spans")));
// ...

var app = builder.Build();

// 5. Автоматичне створення БД (Code First)
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // InMemory не потребує EnsureCreated у такому вигляді, але для інших це важливо
    if (provider != "InMemory")
    {
        dbContext.Database.EnsureCreated();
    }
    else
    {
        // Для InMemory просто переконуємося, що воно ініціалізовано
        dbContext.Database.EnsureCreated();
    }
}

// 6. Middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    // Включаємо Swagger тільки в режимі розробки
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "API V2");
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
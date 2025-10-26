using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure; // MySqlServerVersion
using systemdeeps.WebApplication.Data;
using systemdeeps.WebApplication.Hubs;
using systemdeeps.WebApplication.Interfaces;
using systemdeeps.WebApplication.Repositories;
using systemdeeps.WebApplication.Services;

var builder = WebApplication.CreateBuilder(args);

// -------------------------------
// Database provider selection
// -------------------------------
bool useMySql = builder.Configuration.GetValue<bool>("UseMySql");
string? mysqlConn = builder.Configuration.GetConnectionString("MySql");
string? sqliteConn = builder.Configuration.GetConnectionString("Sqlite");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (useMySql)
    {
        // Set your real MySQL/MariaDB version here
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));
        options.UseMySql(mysqlConn!, serverVersion, mySql => mySql.EnableRetryOnFailure());
    }
    else
    {
        options.UseSqlite(sqliteConn!);
    }
});

// -------------------------------
// MVC + Razor
// -------------------------------
var mvc = builder.Services.AddControllersWithViews();

#if DEBUG
// Works if Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation 8.0.11 is installed
mvc.AddRazorRuntimeCompilation();
#endif

// -------------------------------
// SignalR
// -------------------------------
builder.Services.AddSignalR();

// -------------------------------
// DI: repositories & services
// -------------------------------
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<AffiliateService>();
builder.Services.AddScoped<TurnService>();

var app = builder.Build();

// -------------------------------
// Apply migrations on startup (dev/test)
// -------------------------------
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.UseStaticFiles();
app.UseRouting();

// Default MVC route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// SignalR hub
app.MapHub<TurnHub>("/turnHub");

app.Run();
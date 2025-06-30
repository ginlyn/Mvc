using CrudMVC.Controllers;
using CrudMVC.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);



// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();



// Replace default logging with Serilog
builder.Host.UseSerilog();


// Add services to the container.

//Configure the ConnectionString and DbContext class
builder.Services.AddDbContext<EFCoreDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("EFCoreDBConnection"));
});




builder.Services.AddControllersWithViews();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();



app.UseRequestTiming();


app.UseRouting();

app.UseSession();

app.UseAuthorization();


Log.Information("App Starting...");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employees}/{action=Index}/{id?}");

app.Run();

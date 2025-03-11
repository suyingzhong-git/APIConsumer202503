using APIConsumer.BL.Interfaces;
using APIConsumer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using APIConsumer.BL.services;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
//for Http Client Factory. Transient service. 
builder.Services.AddHttpClient<IHttpClientExternalAPIRainfallService, HttpClientExternalAPIRainfallService>();
builder.Services.AddHttpClient<IHttpClientForExternalAPILocksService, HttpClientForExternalAPILocksService>();
//Set 5 min as the lifetime for the HttpMessageHandler objects in the pool used for the Catalog Typed Client
//.SetHandlerLifetime(TimeSpan.FromMinutes(5));

//logging to file
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File($"Logs/{Assembly.GetExecutingAssembly().GetName().Name}.log")
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

//logging
builder.Services.AddLogging(config => {
    config.AddConsole();
    config.AddDebug();
    });
//global error handling middleware
//builder.Services.AddTransient<ErrorHandlingMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

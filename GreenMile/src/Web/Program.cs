using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

using Web.Data;
using Web.Models;
using Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<IHouseholdService, HouseholdService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<DonationService>();
builder.Services.AddScoped<CustomFoodService>();
builder.Services.AddDbContext<DataContext>(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        // Setup SQLite Connection
        var devConnectionString = builder.Configuration.GetConnectionString("dev");
        options.UseSqlite(connectionString: devConnectionString);
    }
    else if (builder.Environment.IsProduction())
    {
        // Setup MySQL Connection
        var prodConnectionString = builder.Configuration.GetConnectionString("prod");
        var serverVersion = ServerVersion.AutoDetect(prodConnectionString);
        options.UseMySql(connectionString: prodConnectionString, serverVersion);
    }
    else
    {
        Console.WriteLine("[WARNING]: Environment is neither Development or Production.");
        Console.WriteLine("[WARNING]: System will default to using SQLite database provider.");
        var devConnectionString = builder.Configuration.GetConnectionString("dev");
        options.UseSqlite(connectionString: devConnectionString);
    }
});
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>();
// Configure Identity Options
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
// NOTE: Stores to server memory
// TODO: Change to externals stores to allow horizontal scalling
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

builder.Services.ConfigureApplicationCookie(options =>
{

    options.AccessDeniedPath = "/login";
    options.LoginPath = "/login";


});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
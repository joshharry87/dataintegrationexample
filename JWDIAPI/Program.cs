using JWDIADATA.Data;
using JWDIACONTRACTS.Services.Weather;
using JWDIACONTRACTS.Interfaces.Weather;
using JWDIACONTRACTS.Interfaces.GeoSurvey;
using JWDIACONTRACTS.Services.GeoSurvey;

using Microsoft.EntityFrameworkCore;

// Need to add a bunch of services 


var builder = WebApplication.CreateBuilder(args);

// Add DB Context - need to add logic for Dev vs prod etc.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
// builder.Services.AddControllers()
builder.Services.AddControllersWithViews();

// Abstract add services into a separate file to clean this process up
builder.Services.AddScoped<IWeatherDataService, WeatherDataService>();
builder.Services.AddScoped<IGeoSurveyService, GeoSurveyService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

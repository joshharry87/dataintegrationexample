using System.Text;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;


using JWDIADATA.Data;
using JWDIAPI.Configurations;

using Scalar.AspNetCore;

// Need to add a bunch of services 


var builder = WebApplication.CreateBuilder(args);



// Add DB Context - need to add logic for Dev vs prod etc.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



// Identity Configuration
IdentityConfig.ConfigureIdentityOptions(builder.Services);


builder.Services.AddOpenApi();
// Add services to the container.
// builder.Services.AddControllers()
builder.Services.AddControllersWithViews();

// Abstract add services into a separate file to clean this process up
LoadServices.LoadInCustomServices(builder.Services);

// builder.Services.AddScoped<IWeatherDataService, WeatherDataService>();
// builder.Services.AddScoped<IGeoSurveyService, GeoSurveyService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapOpenApi();
app.MapScalarApiReference();
// app.UseHttpsRedirection();
app.UseRouting();

// to be checked etc.
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

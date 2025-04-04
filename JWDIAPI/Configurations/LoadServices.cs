using JWDIACONTRACTS.Services.Weather;
using JWDIACONTRACTS.Interfaces.Weather;
using JWDIACONTRACTS.Interfaces.GeoSurvey;
using JWDIACONTRACTS.Services.GeoSurvey;
using JWDIACONTRACTS.Interfaces.Auth;
using JWDIACONTRACTS.Services.Auth;
using JWDIACONTRACTS.Services.Auth.Custom;
using JWDIACONTRACTS.Interfaces.Auth.Custom;

using JWDIACONTRACTS.Interfaces.Admin;
using JWDIACONTRACTS.Services.Admin;

using JWDIAPI.Helpers.CustomPolicies;

// this should self generate reading by assembly and naming conventions
namespace JWDIAPI.Configurations
{
    public static class LoadServices
    {
        public static void LoadInCustomServices(IServiceCollection services)
        {
            services.AddScoped<IWeatherDataService, WeatherDataService>();
            services.AddScoped<IGeoSurveyService, GeoSurveyService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IUsageLimitService, UsageLimitService>();
            services.AddScoped<UsageLimitFilter>();

        }
    }
}

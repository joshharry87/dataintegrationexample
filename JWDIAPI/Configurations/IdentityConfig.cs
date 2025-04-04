using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;


using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;


using Scalar.AspNetCore;


namespace JWDIAPI.Configurations
{
    public static class IdentityConfig
    {
        
        public static void ConfigureIdentityOptions(IServiceCollection services)
        {
            //
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequiredLength = 6;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+!";
                options.User.RequireUniqueEmail = false;
            });


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        // ValidIssuer = builder.Configuration["Appsettings:Issuer"],
                        ValidIssuer = "Me",
                        ValidateAudience = true,
                        ValidAudience = "ThePeople",
                        // ValidAudience = builder.Configuration["Appsettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes("tokentoketokentoketokentoketokentoketokentoketokentoketokentoketokentoke")
                            // Encoding.UTF8.GetBytes(builder.Configuration["Appsettings:Token"]),
                            ),
                        ValidateIssuerSigningKey = true,

                        
                    };
                });

           
        }
    }
}

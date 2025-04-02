using System.Runtime.InteropServices;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Text;
using System.IO.Enumeration;
using System.IO;
using System;
using System.Security.AccessControl;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;


// using System.IdentityModel.Tokens.JWT;
using JWDIADATA.Data.Entities;
using JWDIADATA.Data;
using JWDIACONTRACTS.Mappings.Auth;
using JWDIACONTRACTS.Interfaces.Auth;
using JWDIACONTRACTS.DTO.Auth;



namespace JWDIACONTRACTS.Services.Auth;
public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }


    public async Task<UserDTO> CreateUserAsync (NewUser userIn) {

        

        UserDataModel user = new UserDataModel(){
            Username = userIn.Username,
            Password = userIn.Password, 
            RequireUniqueEmail = userIn.RequireUniqueEmail,
            Role = "User"
        };

        var hashedPassword = new PasswordHasher<UserDataModel>().HashPassword(user, user.Password);
        user.Password = hashedPassword;


        UserDataModel checkUser = await _context.Users.SingleOrDefaultAsync(u => u.Username == userIn.Username);
        
        if (checkUser == null){
            _context.Users.Add(user);
            _context.SaveChanges();
            return new UserDTO(){
                Id = user.Id,
                Username = user.Username,
                Password = userIn.Password, 
                RequireUniqueEmail = userIn.RequireUniqueEmail,
                Role = user.Role
            };
        }
        else {
            return null;
        }

    }


    public async Task<string> LoginAsync (Login userLogin) {

        UserDataModel checkUser = await _context.Users.SingleOrDefaultAsync(u => u.Username == userLogin.UserName);
        if (checkUser == null){

            return null;
        }
        
        if (new PasswordHasher<UserDataModel>().VerifyHashedPassword(checkUser, checkUser.Password, userLogin.Password)
           == PasswordVerificationResult.Failed){
            return null;
        }

        string token = CreateToken(checkUser);


        return token;
    }




    private string CreateToken (UserDataModel user) {

        //https://learn.microsoft.com/en-us/dotnet/api/system.security.claims.claimtypes?view=net-9.0
        var claims = new List<Claim>{
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role), 
            new Claim(ClaimTypes.Email, user.RequireUniqueEmail),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        // // var key = configuration.GetValue<string>("AppSettings:Token");
        // // var issuer = configuration.GetValue<string>("AppSettings:Issuer");
        // // var audience = configuration.GetValue<string>("AppSettings:Audience");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("tokentoketokentoketokentoketokentoketokentoketokentoketokentoketokentoke"));
        
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        //https://learn.microsoft.com/en-us/dotnet/api/system.identitymodel.tokens.jwt.jwtsecuritytoken.-ctor?view=msal-web-dotnet-latest
        var tokenDescriptor = new JwtSecurityToken(
            issuer: "Me",
            audience: "ThePeople",
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: creds
          
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

        
    }
    
}

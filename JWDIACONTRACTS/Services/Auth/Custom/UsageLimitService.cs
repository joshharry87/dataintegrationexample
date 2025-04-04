

using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;


using JWDIACONTRACTS.DTO.Auth;
using JWDIACONTRACTS.Interfaces.Auth.Custom;
using JWDIADATA.Data;
using JWDIADATA.Data.Entities;



// this needs refactoring, move this into the api and build services for handling the data

namespace JWDIACONTRACTS.Services.Auth.Custom;

public class UsageLimitService : IUsageLimitService
{
    private readonly ApplicationDbContext _context;

    public UsageLimitService(ApplicationDbContext context)
    {
        _context = context;
        
    }


    public bool CheckUsageLimitAsync(string role, string userName, RoleLimits roleData)
    {
        // Custom logic before authorization (e.g., check for additional claims, headers, etc.)
        
        
        UserDataModel userData =  _context.Users.SingleOrDefault(u => u.Username == userName);

        if ( roleData.Roles.Contains(role))  {
            switch(role) {
                case "User":
                    if (userData.Usage == roleData.User){
                        return false;
                        
                    }
                    else {
                        userData.Usage += 1;
                    }
                    break;
                case "Premium":
                    if (userData.Usage == roleData.Premium){
                            return false;

                        }
                    else {
                        userData.Usage += 1;
                        }
                    break;
                case "Admin":
                    if (userData.Usage == roleData.Admin){
                        return false;

                        }
                    else {
                        userData.Usage += 1;
                        }
                    break;
            }

            _context.SaveChanges();

           
        }

        return true;

    }

   
}

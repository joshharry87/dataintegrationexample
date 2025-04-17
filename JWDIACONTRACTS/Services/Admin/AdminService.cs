using JWDIACONTRACTS.Interfaces.Admin;
using JWDIACONTRACTS.DTO.Auth;
using JWDIADATA.Data.Entities;
using JWDIADATA.Data;
using JWDIACONTRACTS.Mappings.Auth;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using JWDIACONTRACTS.Mappings.Auth;

namespace JWDIACONTRACTS.Services.Admin;

public class AdminService: IAdminService
{
    
     private readonly ApplicationDbContext _context;

    public AdminService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserDTO>> GetUsersAsync(){
        List<UserDataModel> users = await _context.Users.ToListAsync();

        List<UserDTO> data = users.ConvertAll(new Converter<UserDataModel, UserDTO>(UserMappings.UserConverter));
        return data;
    }

    public async Task<string> RemoveUserAsync(UserBase userIn){

        var user = await _context.Users.FirstOrDefaultAsync(p => p.Username == userIn.Username);
         if (user == null) {
                return null;
                }
            else {
                _context.Users.Remove(user);
                _context.SaveChanges();
            return "Removed User" ;
                
            }


    }


    public async Task<UserDTO> SetRolesAsync(UserRole userRole){
        var user = await _context.Users.FirstOrDefaultAsync(p => p.Username == userRole.Username);

            if (user == null) {
                return null;
                }
            else {
                user.Role = userRole.Role;
                _context.SaveChanges();
                return new UserDTO() {
                    Role = user.Role,
                    Id = user.Id,
                    Username = user.Username,
                    RequireUniqueEmail = user.RequireUniqueEmail,
                    Password = user.Password,
                    
                };

            }
    }
}

    // manage user roles etc.. takes in username and role value then updates the role based on their username



using System.Runtime.CompilerServices;
using System;
using JWDIACONTRACTS.DTO.Auth;

namespace JWDIACONTRACTS.Interfaces.Auth;
public interface IAuthService
{
    Task<UserDTO> CreateUserAsync (NewUser user);
    
    // Task<UserDTO> RemoveUserAsync (UserBase user);

    Task<string> LoginAsync (Login userLogin);

    Task<string> ChangePasswordAsync (UserPasswordChange userPasswordChange);
    
}

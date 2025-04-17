using JWDIACONTRACTS.DTO.Auth;

namespace JWDIACONTRACTS.Interfaces.Admin;

public interface IAdminService
{
    Task<List<UserDTO>> GetUsersAsync();
    Task<UserDTO> SetRolesAsync(UserRole userRole);

    Task<string> RemoveUserAsync(UserBase userBase);
}

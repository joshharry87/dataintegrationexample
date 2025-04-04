

using JWDIACONTRACTS.DTO.Auth;


namespace JWDIACONTRACTS.Interfaces.Auth.Custom;

public interface IUsageLimitService
{
    
    bool CheckUsageLimitAsync(string role, string userName, RoleLimits roleData);

}

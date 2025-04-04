using Microsoft.AspNetCore.Authorization;

using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

using JWDIACONTRACTS.Interfaces.Auth.Custom;
using JWDIACONTRACTS.DTO.Auth;


// this needs refactoring, move this into the api and build services for handling the data

namespace JWDIAPI.Helpers.CustomPolicies;
public class UsageLimitFilter : IAuthorizationFilter
{

    private readonly IUsageLimitService _usageLimitService;
    private readonly IConfiguration _configuration;

    public UsageLimitFilter( IConfiguration configuration, IUsageLimitService usageLimitService)
    {
        
        _configuration = configuration;
        _usageLimitService = usageLimitService;
    }


    public void OnAuthorization(AuthorizationFilterContext context)
    {
       
        var user = context.HttpContext.User;

        var roleClaim = user.FindFirst(ClaimTypes.Role);
        var usernameClaim = user.FindFirst(ClaimTypes.Name);

        if (roleClaim == null || usernameClaim == null)
        {
            context.Result = new UnauthorizedResult(); 
            return ;
            
        }

        string role = roleClaim.Value;
        string username = usernameClaim.Value;

        var roleData = _configuration.GetRequiredSection("RoleLimits").Get<RoleLimits>();
        

        if ( roleData.Roles.Contains(role))  {
            bool result =  _usageLimitService.CheckUsageLimitAsync(
                    role, 
                    username, 
                    roleData
                 );
            if(!result
                 ){
                    context.Result = new UnauthorizedResult(); 
                 }

        }

    }

   
}

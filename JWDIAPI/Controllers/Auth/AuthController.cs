
using Microsoft.AspNetCore.Mvc;


using JWDIACONTRACTS.DTO.Auth;

using JWDIACONTRACTS.Interfaces.Auth;



namespace JWDIAPI.Controllers.Auth;


[Route("api/[controller]")]
[ApiController]
public class AuthController : BaseApiController
{
    
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [Route("api/[controller]/Login")]
    [HttpPost]
    public async Task<ActionResult<string>> Login(Login userLogin){
        var result = await  _authService.LoginAsync(userLogin);

        return Ok(result);
        
    }
    
    [Route("api/[controller]/CreateUser")]
    [HttpPost]
    public async Task<ActionResult<UserDTO>> CreateNewUser(UserBase userIn){

        var user = await _authService.RemoveUserAsync(userIn);
        if (user == null){
            return BadRequest("Nope");
        }
        else{
            return Ok(user);
        } 
    }

    
}


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
    public async Task<ActionResult<UserDTO>> CreateNewUser(NewUser userIn){

        var user = await _authService.CreateUserAsync(userIn);
        if (user == null){
            return BadRequest("Nope");
        }
        else{
            return Ok(user);
        } 
    }

    [Route("api/[controller]/ChangePassword")]
    [HttpPost]
    public async Task<ActionResult<string>> ChangePassword(UserPasswordChange userIn){

        var user = await _authService.ChangePasswordAsync(userIn);
        if (user == null){
            return BadRequest("Incorrect password!");
        }
        else{
            return Ok(user);
        } 
    }

    
}

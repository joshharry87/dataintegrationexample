
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using JWDIACONTRACTS.DTO.Auth;
using JWDIACONTRACTS.Interfaces.Admin;


namespace JWDIAPI.Controllers.Admin;


[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
[ApiController]
public class AdminController: BaseApiController
{
     private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [Route("api/[controller]/GetUsers")]
    [HttpGet]
    public async Task<ActionResult<List<UserDTO>>> Get(){

        var result = await _adminService.GetUsersAsync();

        return Ok(result);
    }

    [Route("api/[controller]/SetUserRole")]
    [HttpPost]
    public async Task<ActionResult<UserDTO>> SetRole(UserRole userRole){

        var result = await _adminService.SetRolesAsync(userRole);
        if (result != null){
            return Ok(result);
        }
        else{
            return BadRequest("Invalid User");
        }
        
    }

    [Route("api/[controller]/RemoveUser")]
    [HttpPost]
    public async Task<ActionResult<string>> RemoveUser(UserBase userBase){

        var result = await _adminService.RemoveUserAsync(userBase);
        if (result != null){
            return Ok(result);
        }
        else{
            return BadRequest("Username Does Not Exist!");
        }
        
    }


}

using System.ComponentModel.DataAnnotations;


namespace JWDIACONTRACTS.DTO.Auth;
public class UserPasswordChange
{

    [Required]
    public required string Username {get; set;}

    [Required]
    public required string Password {get; set;}

    [Required]
    public required string NewPassword {get; set;}    
    
}

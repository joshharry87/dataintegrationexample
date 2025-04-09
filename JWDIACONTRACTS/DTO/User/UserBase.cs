using System.ComponentModel.DataAnnotations;


namespace JWDIACONTRACTS.DTO.Auth;

public class UserBase
{
    
    [Required]
    public required string Username {get; set;}

}

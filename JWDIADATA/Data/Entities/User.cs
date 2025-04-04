using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System;

namespace JWDIADATA.Data.Entities;

public class UserDataModel
{


    public  Guid Id {get; set;}

    [Required]
    public required string Username {get; set;}


    [Required]
    public required string Password {get; set;}


    [Required]
    [EmailAddress]
    public required string RequireUniqueEmail {get; set;}


    public string Role {get; set;}

    public DateTime LastActive {get; set;}

    public int Usage {get; set;}


}

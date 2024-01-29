using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO;

public class RegisterRequestDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
    
    // [Required] public string Username { get; set; } = String.Empty;

    public string[] Roles { get; set; } = new string[] { };


}
using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models;


//Connexion
public class LoginModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}
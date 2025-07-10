using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models;


//Connexion
public class LoginModel
{
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}
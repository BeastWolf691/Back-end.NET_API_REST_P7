using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models;

//Inscription
public class RegisterModel
{

    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

}
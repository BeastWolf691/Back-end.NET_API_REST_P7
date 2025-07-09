using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models;

//Inscription
public class RegisterModel
{
    public RegisterModel(string fullName, string email, string password)
    {
        FullName = fullName;
        Email = email;
        Password = password;
    }

    [Required]
    [StringLength(50, ErrorMessage = "Le nom complet ne doit pas excéder 50 caractères.")]
    public string FullName { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "L'adresse email est invalide")]
    public string Email { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 8)]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{8,}$",
        ErrorMessage = "Le mot de passe doit contenir au moins une majuscule, une minuscule, un chiffre, un symbole et 8 caractères.")]
    public string Password { get; set; } = string.Empty;

}
using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models.Dto;

//sert pour la gestion des comptes coté admin
public class UserDto
    {
        public string Id { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{8,}$",
        ErrorMessage = "Le mot de passe doit contenir au moins une majuscule, une minuscule, un chiffre, un symbole et 8 caractères.")]
        public string Password { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Le nom d'utilisateur ne doit pas excéder 50 caractères.")]
        public string UserName { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Le nom complet ne doit pas excéder 50 caractères.")]
        public string FullName { get; set; } = string.Empty;
    }

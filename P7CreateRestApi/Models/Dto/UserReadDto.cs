using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models.Dto
{
    //lecture
    public class UserReadDto
    {
        public string Id { get; set; } = string.Empty;

        [Required]
        [StringLength(50, ErrorMessage = "Le nom d'utilisateur ne doit pas excéder 50 caractères.")]
        public string UserName { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Le nom complet ne doit pas excéder 50 caractères.")]
        public string FullName { get; set; } = string.Empty;
    }
}

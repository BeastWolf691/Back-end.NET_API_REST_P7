using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models.Dto
{
    public class RatingDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "La note de solvabilité Moody's ne doit pas excéder 50 caractères")]
        public string MoodysRating { get; set; } = string.Empty;
        [Required]
        [StringLength(50, ErrorMessage = "La note de solvabilité S&P ne doit pas excéder 50 caractères")]
        public string SandPRating {  get; set; } = string.Empty;
        [Required]
        [StringLength(50, ErrorMessage = "La note de solvabilité Fitch ne doit pas excéder 50 caractères")]
        public string FitchRating {  get; set; } = string.Empty;
        [Range(0, byte.MaxValue, ErrorMessage = "Le numéro de commande ne peut pas être un nombre négatif")]
        public byte? OrderNumber { get; set; }
    }
}

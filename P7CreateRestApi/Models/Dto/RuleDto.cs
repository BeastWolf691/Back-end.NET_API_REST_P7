using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models.Dto
{
    public class RuleDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Le nom ne peut pas excéder 100 caractères.")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(500, ErrorMessage = "La description ne peut pas excéder 500 caractères.")]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string Json { get; set; } = string.Empty;
        [Required]
        public string Template { get; set; } = string.Empty;
        [Required]
        public string SqlStr { get; set; } = string.Empty;
        [Required]
        public string SqlPart { get; set; } = string.Empty;
    }
}

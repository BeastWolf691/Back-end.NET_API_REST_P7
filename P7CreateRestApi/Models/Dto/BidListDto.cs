using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models.Dto
{
    public class BidListDto
    {
        public int BidListId { get; set; }

        [Required(ErrorMessage = "Le compte est obligatoire.")]
        public string Account { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le type de l'offre est obligatoire.")]
        public string BidType { get; set; } = string.Empty;
        public double? BidQuantity { get; set; }
    }
}

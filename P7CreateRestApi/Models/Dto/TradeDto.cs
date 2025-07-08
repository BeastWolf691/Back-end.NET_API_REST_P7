using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models.Dto
{
    public class TradeDto
    {
        public int TradeId { get; set; }
        
        [Required]
        [StringLength(50, ErrorMessage = "Le compte ne peut excéder 50 caractères.")]
        public string Account { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50, ErrorMessage = "Le type de compte ne peut excéder 50 caractères.")]
        public string AccountType { get; set; } = string.Empty;
        
        [Range(0, double.MaxValue, ErrorMessage = "La quantité achetée ne peut pas être un nombre négatif.")]
        public double? BuyQuantity { get; set; }
        
        [Range(0, double.MaxValue, ErrorMessage = "La quantité vendue ne peut pas être un nombre négatif.")]
        public double? SellQuantity { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Le prix d'achat ne peut pas être un nombre négatif")]
        public double? BuyPrice { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Le prix de vente ne peut pas être un nombre négatif")]
        public double? SellPrice { get; set; }
        
        public DateTime? TradeDate { get; set; }

        [StringLength(50, ErrorMessage = "Le nom du produit financier ne peut excéder 50 caractères.")]
        public string TradeSecurity { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Le statut de la transaction ne peut excéder 50 caractères.")]
        public string TradeStatus { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Le nom du trader ne peut excéder 50 caractères.")]
        public string Trader { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Le nom de la référence (benchmark) ne peut excéder 50 caractères.")]
        public string Benchmark { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Le nom du book ne peut excéder 50 caractères.")]
        public string Book { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Le nom du créateur ne peut excéder 50 caractères.")]
        public string CreationName { get; set; } = string.Empty;
        
        public DateTime? CreationDate { get; set; }

        [StringLength(50, ErrorMessage = "Le nom du réviseur ne peut excéder 50 caractères.")]
        public string RevisionName { get; set; } = string.Empty;
        
        public DateTime? RevisionDate { get; set; }

        [StringLength(50, ErrorMessage = "Le nom du deal ne peut excéder 50 caractères.")]
        public string DealName { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Le type de deal ne peut excéder 50 caractères.")]
        public string DealType { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "L'identifiant de la liste source ne peut excéder 50 caractères.")]
        public string SourceListId { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Le type de transaction (side) ne peut excéder 50 caractères.")]
        public string Side { get; set; } = string.Empty;
    }
}

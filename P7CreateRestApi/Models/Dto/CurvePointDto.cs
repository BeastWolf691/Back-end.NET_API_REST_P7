using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models.Dto
{
    public class CurvePointDto
    {
        public int Id { get; set; }
        [Required]
        public byte CurveId { get; set; }
        public DateTime AsOfDate { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Le délai ne peut pas être un nombre négatif")]
        public double? Term { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "La valeur ne peut pas être un nombre négatif")]
        public double? CurvePointValue { get; set; }
        public DateTime CreationDate { get; set; }
    }
}

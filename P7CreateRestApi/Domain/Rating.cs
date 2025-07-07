namespace Dot.Net.WebApi.Controllers.Domain
{
    public class Rating
    {
        public int Id { get; set; }
        public string MoodysRating { get; set; } = string.Empty;
        public string SandPRating { get; set; } = string.Empty;
        public string FitchRating { get; set; } = string.Empty;
        public byte? OrderNumber { get; set; }
    }
}
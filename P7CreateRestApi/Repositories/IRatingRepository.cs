using P7CreateRestApi.Models.Dto;

namespace P7CreateRestApi.Repositories
{
    public interface IRatingRepository
    {
        Task<IEnumerable<RatingDto>> GetRatings();
        Task<RatingDto?> GetRating(int id);
        Task<RatingDto> AddRating(RatingDto ratingDto);
        Task<RatingDto?> UpdateRating(int id, RatingDto ratingDto);
        Task<bool> DeleteRating(int id);
    }
}

using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Models.Dto;

namespace P7CreateRestApi.Services
{
    public interface IRatingService
    {
        Task<IEnumerable<Rating>> GetRatings();
        Task<Rating?> GetRating(int id);
        Task<Rating> AddRating(Rating rating);
        Task<Rating?> UpdateRating(int id, Rating rating);
        Task<bool> DeleteRating(int id);
    }
}

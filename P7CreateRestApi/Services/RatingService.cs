using P7CreateRestApi.Models.Dto;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;

        public RatingService(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public async Task<IEnumerable<RatingDto>> GetRatings()
        {
            return await _ratingRepository.GetRatings();
        }

        public async Task<RatingDto?> GetRating(int id)
        {
            return await _ratingRepository.GetRating(id);
        }

        public async Task<RatingDto> AddRating(RatingDto rating)
        {
            return await _ratingRepository.AddRating(rating);
        }

        public async Task<RatingDto?> UpdateRating(int id, RatingDto ratingDto)
        {
            return await _ratingRepository.UpdateRating(id, ratingDto);
        }

        public async Task<bool> DeleteRating(int id)
        {
            return await _ratingRepository.DeleteRating(id);
        }
    }
}

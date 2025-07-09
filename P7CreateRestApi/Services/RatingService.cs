using AutoMapper;
using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Models.Dto;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IMapper _mapper;

        public RatingService(IRatingRepository ratingRepository, IMapper mapper)
        {
            _ratingRepository = ratingRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RatingDto>> GetRatings()
        {
            var ratings = await _ratingRepository.GetRatings();
            return _mapper.Map<IEnumerable<RatingDto>>(ratings);
        }

        public async Task<RatingDto?> GetRating(int id)
        {
            var rating = await _ratingRepository.GetRating(id);
            if (rating == null) return null;
            return _mapper.Map<RatingDto>(rating);
        }

        public async Task<RatingDto> AddRating(RatingDto ratingDto)
        {
            var rating = _mapper.Map<Rating>(ratingDto);
            var added = await _ratingRepository.AddRating(rating);
            return _mapper.Map<RatingDto>(added);
        }

        public async Task<RatingDto?> UpdateRating(int id, RatingDto ratingDto)
        {
            var rating = _mapper.Map<Rating>(ratingDto);
            var updated = await _ratingRepository.UpdateRating(id, rating);
            if (updated == null) return null;
            return _mapper.Map<RatingDto>(updated);
        }

        public async Task<bool> DeleteRating(int id)
        {
            return await _ratingRepository.DeleteRating(id);
        }
    }
}

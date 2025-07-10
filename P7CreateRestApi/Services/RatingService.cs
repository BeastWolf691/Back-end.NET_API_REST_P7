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

        private void ValidateRatingDto(RatingDto ratingDto)
        {
            if (string.IsNullOrWhiteSpace(ratingDto.MoodysRating) || ratingDto.MoodysRating.Length > 50)
                throw new ArgumentException("La note de solvabilité Moody's est obligatoire et ne doit pas excéder 50 caractères.");
            
            if (string.IsNullOrWhiteSpace(ratingDto.SandPRating) || ratingDto.SandPRating.Length > 50)
                throw new ArgumentException("La note de solvabilité S&P est obligatoire et ne doit pas excéder 50 caractères.");
            
            if (string.IsNullOrWhiteSpace(ratingDto.FitchRating) || ratingDto.FitchRating.Length > 50)
                throw new ArgumentException("La note de solvabilité Fitch est obligatoire et ne doit pas excéder 50 caractères.");
            
            if (ratingDto.OrderNumber.HasValue && ratingDto.OrderNumber < 0)
                throw new ArgumentException("Le numéro de commande ne peut pas être négatif.");
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
            ValidateRatingDto(ratingDto);
            var rating = _mapper.Map<Rating>(ratingDto);

            var added = await _ratingRepository.AddRating(rating);
            return _mapper.Map<RatingDto>(added);
        }

        public async Task<RatingDto?> UpdateRating(int id, RatingDto ratingDto)
        {
            var rating = await _ratingRepository.GetRating(id);
            if (rating == null) return null;

            if (!string.IsNullOrWhiteSpace(ratingDto.MoodysRating))
                rating.MoodysRating = ratingDto.MoodysRating;

            if (!string.IsNullOrWhiteSpace(ratingDto.SandPRating))
                rating.SandPRating = ratingDto.SandPRating;

            if (!string.IsNullOrWhiteSpace(ratingDto.FitchRating))
                rating.FitchRating = ratingDto.FitchRating;

            if (ratingDto.OrderNumber.HasValue)
                rating.OrderNumber = ratingDto.OrderNumber.Value;

            var dtoToValidate = _mapper.Map<RatingDto>(rating);
            ValidateRatingDto(dtoToValidate);

            await _ratingRepository.UpdateRating(id, rating);
            return _mapper.Map<RatingDto>(rating);
        }

        public async Task<bool> DeleteRating(int id)
        {
            return await _ratingRepository.DeleteRating(id);
        }
    }
}

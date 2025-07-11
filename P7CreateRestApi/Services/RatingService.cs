using Dot.Net.WebApi.Domain;
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

        private void ValidateRating(Rating rating)
        {
            if (string.IsNullOrWhiteSpace(rating.MoodysRating) || rating.MoodysRating.Length > 50)
                throw new ArgumentException("La note de solvabilité Moody's est obligatoire et ne doit pas excéder 50 caractères.");
            
            if (string.IsNullOrWhiteSpace(rating.SandPRating) || rating.SandPRating.Length > 50)
                throw new ArgumentException("La note de solvabilité S&P est obligatoire et ne doit pas excéder 50 caractères.");
            
            if (string.IsNullOrWhiteSpace(rating.FitchRating) || rating.FitchRating.Length > 50)
                throw new ArgumentException("La note de solvabilité Fitch est obligatoire et ne doit pas excéder 50 caractères.");
            
            if (rating.OrderNumber.HasValue && rating.OrderNumber < 0)
                throw new ArgumentException("Le numéro de commande ne peut pas être négatif.");
        }

        public async Task<IEnumerable<Rating>> GetRatings()
        {
            return await _ratingRepository.GetRatings();
        }

        public async Task<Rating?> GetRating(int id)
        {
            return await _ratingRepository.GetRating(id);
        }

        public async Task<Rating> AddRating(Rating rating)
        {
            ValidateRating(rating);
            return await _ratingRepository.AddRating(rating);
        }

        public async Task<Rating?> UpdateRating(int id, Rating rating)
        {
            var existing = await _ratingRepository.GetRating(id);
            if (existing == null) return null;

            existing.MoodysRating = !string.IsNullOrWhiteSpace(rating.MoodysRating) ? rating.MoodysRating : existing.MoodysRating;
            existing.SandPRating = !string.IsNullOrWhiteSpace(rating.SandPRating) ? rating.SandPRating : existing.SandPRating;
            existing.FitchRating = !string.IsNullOrWhiteSpace(rating.FitchRating) ? rating.FitchRating : existing.FitchRating;
            existing.OrderNumber = (rating.OrderNumber.HasValue &&  rating.OrderNumber !=0) ? rating.OrderNumber : existing.OrderNumber;

            ValidateRating(existing);

            await _ratingRepository.UpdateRating(id, existing);
            return existing;
        }

        public async Task<bool> DeleteRating(int id)
        {
            return await _ratingRepository.DeleteRating(id);
        }
    }
}

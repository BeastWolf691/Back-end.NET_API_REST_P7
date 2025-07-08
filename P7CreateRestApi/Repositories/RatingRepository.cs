using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Models.Dto;

namespace P7CreateRestApi.Repositories
{
    public class RatingRepository : IRatingRepository
    { 
        private readonly LocalDbContext _context;

        public RatingRepository(LocalDbContext context)
        {
            _context = context;
        }

        private static RatingDto ToDto(Rating rating) => new RatingDto
        {
            Id = rating.Id,
            MoodysRating = rating.MoodysRating,
            SandPRating = rating.SandPRating,
            FitchRating = rating.FitchRating,
            OrderNumber = rating.OrderNumber
        };

        private static Rating ToEntity(RatingDto ratingDto) => new Rating
        {
            MoodysRating = ratingDto.MoodysRating,
            SandPRating = ratingDto.SandPRating,
            FitchRating = ratingDto.FitchRating,
            OrderNumber = ratingDto.OrderNumber
        };

        public async Task<IEnumerable<RatingDto>> GetRatings()
        {
            return await _context.Ratings
                .Select(rating => ToDto(rating))
                .ToListAsync();
        }

        public async Task<RatingDto?> GetRating(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);
            return rating == null ? null : ToDto(rating);
        }

        public async Task<RatingDto> AddRating(RatingDto ratingDto)
        {
            var rating = ToEntity(ratingDto);
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();
            return ToDto(rating);
        }

        public async Task<RatingDto?> UpdateRating(int id, RatingDto ratingDto)
        {
            var rating = await _context.Ratings.FindAsync(id);
            if (rating == null) return null;

            rating.MoodysRating = ratingDto.MoodysRating;
            rating.SandPRating = ratingDto.SandPRating;
            rating.FitchRating = ratingDto.FitchRating;
            rating.OrderNumber = ratingDto.OrderNumber;

            _context.Ratings.Update(rating);
            await _context.SaveChangesAsync();
            return ToDto(rating);
        }

        public async Task<bool> DeleteRating(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);
            if (rating == null) return false;

            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

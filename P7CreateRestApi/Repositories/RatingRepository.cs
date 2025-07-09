using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
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

        public async Task<IEnumerable<Rating>> GetRatings()
        {
            return await _context.Ratings.ToListAsync();
        }

        public async Task<Rating?> GetRating(int id)
        {
            return await _context.Ratings.FindAsync(id);
        }

        public async Task<Rating> AddRating(Rating rating)
        {
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();
            return rating;
        }

        public async Task<Rating?> UpdateRating(int id, Rating rating)
        {
            var existingRating = await _context.Ratings.FindAsync(id);
            if (existingRating == null) return null;

            _context.Entry(existingRating).CurrentValues.SetValues(rating);
            await _context.SaveChangesAsync();
            return existingRating;
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

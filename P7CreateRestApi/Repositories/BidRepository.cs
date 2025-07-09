using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Models.Dto;


namespace P7CreateRestApi.Repositories
{
    public class BidRepository : IBidRepository
    {
        private readonly LocalDbContext _context;

        public BidRepository(LocalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BidList>> GetBidLists()
        {
            return await _context.BidLists.ToListAsync();
        }

        public async Task<BidList?> GetBidList(int id)
        {
            return await _context.BidLists.FindAsync(id);
        }

        public async Task<BidList> AddBidList(BidList bidList)
        {
            _context.BidLists.Add(bidList);
            await _context.SaveChangesAsync();
            return bidList;
        }

        public async Task<BidList?> UpdateBidList(int id, BidList bidList)
        {
            var existingBid = await _context.BidLists.FindAsync(id);
            if (existingBid == null) return null;

            _context.Entry(existingBid).CurrentValues.SetValues(bidList);
            await _context.SaveChangesAsync();
            return existingBid;
        }

        public async Task<bool> DeleteBidList(int id)
        {
            var bidList = await _context.BidLists.FindAsync(id);
            if (bidList == null) return false;

            _context.BidLists.Remove(bidList);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;


namespace P7CreateRestApi.Repositories
{
    public class TradeRepository : ITradeRepository
    {

        private readonly LocalDbContext _context;

        public TradeRepository(LocalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Trade>> GetTrades()
        {
            return await _context.Trades.ToListAsync();
        }

        public async Task<Trade?> GetTradeById(int id)
        {
            return await _context.Trades.FindAsync(id);
        }

        public async Task<Trade> AddTrade(Trade trade)
        {
            _context.Trades.Add(trade);
            await _context.SaveChangesAsync();
            return trade;
        }

        public async Task<Trade?> UpdateTrade(Trade trade)
        {
            var existing = await _context.Trades.FindAsync(trade.TradeId);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(trade);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteTrade(int id)
        {
            var trade = await _context.Trades.FindAsync(id);
            if (trade == null) return false;

            _context.Trades.Remove(trade);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

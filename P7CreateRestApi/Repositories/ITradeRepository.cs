using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Models.Dto;

namespace P7CreateRestApi.Repositories
{
    public interface ITradeRepository
    {
        Task<IEnumerable<Trade>> GetTrades();
        Task<Trade?> GetTradeById(int id);
        Task<Trade> AddTrade(Trade trade);
        Task<Trade?> UpdateTrade(int id, Trade trade);
        Task<bool> DeleteTrade(int id);
    }
}

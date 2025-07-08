using P7CreateRestApi.Models.Dto;

namespace P7CreateRestApi.Repositories
{
    public interface ITradeRepository
    {
        Task<IEnumerable<TradeDto>> GetTrades();
        Task<TradeDto?> GetTrade(int id);
        Task<TradeDto> AddTrade(TradeDto tradeDto);
        Task<TradeDto?> UpdateTrade(int id, TradeDto tradeDto);
        Task<bool> DeleteTrade(int id);
    }
}

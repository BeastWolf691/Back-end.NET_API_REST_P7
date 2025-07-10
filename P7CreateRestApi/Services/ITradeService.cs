using P7CreateRestApi.Models.Dto;

namespace P7CreateRestApi.Services
{
    public interface ITradeService
    {
        Task<IEnumerable<TradeDto>> GetTrades();
        Task<TradeDto?> GetTradeById(int id);
        Task<TradeDto> AddTrade(TradeDto tradeDto);
        Task<TradeDto?> UpdateTrade(int id, TradeDto tradeDto);
        Task<bool> DeleteTrade(int id);
    }
}

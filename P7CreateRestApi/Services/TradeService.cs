using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Models.Dto;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Services
{
    public class TradeService : ITradeService
    {

        private readonly ITradeRepository _tradeRepository;

        public TradeService(ITradeRepository tradeRepository)
        {
            _tradeRepository = tradeRepository;
        }

        public async Task<IEnumerable<TradeDto>> GetTrades()
        {
            return await _tradeRepository.GetTrades();
        }

        public async Task<TradeDto?> GetTrade(int id)
        {
            return await _tradeRepository.GetTrade(id);
        }

        public async Task<TradeDto> AddTrade(TradeDto tradeDto)
        {
            return await _tradeRepository.AddTrade(tradeDto);
        }

        public async Task<TradeDto?> UpdateTrade(int id, TradeDto tradeDto)
        {
            return await _tradeRepository.UpdateTrade(id, tradeDto);
        }

        public async Task<bool> DeleteTrade(int id)
        {
            return await _tradeRepository.DeleteTrade(id);
        }
    }
}

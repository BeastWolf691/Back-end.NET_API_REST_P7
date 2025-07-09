using AutoMapper;
using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Models.Dto;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Services
{
    public class TradeService : ITradeService
    {

        private readonly ITradeRepository _repository;
        private readonly IMapper _mapper;

        public TradeService(ITradeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TradeDto>> GetTrades()
        {
            var trades = await _repository.GetTrades();
            return _mapper.Map<IEnumerable<TradeDto>>(trades);
        }

        public async Task<TradeDto?> GetTradeById(int id)
        {
            var trade = await _repository.GetTradeById(id);
            return trade == null ? null : _mapper.Map<TradeDto>(trade);
        }

        public async Task<TradeDto> AddTrade(TradeDto tradeDto)
        {
            var trade = _mapper.Map<Trade>(tradeDto);
            var createdTrade = await _repository.AddTrade(trade);
            return _mapper.Map<TradeDto>(createdTrade);
        }

        public async Task<TradeDto?> UpdateTrade(TradeDto tradeDto)
        {
            var trade = _mapper.Map<Trade>(tradeDto);
            var updatedTrade = await _repository.UpdateTrade(trade);
            return updatedTrade == null ? null : _mapper.Map<TradeDto>(updatedTrade);
        }

        public async Task<bool> DeleteTrade(int id)
        {
            return await _repository.DeleteTrade(id);
        }
    }
}

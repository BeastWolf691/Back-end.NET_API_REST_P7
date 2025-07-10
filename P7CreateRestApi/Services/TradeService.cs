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
        private void ValidateTrade(TradeDto tradeDto)
        {
            // Validation des quantités et prix (pas de négatif)
            if (tradeDto.BuyQuantity.HasValue && tradeDto.BuyQuantity.Value < 0)
                throw new ArgumentException("La quantité achetée ne peut pas être négative.");

            if (tradeDto.SellQuantity.HasValue && tradeDto.SellQuantity.Value < 0)
                throw new ArgumentException("La quantité vendue ne peut pas être négative.");

            if (tradeDto.BuyPrice.HasValue && tradeDto.BuyPrice.Value < 0)
                throw new ArgumentException("Le prix d'achat ne peut pas être négatif.");

            if (tradeDto.SellPrice.HasValue && tradeDto.SellPrice.Value < 0)
                throw new ArgumentException("Le prix de vente ne peut pas être négatif.");

            // Validation des champs obligatoires (string non null ou vide)
            if (string.IsNullOrWhiteSpace(tradeDto.Account))
                throw new ArgumentException("Le champ Account est obligatoire.", nameof(tradeDto.Account));
            
            if (string.IsNullOrWhiteSpace(tradeDto.AccountType))
                throw new ArgumentException("Le champ AccountType est obligatoire.", nameof(tradeDto.AccountType));
            
            if (string.IsNullOrWhiteSpace(tradeDto.TradeSecurity))
                throw new ArgumentException("Le champ TradeSecurity est obligatoire.", nameof(tradeDto.TradeSecurity));
            
            if (string.IsNullOrWhiteSpace(tradeDto.TradeStatus))
                throw new ArgumentException("Le champ TradeStatus est obligatoire.", nameof(tradeDto.TradeStatus));
            
            if (string.IsNullOrWhiteSpace(tradeDto.Trader))
                throw new ArgumentException("Le champ Trader est obligatoire.", nameof(tradeDto.Trader));
            
            if (string.IsNullOrWhiteSpace(tradeDto.Benchmark))
                throw new ArgumentException("Le champ Benchmark est obligatoire.", nameof(tradeDto.Benchmark));
            
            if (string.IsNullOrWhiteSpace(tradeDto.Book))
                throw new ArgumentException("Le champ Book est obligatoire.", nameof(tradeDto.Book));
            
            if (string.IsNullOrWhiteSpace(tradeDto.CreationName))
                throw new ArgumentException("Le champ CreationName est obligatoire.", nameof(tradeDto.CreationName));
            
            if (string.IsNullOrWhiteSpace(tradeDto.RevisionName))
                throw new ArgumentException("Le champ RevisionName est obligatoire.", nameof(tradeDto.RevisionName));
            
            if (string.IsNullOrWhiteSpace(tradeDto.DealName))
                throw new ArgumentException("Le champ DealName est obligatoire.", nameof(tradeDto.DealName));
            
            if (string.IsNullOrWhiteSpace(tradeDto.DealType))
                throw new ArgumentException("Le champ DealType est obligatoire.", nameof(tradeDto.DealType));
            
            if (string.IsNullOrWhiteSpace(tradeDto.SourceListId))
                throw new ArgumentException("Le champ SourceListId est obligatoire.", nameof(tradeDto.SourceListId));
            
            if (string.IsNullOrWhiteSpace(tradeDto.Side))
                throw new ArgumentException("Le champ Side est obligatoire.", nameof(tradeDto.Side));
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
            ValidateTrade(tradeDto);
            var trade = _mapper.Map<Trade>(tradeDto);
            var createdTrade = await _repository.AddTrade(trade);
            return _mapper.Map<TradeDto>(createdTrade);
        }

        public async Task<TradeDto?> UpdateTrade(int id, TradeDto tradeDto)
        {
            var trade = await _repository.GetTradeById(id);
            if (trade == null) return null;

            if (!string.IsNullOrWhiteSpace(tradeDto.Account))
                trade.Account = tradeDto.Account;

            if (!string.IsNullOrWhiteSpace(tradeDto.AccountType))
                trade.AccountType = tradeDto.AccountType;

            if (tradeDto.BuyQuantity.HasValue)
                trade.BuyQuantity = tradeDto.BuyQuantity;

            if (tradeDto.SellQuantity.HasValue)
                trade.SellQuantity = tradeDto.SellQuantity;

            if (tradeDto.BuyPrice.HasValue)
                trade.BuyPrice = tradeDto.BuyPrice;

            if (tradeDto.SellPrice.HasValue)
                trade.SellPrice = tradeDto.SellPrice;

            if (!string.IsNullOrWhiteSpace(tradeDto.TradeSecurity))
                trade.TradeSecurity = tradeDto.TradeSecurity;

            if (!string.IsNullOrWhiteSpace(tradeDto.TradeStatus))
                trade.TradeStatus = tradeDto.TradeStatus;

            if (!string.IsNullOrWhiteSpace(tradeDto.Trader))
                trade.Trader = tradeDto.Trader;

            if (!string.IsNullOrWhiteSpace(tradeDto.Benchmark))
                trade.Benchmark = tradeDto.Benchmark;

            if (!string.IsNullOrWhiteSpace(tradeDto.Book))
                trade.Book = tradeDto.Book;

            if (!string.IsNullOrWhiteSpace(tradeDto.CreationName))
                trade.CreationName = tradeDto.CreationName;

            if (!string.IsNullOrWhiteSpace(tradeDto.RevisionName))
                trade.RevisionName = tradeDto.RevisionName;

            if (!string.IsNullOrWhiteSpace(tradeDto.DealName))
                trade.DealName = tradeDto.DealName;

            if (!string.IsNullOrWhiteSpace(tradeDto.DealType))
                trade.DealType = tradeDto.DealType;

            if (!string.IsNullOrWhiteSpace(tradeDto.SourceListId))
                trade.SourceListId = tradeDto.SourceListId;

            if (!string.IsNullOrWhiteSpace(tradeDto.Side))
                trade.Side = tradeDto.Side;

            var dtoToValidate = _mapper.Map<TradeDto>(trade);
            ValidateTrade(dtoToValidate);

            await _repository.UpdateTrade(id, trade);
            return _mapper.Map<TradeDto>(trade);
        }

        public async Task<bool> DeleteTrade(int id)
        {
            return await _repository.DeleteTrade(id);
        }
    }
}

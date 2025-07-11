using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Services
{
    public class TradeService : ITradeService
    {

        private readonly ITradeRepository _repository;

        public TradeService(ITradeRepository repository)
        {
            _repository = repository;
        }
        private void ValidateTrade(Trade trade)
        {
            // Validation des quantités et prix (pas de négatif)
            if (trade.BuyQuantity.HasValue && trade.BuyQuantity.Value < 0)
                throw new ArgumentException("La quantité achetée ne peut pas être négative.");

            if (trade.SellQuantity.HasValue && trade.SellQuantity.Value < 0)
                throw new ArgumentException("La quantité vendue ne peut pas être négative.");

            if (trade.BuyPrice.HasValue && trade.BuyPrice.Value < 0)
                throw new ArgumentException("Le prix d'achat ne peut pas être négatif.");

            if (trade.SellPrice.HasValue && trade.SellPrice.Value < 0)
                throw new ArgumentException("Le prix de vente ne peut pas être négatif.");

            // Validation des champs obligatoires (string non null ou vide)
            if (string.IsNullOrWhiteSpace(trade.Account))
                throw new ArgumentException("Le champ Account est obligatoire.", nameof(trade.Account));
            
            if (string.IsNullOrWhiteSpace(trade.AccountType))
                throw new ArgumentException("Le champ AccountType est obligatoire.", nameof(trade.AccountType));
            
            if (string.IsNullOrWhiteSpace(trade.TradeSecurity))
                throw new ArgumentException("Le champ TradeSecurity est obligatoire.", nameof(trade.TradeSecurity));
            
            if (string.IsNullOrWhiteSpace(trade.TradeStatus))
                throw new ArgumentException("Le champ TradeStatus est obligatoire.", nameof(trade.TradeStatus));
            
            if (string.IsNullOrWhiteSpace(trade.Trader))
                throw new ArgumentException("Le champ Trader est obligatoire.", nameof(trade.Trader));
            
            if (string.IsNullOrWhiteSpace(trade.Benchmark))
                throw new ArgumentException("Le champ Benchmark est obligatoire.", nameof(trade.Benchmark));
            
            if (string.IsNullOrWhiteSpace(trade.Book))
                throw new ArgumentException("Le champ Book est obligatoire.", nameof(trade.Book));
            
            if (string.IsNullOrWhiteSpace(trade.CreationName))
                throw new ArgumentException("Le champ CreationName est obligatoire.", nameof(trade.CreationName));
            
            if (string.IsNullOrWhiteSpace(trade.RevisionName))
                throw new ArgumentException("Le champ RevisionName est obligatoire.", nameof(trade.RevisionName));
            
            if (string.IsNullOrWhiteSpace(trade.DealName))
                throw new ArgumentException("Le champ DealName est obligatoire.", nameof(trade.DealName));
            
            if (string.IsNullOrWhiteSpace(trade.DealType))
                throw new ArgumentException("Le champ DealType est obligatoire.", nameof(trade.DealType));
            
            if (string.IsNullOrWhiteSpace(trade.SourceListId))
                throw new ArgumentException("Le champ SourceListId est obligatoire.", nameof(trade.SourceListId));
            
            if (string.IsNullOrWhiteSpace(trade.Side))
                throw new ArgumentException("Le champ Side est obligatoire.", nameof(trade.Side));
        }

        public async Task<IEnumerable<Trade>> GetTrades()
        {
            return await _repository.GetTrades();
        }

        public async Task<Trade?> GetTradeById(int id)
        {
            return await _repository.GetTradeById(id);
        }

        public async Task<Trade> AddTrade(Trade trade)
        {
            ValidateTrade(trade);
            return await _repository.AddTrade(trade);
        }

        public async Task<Trade?> UpdateTrade(int id, Trade trade)
        {
            var existing = await _repository.GetTradeById(id);
            if (existing == null) return null;

            existing.Account = !string.IsNullOrWhiteSpace(trade.Account) ? trade.Account : existing.Account;
            existing.AccountType = !string.IsNullOrWhiteSpace(trade.AccountType) ? trade.AccountType : existing.AccountType;
            existing.BuyQuantity = trade.BuyQuantity.HasValue ? trade.BuyQuantity : existing.BuyQuantity;
            existing.SellQuantity = trade.SellQuantity.HasValue ? trade.SellQuantity : existing.SellQuantity;
            existing.BuyPrice = trade.BuyPrice.HasValue ? trade.BuyPrice : existing.BuyPrice;
            existing.SellPrice = trade.SellPrice.HasValue ? trade.SellPrice : existing.SellPrice;
            existing.TradeSecurity = !string.IsNullOrWhiteSpace(trade.TradeSecurity) ? trade.TradeSecurity : existing.TradeSecurity;
            existing.TradeStatus = !string.IsNullOrWhiteSpace(trade.TradeStatus) ? trade.TradeStatus : existing.TradeStatus;
            existing.Trader = !string.IsNullOrWhiteSpace(trade.Trader) ? trade.Trader : existing.Trader;
            existing.Benchmark = !string.IsNullOrWhiteSpace(trade.Benchmark) ? trade.Benchmark : existing.Benchmark;
            existing.Book = !string.IsNullOrWhiteSpace(trade.Book) ? trade.Book : existing.Book;
            existing.CreationName = !string.IsNullOrWhiteSpace(trade.CreationName) ? trade.CreationName : existing.CreationName;
            existing.RevisionName = !string.IsNullOrWhiteSpace(trade.RevisionName) ? trade.RevisionName : existing.RevisionName;
            existing.DealName = !string.IsNullOrWhiteSpace(trade.DealName) ? trade.DealName : existing.DealName;
            existing.DealType = !string.IsNullOrWhiteSpace(trade.DealType) ? trade.DealType : existing.DealType;
            existing.SourceListId = !string.IsNullOrWhiteSpace(trade.SourceListId) ? trade.SourceListId : existing.SourceListId;
            existing.Side = !string.IsNullOrWhiteSpace(trade.Side) ? trade.Side : existing.Side;

            ValidateTrade(existing);

            await _repository.UpdateTrade(id, existing);
            return existing;
        }

        public async Task<bool> DeleteTrade(int id)
        {
            return await _repository.DeleteTrade(id);
        }
    }
}

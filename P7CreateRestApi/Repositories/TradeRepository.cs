using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Models.Dto;


namespace P7CreateRestApi.Repositories
{
    public class TradeRepository : ITradeRepository
    {

        private readonly LocalDbContext _context;

        public TradeRepository(LocalDbContext context)
        {
            _context = context;
        }

        private static TradeDto ToDto(Trade trade) => new TradeDto
        {
            TradeId = trade.TradeId,
            Account = trade.Account,
            AccountType = trade.AccountType,
            BuyQuantity = trade.BuyQuantity,
            SellQuantity = trade.SellQuantity,
            BuyPrice = trade.BuyPrice,
            SellPrice = trade.SellPrice,
            TradeDate = trade.TradeDate,
            TradeSecurity = trade.TradeSecurity,
            TradeStatus = trade.TradeStatus,
            Trader = trade.Trader,
            Benchmark = trade.Benchmark,
            Book = trade.Book,
            CreationName = trade.CreationName,
            CreationDate = trade.CreationDate,
            RevisionName = trade.RevisionName,
            RevisionDate = trade.RevisionDate,
            DealName = trade.DealName,
            DealType = trade.DealType,
            SourceListId = trade.SourceListId,
            Side = trade.Side
        };

        private static Trade ToEntity(TradeDto tradeDto) => new Trade
        {
            TradeId = tradeDto.TradeId,
            Account = tradeDto.Account,
            AccountType = tradeDto.AccountType,
            BuyQuantity = tradeDto.BuyQuantity,
            SellQuantity = tradeDto.SellQuantity,
            BuyPrice = tradeDto.BuyPrice,
            SellPrice = tradeDto.SellPrice,
            TradeDate = tradeDto.TradeDate,
            TradeSecurity = tradeDto.TradeSecurity,
            TradeStatus = tradeDto.TradeStatus,
            Trader = tradeDto.Trader,
            Benchmark = tradeDto.Benchmark,
            Book = tradeDto.Book,
            CreationName = tradeDto.CreationName,
            CreationDate = tradeDto.CreationDate,
            RevisionName = tradeDto.RevisionName,
            RevisionDate = tradeDto.RevisionDate,
            DealName = tradeDto.DealName,
            DealType = tradeDto.DealType,
            SourceListId = tradeDto.SourceListId,
            Side = tradeDto.Side
        };

        public async Task<IEnumerable<TradeDto>> GetTrades()
        {
            return await _context.Trades
                .Select(trade => ToDto(trade))
                .ToListAsync();
        }

        public async Task<TradeDto?> GetTrade(int id)
        {
            var trade = await _context.Trades.FindAsync(id);
            return trade is null ? null : ToDto(trade);
        }

        public async Task<TradeDto> AddTrade(TradeDto tradeDto)
        {
            var trade = ToEntity(tradeDto);
            _context.Trades.Add(trade);
            await _context.SaveChangesAsync();
            return ToDto(trade);
        }

        public async Task<TradeDto?> UpdateTrade(int id, TradeDto tradeDto)
        {
            var trade = await _context.Trades.FindAsync(id);
            if (trade == null) return null;

            // Mise à jour des champs
            trade.Account = tradeDto.Account;
            trade.AccountType = tradeDto.AccountType;
            trade.BuyQuantity = tradeDto.BuyQuantity;
            trade.SellQuantity = tradeDto.SellQuantity;
            trade.BuyPrice = tradeDto.BuyPrice;
            trade.SellPrice = tradeDto.SellPrice;
            trade.TradeDate = tradeDto.TradeDate;
            trade.TradeSecurity = tradeDto.TradeSecurity;
            trade.TradeStatus = tradeDto.TradeStatus;
            trade.Trader = tradeDto.Trader;
            trade.Benchmark = tradeDto.Benchmark;
            trade.Book = tradeDto.Book;
            trade.CreationName = tradeDto.CreationName;
            trade.CreationDate = tradeDto.CreationDate;
            trade.RevisionName = tradeDto.RevisionName;
            trade.RevisionDate = tradeDto.RevisionDate;
            trade.DealName = tradeDto.DealName;
            trade.DealType = tradeDto.DealType;
            trade.SourceListId = tradeDto.SourceListId;
            trade.Side = tradeDto.Side;

            _context.Trades.Update(trade);
            await _context.SaveChangesAsync();
            return ToDto(trade);
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

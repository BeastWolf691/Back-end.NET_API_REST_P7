using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;
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

        private static BidListDto ToDto(BidList bid) => new BidListDto
        {
            BidListId = bid.BidListId,
            Account = bid.Account,
            BidType = bid.BidType,
            BidQuantity = bid.BidQuantity
        };

        private static BidList ToEntity(BidListDto bidListDto) => new BidList
        {
            Account = bidListDto.Account,
            BidType = bidListDto.BidType,
            BidQuantity = bidListDto.BidQuantity
        };

        public async Task<IEnumerable<BidListDto>> GetBidLists()
        {
            return await _context.BidLists
                .Select(bid => ToDto(bid))
                .ToListAsync();
        }

        public async Task<BidListDto?> GetBidList(int id)
        {
            var bidList = await _context.BidLists.FindAsync(id);
            return bidList is null ? null : ToDto(bidList);
        }

        public async Task<BidListDto> AddBidList(BidListDto bidListDto)
        {
            var bidList = ToEntity(bidListDto);
            _context.BidLists.Add(bidList);
            await _context.SaveChangesAsync();
            return ToDto(bidList);
        }

        public async Task<BidListDto?> UpdateBidList(int id, BidListDto bidListDto)
        {
            var bidList = await _context.BidLists.FindAsync(id);

            if (bidList == null) return null;

            bidList.Account = bidListDto.Account;
            bidList.BidType = bidListDto.BidType;
            bidList.BidQuantity = bidListDto.BidQuantity;

            _context.BidLists.Update(bidList);
            await _context.SaveChangesAsync();

            return ToDto(bidList);
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
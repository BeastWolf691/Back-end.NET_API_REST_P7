using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Models.Dto;

namespace P7CreateRestApi.Services
{
    public interface IBidService
    {
        Task<IEnumerable<BidList>> GetBidLists();
        Task<BidList?> GetBidList(int id);
        Task<BidList> AddBidList(BidList bidList);
        Task<BidList?> UpdateBidList(int id, BidList bidList);
        Task<bool> DeleteBidList(int id);
    }
}

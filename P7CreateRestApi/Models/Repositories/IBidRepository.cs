using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Models.Dto;

namespace P7CreateRestApi.Models.Repositories
{
    public interface IBidRepository
    {
        Task<IEnumerable<BidListDto>> GetBidLists();
        Task<BidListDto?> GetBidList(int id);
        Task<BidList> AddBidList(BidListDto bidListDto);
        Task<BidList?> UpdateBidList(int id, BidListDto bidListDto);
            Task<bool> DeleteBidList(int id);
    }
}

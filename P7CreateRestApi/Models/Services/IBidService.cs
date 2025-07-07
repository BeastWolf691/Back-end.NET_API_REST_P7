using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Models.Dto;

namespace P7CreateRestApi.Models.Services
{
    public interface IBidService
    {
        Task<IEnumerable<BidListDto>> GetBidLists();
        Task<BidListDto?> GetBidList(int id);
        Task<BidListDto> AddBidList(BidListDto bidListDto);
        Task<BidListDto?> UpdateBidList(int id, BidListDto bidListDto);
        Task<bool> DeleteBidList(int id);
    }
}

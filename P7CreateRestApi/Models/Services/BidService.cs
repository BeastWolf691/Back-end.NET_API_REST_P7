using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Models.Dto;
using P7CreateRestApi.Models.Repositories;

namespace P7CreateRestApi.Models.Services
{
    public class BidService
    {

        private readonly IBidRepository _bidRepository;

        public BidService(IBidRepository bidRepository)
        {
            _bidRepository = bidRepository;
        }

        public async Task<IEnumerable<BidListDto>> GetBidLists()
        {
            return await _bidRepository.GetBidLists();
        }

        public async Task<BidListDto?> GetBidList(int id)
        {
            return await _bidRepository.GetBidList(id);
        }

        public async Task<BidList> AddBidList(BidListDto bidListDto)
        {
            return await _bidRepository.AddBidList(bidListDto);
        }

        public async Task<BidList?> UpdateBidList(int id, BidListDto bidListDto)
        {
            return await _bidRepository.UpdateBidList(id, bidListDto);
        }

        public async Task<bool> DeleteBidList(int id)
        {
            return await _bidRepository.DeleteBidList(id);
        }
    }
}

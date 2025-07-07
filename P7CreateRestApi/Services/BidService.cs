using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Models.Dto;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Services
{
    public class BidService : IBidService
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

        public async Task<BidListDto> AddBidList(BidListDto bidListDto)
        {
            return await _bidRepository.AddBidList(bidListDto);
        }

        public async Task<BidListDto?> UpdateBidList(int id, BidListDto bidListDto)
        {
            return await _bidRepository.UpdateBidList(id, bidListDto);
        }

        public async Task<bool> DeleteBidList(int id)
        {
            return await _bidRepository.DeleteBidList(id);
        }
    }
}

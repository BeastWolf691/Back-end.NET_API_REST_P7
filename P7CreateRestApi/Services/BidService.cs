using AutoMapper;
using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Models.Dto;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Services
{
    public class BidService : IBidService
    {

        private readonly IBidRepository _bidRepository;
        private readonly IMapper _mapper;

        public BidService(IBidRepository bidRepository, IMapper mapper)
        {
            _bidRepository = bidRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BidListDto>> GetBidLists()
        {
            var bids = await _bidRepository.GetBidLists();
            return _mapper.Map<IEnumerable<BidListDto>>(bids);
        }

        public async Task<BidListDto?> GetBidList(int id)
        {
            var bid = await _bidRepository.GetBidList(id);
            return bid == null ? null : _mapper.Map<BidListDto>(bid);
        }

        public async Task<BidListDto> AddBidList(BidListDto bidListDto)
        {
            var bidEntity = _mapper.Map<BidList>(bidListDto);
            var addedBid = await _bidRepository.AddBidList(bidEntity);
            return _mapper.Map<BidListDto>(addedBid);
        }

        public async Task<BidListDto?> UpdateBidList(int id, BidListDto bidListDto)
        {
            var bidEntity = _mapper.Map<BidList>(bidListDto);
            var updatedBid = await _bidRepository.UpdateBidList(id, bidEntity);
            return updatedBid == null ? null : _mapper.Map<BidListDto>(updatedBid);
        }

        public async Task<bool> DeleteBidList(int id)
        {
            return await _bidRepository.DeleteBidList(id);
        }
    }
}

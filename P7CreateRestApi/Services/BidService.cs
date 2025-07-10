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

        private void ValidateBidList(BidListDto bidListDto)
        {
            if (string.IsNullOrWhiteSpace(bidListDto.Account))
                throw new ArgumentException("Le compte est obligatoire.");

            if (string.IsNullOrWhiteSpace(bidListDto.BidType))
                throw new ArgumentException("Le type de l'offre est obligatoire.");

            if (bidListDto.BidQuantity.HasValue && bidListDto.BidQuantity < 0)
                throw new ArgumentException("La quantité ne peut pas être négative.");
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
            ValidateBidList(bidListDto);
            var bidEntity = _mapper.Map<BidList>(bidListDto);

            var addedBid = await _bidRepository.AddBidList(bidEntity);
            return _mapper.Map<BidListDto>(addedBid);
        }

        public async Task<BidListDto?> UpdateBidList(int id, BidListDto bidListDto)
        {
            var existing = await _bidRepository.GetBidList(id);
            if (existing == null) return null;

            if (!string.IsNullOrWhiteSpace(bidListDto.Account))
                existing.Account = bidListDto.Account;

            if (!string.IsNullOrWhiteSpace(bidListDto.BidType))
                existing.BidType = bidListDto.BidType;

            if (bidListDto.BidQuantity.HasValue)
                existing.BidQuantity = bidListDto.BidQuantity.Value;

            var dtoToValidate = _mapper.Map<BidListDto>(existing);
            ValidateBidList(dtoToValidate);

            await _bidRepository.UpdateBidList(id, existing);
            return _mapper.Map<BidListDto>(existing);
        }

        public async Task<bool> DeleteBidList(int id)
        {
            return await _bidRepository.DeleteBidList(id);
        }
    }
}

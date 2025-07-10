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

        private void ValidateBidList(BidList bid)
        {
            if (string.IsNullOrWhiteSpace(bid.Account))
                throw new ArgumentException("Le compte est obligatoire.");

            if (string.IsNullOrWhiteSpace(bid.BidType))
                throw new ArgumentException("Le type de l'offre est obligatoire.");

            if (bid.BidQuantity < 0)
                throw new ArgumentException("La quantité ne peut pas être négative.");
        }

        public async Task<IEnumerable<BidList>> GetBidLists()
        {
            return await _bidRepository.GetBidLists();
        }

        public async Task<BidList?> GetBidList(int id)
        {
            return await _bidRepository.GetBidList(id);
        }

        public async Task<BidList> AddBidList(BidList bidList)
        {
            ValidateBidList(bidList);
            return await _bidRepository.AddBidList(bidList);
        }

        public async Task<BidList?> UpdateBidList(int id, BidList bidList)
        {
            var existing = await _bidRepository.GetBidList(id);
            if (existing == null) return null;

            existing.Account = bidList.Account ?? existing.Account;
            existing.BidType = bidList.BidType ?? existing.BidType;
            if (bidList.BidQuantity >= 0) existing.BidQuantity = bidList.BidQuantity;

            ValidateBidList(existing);

            await _bidRepository.UpdateBidList(id, existing);
            return existing;
        }

        public async Task<bool> DeleteBidList(int id)
        {
            return await _bidRepository.DeleteBidList(id);
        }
    }
}

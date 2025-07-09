using AutoMapper;
using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Models.Dto;

namespace P7CreateRestApi.MappingProfiles
{
    public class BidListProfile : Profile
    {
        public BidListProfile()
        {
            // Mapping BidList -> BidListDto
            CreateMap<BidList, BidListDto>().ReverseMap();
        }
    }
}

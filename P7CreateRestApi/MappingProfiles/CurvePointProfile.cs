using AutoMapper;
using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Models.Dto;

namespace P7CreateRestApi.MappingProfiles
{
    public class CurvePointProfile : Profile
    {
        public CurvePointProfile()
        {
            CreateMap<CurvePoint, CurvePointDto>().ReverseMap();
        }
    }
}

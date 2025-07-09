using AutoMapper;
using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Models.Dto;

public class TradeProfile : Profile
{
    public TradeProfile()
    {
        CreateMap<Trade, TradeDto>().ReverseMap();
    }
}

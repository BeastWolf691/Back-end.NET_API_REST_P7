using AutoMapper;
using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Models.Dto;

public class RuleProfile : Profile
{
    public RuleProfile()
    {
        CreateMap<RuleName, RuleDto>().ReverseMap();
    }
}

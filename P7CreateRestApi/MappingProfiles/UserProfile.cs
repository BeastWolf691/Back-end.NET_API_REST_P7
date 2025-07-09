using AutoMapper;
using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Models.Dto;

namespace P7CreateRestApi.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserReadDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}

using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Models.Dto;

namespace P7CreateRestApi.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserReadDto>> GetAllUsersAsync();
        Task<UserReadDto?> GetUserByIdAsync(string id);
        Task<UserReadDto?> CreateUserAsync(UserDto userDto);
        Task<bool> UpdateUserAsync(string id, UserDto userDto);
        Task<bool> DeleteUserAsync(string id);
    }

}

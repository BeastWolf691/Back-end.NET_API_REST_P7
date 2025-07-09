using Microsoft.AspNetCore.Identity;
using P7CreateRestApi.Models.Dto;

namespace P7CreateRestApi.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserReadDto>> GetAllUsers();
        Task<UserReadDto?> GetUserById(string id);
        Task<IdentityResult> AddUser(UserDto userDto, string password);
        Task<IdentityResult> UpdateUser(UserDto userDto);
        Task<IdentityResult> DeleteUser(string id);
    }
}

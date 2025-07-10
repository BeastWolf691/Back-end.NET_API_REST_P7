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
        Task<IList<string>?> GetUserRoles(string userId);
        Task<IdentityResult> AddRoleToUser(string userId, string role);
        Task<IdentityResult> RemoveRoleFromUser(string userId, string role);
        Task<IdentityResult> CreateRole(string roleName);
        Task<IdentityResult> DeleteRole(string roleName);
    }
}

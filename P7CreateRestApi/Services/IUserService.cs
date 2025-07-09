using P7CreateRestApi.Models.Dto;

namespace P7CreateRestApi.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserReadDto>> GetAllUsersAsync();
        Task<UserReadDto?> GetUserByIdAsync(string id);
        Task<UserReadDto?> CreateUserAsync(UserDto userDto);
        Task<bool> UpdateUserAsync(string id, UserDto userDto);
        Task<bool> DeleteUserAsync(string id);
    }
}

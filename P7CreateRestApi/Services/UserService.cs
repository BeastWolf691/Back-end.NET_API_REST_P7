using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Models.Dto;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserReadDto>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<UserReadDto?> GetUserByIdAsync(string id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<UserReadDto?> CreateUserAsync(UserDto userDto)
        {
            return await _userRepository.CreateUserAsync(userDto);
        }

        public async Task<bool> UpdateUserAsync(string id, UserDto userDto)
        {
            return await _userRepository.UpdateUserAsync(id, userDto);
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            return await _userRepository.DeleteUserAsync(id);
        }
    }
}

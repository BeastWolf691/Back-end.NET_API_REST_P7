using AutoMapper;
using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Models.Dto;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserReadDto>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return _mapper.Map<IEnumerable<UserReadDto>>(users);
        }

        public async Task<UserReadDto?> GetUserById(string id)
        {
            var user = await _userRepository.GetUserById(id);
            return user == null ? null : _mapper.Map<UserReadDto>(user);
        }

        public async Task<IdentityResult> AddUser(UserDto userDto, string password)
        {
            var user = _mapper.Map<User>(userDto);
            return await _userRepository.AddUser(user, password);
        }

        public async Task<IdentityResult> UpdateUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            return await _userRepository.UpdateUser(user);
        }

        public async Task<IdentityResult> DeleteUser(string id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = $"User with ID {id} not found."
                });
            }

            return await _userRepository.DeleteUser(user);
        }
    }
}

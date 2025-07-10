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
            var user = await _userRepository.GetUserById(userDto.Id);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = $"User with ID {userDto.Id} not found."
                });
            }

            _mapper.Map(userDto, user);
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

        public async Task<IList<string>?> GetUserRoles(string userId)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null)
            {
                return null;
            }

            return await _userRepository.GetUserRoles(user);
        }

        public async Task<IdentityResult> AddRoleToUser(string userId, string role)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = $"User with ID {userId} not found."
                });
            }

            return await _userRepository.AddRoleToUser(user, role);
        }

        public async Task<IdentityResult> RemoveRoleFromUser(string userId, string role)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = $"User with ID {userId} not found."
                });
            }

            return await _userRepository.RemoveRoleFromUser(user, role);
        }

        public async Task<IdentityResult> CreateRole(string roleName)
        {
            if (await _userRepository.RoleExists(roleName))
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = $"Role '{roleName}' already exists."
                });
            }

            return await _userRepository.CreateRole(roleName);
        }

        public async Task<IdentityResult> DeleteRole(string roleName)
        {
            var result = await _userRepository.DeleteRole(roleName);
            if (!result.Succeeded)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = $"Failed to delete role '{roleName}'."
                });
            }

            return result;
        }
    }
}

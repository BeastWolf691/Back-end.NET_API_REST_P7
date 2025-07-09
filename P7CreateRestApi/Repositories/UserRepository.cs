using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using P7CreateRestApi.Data;

namespace P7CreateRestApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LocalDbContext _context;
        private readonly UserManager<User> _userManager;

        public UserRepository(LocalDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IEnumerable<UserReadDto>> GetAllUsersAsync()
        {
            var users = _userManager.Users;

            return users.Select(u => new UserReadDto
            {
                Id = u.Id,
                UserName = u.UserName,
                FullName = u.FullName
            }).ToList();
        }


        public async Task<UserReadDto?> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return null;

            return new UserReadDto
            {
                Id = user.Id,
                UserName = user.UserName,
                FullName = user.FullName
            };
        }

        public async Task<UserReadDto?> CreateUserAsync(UserDto userDto)
        {
            var user = new User
            {
                UserName = userDto.UserName,
                FullName = userDto.FullName
            };

            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (!result.Succeeded) return null;

            return new UserReadDto
            {
                Id = user.Id,
                UserName = user.UserName,
                FullName = user.FullName
            };
        }

        public async Task<bool> UpdateUserAsync(string id, UserDto userDto)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return false;

            user.UserName = userDto.UserName;
            user.FullName = userDto.FullName;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return false;

            // Modifier le mot de passe si un nouveau est fourni
            if (!string.IsNullOrEmpty(userDto.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var pwdResult = await _userManager.ResetPasswordAsync(user, token, userDto.Password);
                if (!pwdResult.Succeeded) return false;
            }

            return true;
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return false;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }
    }
}
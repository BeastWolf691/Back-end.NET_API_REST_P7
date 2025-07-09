using System.Collections.Generic;
using System.Threading.Tasks;
using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Identity;

namespace P7CreateRestApi.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User?> GetUserById(string id);
        Task<IdentityResult> AddUser(User user, string password);
        Task<IdentityResult> UpdateUser(User user);
        Task<IdentityResult> DeleteUser(User user);
    }
}

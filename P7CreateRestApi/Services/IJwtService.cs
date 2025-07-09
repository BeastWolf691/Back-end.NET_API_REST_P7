using Dot.Net.WebApi.Domain;

namespace P7CreateRestApi.Services
{
    public interface IJwtService
    {
        Task<string> GenerateJwtToken(User user);
    }
}

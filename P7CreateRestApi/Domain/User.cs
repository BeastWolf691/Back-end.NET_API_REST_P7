using Microsoft.AspNetCore.Identity;

namespace Dot.Net.WebApi.Domain
{
    public class User : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public string Role {  get; set; } = string.Empty;
    }
}

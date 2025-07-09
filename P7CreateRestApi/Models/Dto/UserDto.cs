using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models.Dto
{
//sert pour la gestion des comptes coté admin
    public class UserDto
    {
        public string Id { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
    }
}


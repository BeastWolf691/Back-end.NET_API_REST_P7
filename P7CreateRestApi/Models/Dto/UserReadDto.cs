using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models.Dto
{
    //lecture
    public class UserReadDto
    {
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
    }
}

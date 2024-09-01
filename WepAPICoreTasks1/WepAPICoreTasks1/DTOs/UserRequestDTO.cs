using System.ComponentModel.DataAnnotations;

namespace WepAPICoreTasks1.DTOs
{
    public class UserRequestDTO
    {
        public string? Username { get; set; }

        public string? Password { get; set; }


        [EmailAddress]
        public string? Email { get; set; }
    }
}

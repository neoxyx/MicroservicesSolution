using System.ComponentModel.DataAnnotations;

namespace UserService.Models.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Username or Email is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
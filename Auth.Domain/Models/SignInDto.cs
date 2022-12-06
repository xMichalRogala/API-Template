using System.ComponentModel.DataAnnotations;

namespace Auth.Domain.Models
{
    public class SignInDto
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

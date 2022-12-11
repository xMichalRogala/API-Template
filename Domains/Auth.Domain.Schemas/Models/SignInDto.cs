using System.ComponentModel.DataAnnotations;

namespace Auth.Domain.Schemas.Models
{
    public class SignInDto
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

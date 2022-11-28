using System.ComponentModel.DataAnnotations;

namespace TemplateApi.Domain.Auth.Models
{
    public class SignInDto
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

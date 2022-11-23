using System.ComponentModel.DataAnnotations.Schema;

namespace TemplateApi.Domain.Auth.Entities
{
    public sealed class UserCredential
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Login { get; set; }
        public string Salt { get; set; }
        [Column("Password")]
        public byte[] PasswordBytes { get; set; }
        public int Iterations { get; set; }
    }
}

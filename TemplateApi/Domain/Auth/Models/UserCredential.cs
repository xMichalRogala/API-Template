using System.ComponentModel.DataAnnotations.Schema;

namespace TemplateApi.Domain.Auth.Models
{
    public sealed class UserCredential
    {
        public string Login { get; set; }
        public string Salt { get; set; }
        [Column("Password")]
        public byte[] PasswordBytes { get; set; }
        [NotMapped]
        private long Password
        {
            get { return BitConverter.ToInt64(this.PasswordBytes, 0); }
            set { this.PasswordBytes = BitConverter.GetBytes(value); }
        }
        public int Iterations { get; set; }
    }
}

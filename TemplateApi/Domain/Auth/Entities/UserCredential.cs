using System.ComponentModel.DataAnnotations.Schema;

namespace TemplateApi.Domain.Auth.Entities
{
    public sealed class UserCredential
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Salt { get; set; }
        [Column("Password")]
        public byte[] PasswordBytes { get; set; }
        [NotMapped]
        private long Password
        {
            get { return BitConverter.ToInt64(PasswordBytes, 0); }
            set { PasswordBytes = BitConverter.GetBytes(value); }
        }
        public int Iterations { get; set; }
    }
}

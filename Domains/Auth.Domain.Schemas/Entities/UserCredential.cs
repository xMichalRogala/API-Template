using System.ComponentModel.DataAnnotations.Schema;
using TemplateApi.Commons.Entity.Abstract;
using TemplateApi.CQRS.Events.Abstract;

namespace Auth.Domain.Schemas.Entities
{
    public sealed class UserCredential : AggregateRoot, IEntityBase<Guid>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Login { get; set; }
        public string Salt { get; set; }
        [Column("Password")]
        public byte[] PasswordBytes { get; set; }
        public int Iterations { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}

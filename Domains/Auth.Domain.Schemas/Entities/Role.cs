using TemplateApi.Commons.Enums.Abstract;

namespace Auth.Domain.Schemas.Entities
{
    public sealed class Role : Enumeration<Role>
    {
        public static readonly Role Registered = new(1, nameof(Registered));
        public Role(int id, string name) : base(id, name)
        {
        }

        public ICollection<Permission> Permissions { get; set; }
        public ICollection<UserCredential> Credentials { get; set; }
    }
}

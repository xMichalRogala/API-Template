using TemplateApi.Commons.Entity.Abstract;

namespace Core.Domain.Entities
{
    public class User : EntityBase<Guid>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

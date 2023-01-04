using TemplateApi.Commons.Entity.Abstract;
using TemplateApi.CQRS.Events.Abstract;

namespace Core.Domain.Schemas.Entities
{
    public class User : AggregateRoot, IEntityBase<Guid>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

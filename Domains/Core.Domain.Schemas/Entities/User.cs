using Core.Domain.Messages.Events;
using Core.Domain.Schemas.Models;
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

        public User Create(UserDto userDto) //todo factory
        {
            FirstName = userDto.FirstName;
            LastName = userDto.LastName;
            Login = userDto.Login;

            Publish(new UserCreated
            {
                Login = Login,
                Password = userDto.Password
            });

            return this;
        }
    }
}

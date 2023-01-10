using Core.Domain.Schemas.Entities;
using Core.Domain.Schemas.Models;

namespace Core.Domain.Services.Abstract
{
    public interface IUserService
    {
        public Task<User> CreateUser(UserDto userDto, CancellationToken cancellationToken);
    }
}

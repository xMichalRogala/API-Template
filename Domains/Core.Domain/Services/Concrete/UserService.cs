using Core.Domain.Schemas.Entities;
using Core.Domain.Schemas.Models;
using Core.Domain.Services.Abstract;
using TemplateApi.Application.Abstract;

namespace Core.Domain.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<User> CreateUser(UserDto userDto, CancellationToken cancellationToken)
        {
            var createdUser = new User();

            createdUser.Create(userDto);

            await unitOfWork.userRepository.Add(createdUser, cancellationToken);

            var resultOfWork = await unitOfWork.Complete(cancellationToken);

            return resultOfWork ? createdUser : null;
        }
    }
}

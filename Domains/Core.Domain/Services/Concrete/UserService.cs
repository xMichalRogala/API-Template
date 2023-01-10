using Auth.Domain.Messages.Commands;
using Core.Domain.Schemas.Entities;
using Core.Domain.Schemas.Models;
using Core.Domain.Services.Abstract;
using TemplateApi.Application.Abstract;
using TemplateApi.CQRS.Commands.Abstract;

namespace Core.Domain.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICommandDispatcher commandDispatcher;

        public UserService(IUnitOfWork unitOfWork, ICommandDispatcher commandDispatcher)
        {
            this.unitOfWork = unitOfWork;
            this.commandDispatcher = commandDispatcher;
        }

        public async Task<User> CreateUser(UserDto userDto, CancellationToken cancellationToken)
        {
            var createdUser = new User();

            createdUser.Create(userDto);

            await unitOfWork.userRepository.Add(createdUser, cancellationToken);

            await commandDispatcher.DispatchAsync(new CreateUserCredentialCommand(userDto.Login, userDto.Password), cancellationToken);


            var resultOfWork = await unitOfWork.Complete(cancellationToken);

            return resultOfWork ? createdUser : null;
        }
    }
}

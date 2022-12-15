using Auth.Domain.Messages.Commands;
using Core.Domain.Schemas.Entities;
using Core.Domain.Schemas.Models;
using Microsoft.AspNetCore.Mvc;
using TemplateApi.Application.Abstract;
using TemplateApi.CQRS.Commands.Abstract;

namespace TemplateApi.Endpoints.Domains.Core
{
    public abstract class UserRequests
    {
        public static async Task<IResult> Create([FromBody] UserDto userDto,
            ICommandDispatcher commandDispatcher,
            IUnitOfWork unitOfWork,
            CancellationToken cancellationToken)
        {
            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Login = userDto.Login
            };    

            await unitOfWork.userRepository.Add(user, cancellationToken);

            await commandDispatcher.DispatchAsync(new CreateUserCredentialCommand(userDto.Login, userDto.Password), cancellationToken);

            var resultOfWork = await unitOfWork.Complete(cancellationToken);

            if (!resultOfWork)
                await commandDispatcher.DispatchAsync(new RemoveUserCredentialCommand(userDto.Login), cancellationToken);

            return resultOfWork ? Results.Ok() : Results.BadRequest();
        }
    }
}

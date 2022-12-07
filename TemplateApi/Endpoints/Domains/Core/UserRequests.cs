using Auth.Domain.Services.Abstract;
using Core.Domain.Entities;
using Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using TemplateApi.Application.Abstract;

namespace Core.Domain
{
    public class UserRequests
    {
        public async static Task<IResult> Create([FromBody] UserDto userDto,
            IAuthService authService,
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

            await authService.SaveUserCredentials(userDto.Login, userDto.Password, cancellationToken);

            var resultOfWork = await unitOfWork.Complete();

            if (!resultOfWork)
                await authService.RemoveUserCredentials(userDto.Login, cancellationToken);

            return Results.Ok();
        }
    }
}

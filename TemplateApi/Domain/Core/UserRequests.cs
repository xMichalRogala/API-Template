using TemplateApi.Domain.Auth.Services.Abstract;
using TemplateApi.Domain.Core.DAL.Abstract;
using TemplateApi.Domain.Core.Entities;
using TemplateApi.Domain.Core.Models;

namespace TemplateApi.Domain.Core
{
    public class UserRequests
    {
        public async static Task<IResult> Create(UserDto userDto, IAuthService authService, IUnitOfWork unitOfWork)
        {
            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Login = userDto.Login
            };
            await unitOfWork.userRepository.Add(user);

            await authService.SaveUserCredentials(userDto.Login, userDto.Password);

            var resultOfWork = await unitOfWork.Complete();

            if (!resultOfWork)
                await authService.RemoveUserCredentials(userDto.Login);

            return Results.Ok();
        }
    }
}

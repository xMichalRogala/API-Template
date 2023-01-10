using Core.Domain.Schemas.Models;
using Core.Domain.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace TemplateApi.Endpoints.Domains.Core
{
    public abstract class UserRequests
    {
        public static async Task<IResult> Create([FromBody] UserDto userDto,
            IUserService userService,
            CancellationToken cancellationToken)
        {
            var createdUser = await userService.CreateUser(userDto, cancellationToken);

            return Results.Ok(createdUser);
        }
    }
}

using Auth.Domain.Authorization.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Auth.Domain.Authorization;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    
    public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var userCredentialId = context.User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;

        if (!Guid.TryParse(userCredentialId, out Guid parsedUserCredentialId))
        {
            return;
        }

        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();

            var memberPermissions = await permissionService.GetPermissionsAsync(parsedUserCredentialId);

            if (memberPermissions.Contains(requirement.Permission))
            {
                context.Succeed(requirement);
            }
        }
    }
}
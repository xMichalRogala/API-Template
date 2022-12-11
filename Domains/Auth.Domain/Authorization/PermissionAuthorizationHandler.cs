using Auth.Domain.Authorization.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

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
        string userCredentialId = context.User.Claims.FirstOrDefault(
            x => x.Type == ClaimTypes.NameIdentifier)?.Value;

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
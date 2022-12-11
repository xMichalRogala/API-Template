using Auth.Domain.Authorization.Abstract;
using Auth.Domain.Schemas.Entities;
using Microsoft.EntityFrameworkCore;
using TemplateApi.Persistence.DbContexts.Auth;

namespace Auth.Domain.Authorization;

public class PermissionService : IPermissionService
{
    private readonly AuthDbContext _context;
    
    public PermissionService(AuthDbContext context)
    {
        _context = context;
    }
    
    public async Task<HashSet<string>> GetPermissionsAsync(Guid userCredentialId)
    {
        var roles = await _context.Set<UserCredential>() //todo probably better would be to cache service
            .Include(x => x.Roles)
            .ThenInclude(r => r.Permissions)
            .Where(x => x.Id == userCredentialId)
            .Select(x => x.Roles)
            .ToArrayAsync();

        return roles.SelectMany(x => x)
            .SelectMany(x => x.Permissions)
            .Select(x => x.Name)
            .ToHashSet();
    }
}
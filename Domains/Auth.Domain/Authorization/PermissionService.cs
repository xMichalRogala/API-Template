using Auth.Domain.Authorization.Abstract;
using Auth.Domain.Repositories.Abstract;
using TemplateApi.Persistence.DbContexts.Auth;

namespace Auth.Domain.Authorization;

public class PermissionService : IPermissionService
{
    // private readonly AuthDbContext _context;
    //
    // public PermissionService(IAuthRepository authRepository)
    // {
    //     _authRepository = authRepository;
    // }
    //
    // public Task<HashSet<string>> GetPermissionsAsync(Guid userCredentialId)
    // {
    //     var roles = await _context.Set
    // }
    public Task<HashSet<string>> GetPermissionsAsync(Guid userCredentialId)
    {
        throw new NotImplementedException();
    }
}
namespace Auth.Domain.Authorization.Abstract;

public interface IPermissionService
{
    Task<HashSet<string>> GetPermissionsAsync(Guid userCredentialId);
}
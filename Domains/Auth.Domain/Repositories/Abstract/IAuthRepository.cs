using Auth.Domain.Schemas.Entities;
using TemplateApi.Commons.Data.Repository;


namespace Auth.Domain.Repositories.Abstract
{
    public interface IAuthRepository : IGenericRepository<UserCredential, Guid>
    {
        Task<Role> GetRole(Role role);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

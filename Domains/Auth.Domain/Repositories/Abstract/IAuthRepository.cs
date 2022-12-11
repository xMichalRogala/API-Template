using Auth.Domain.Schemas.Entities;
using TemplateApi.Commons.Data.Repository;


namespace Auth.Domain.Repositories.Abstract
{
    public interface IAuthRepository : IGenericRepository<UserCredential, Guid>
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

using TemplateApi.Commons.Data.Repository;
using Auth.Domain.Entities;

namespace Auth.Domain.DAL.Abstract
{
    public interface IAuthRepository : IGenericRepository<UserCredential, Guid>
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

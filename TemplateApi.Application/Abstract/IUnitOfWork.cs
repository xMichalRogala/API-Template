using Core.Domain.Schemas.Entities;
using TemplateApi.Commons.Data.Repository;

namespace TemplateApi.Application.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User, Guid> userRepository { get; }

        Task<bool> Complete(CancellationToken cancellationToken = default);
    }
}

using TemplateApi.Commons.Data.Repository;
using TemplateApi.Domain.Core.Entities;

namespace TemplateApi.Domain.Core.DAL.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User, Guid> userRepository { get; }

        Task<bool> Complete();
    }
}

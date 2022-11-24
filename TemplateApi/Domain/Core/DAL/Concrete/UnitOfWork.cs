using TemplateApi.Common.Data;
using TemplateApi.Commons.Data.Repository;
using TemplateApi.Domain.Core.DAL.Abstract;
using TemplateApi.Domain.Core.Entities;

namespace TemplateApi.Domain.Core.DAL.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IGenericRepository<User, Guid> _userRepository;
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _userRepository = new GenericRepository<User, Guid>(dbContext);
        }

        public IGenericRepository<User, Guid> userRepository => _userRepository ?? new GenericRepository<User, Guid>(_dbContext);

        public async Task<bool> Complete()
            => await _dbContext.SaveChangesAsync() > 0;

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}

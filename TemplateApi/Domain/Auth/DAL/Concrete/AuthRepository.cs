using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TemplateApi.Domain.Auth.DAL.Abstract;
using TemplateApi.Domain.Auth.Entities;

namespace TemplateApi.Domain.Auth.DAL.Concrete
{
    public sealed class AuthRepository : IAuthRepository
    {
        private readonly AuthDbContext _dbContext;

        public AuthRepository(AuthDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(UserCredential entity, CancellationToken cancellationToken = default) 
            => await _dbContext.AddAsync(entity, cancellationToken);

        public async Task<UserCredential> Find(Expression<Func<UserCredential, bool>> predicate, CancellationToken cancellationToken = default)
            => await _dbContext.UserCredentials.SingleOrDefaultAsync(predicate, cancellationToken);

        public async Task<IEnumerable<UserCredential>> Where(Expression<Func<UserCredential, bool>> predicate, CancellationToken cancellationToken = default)
            => await _dbContext.UserCredentials.Where(predicate).ToListAsync(cancellationToken);

        public async Task<IEnumerable<UserCredential>> GetAll(CancellationToken cancellationToken = default)
            => await _dbContext.UserCredentials.ToListAsync(cancellationToken);

        public async Task<UserCredential> GetById(Guid id, CancellationToken cancellationToken = default)
            => await _dbContext.UserCredentials.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public void Update(UserCredential entity)
            => _dbContext.Update(entity);

        public void Delete(UserCredential entity)
            => _dbContext.Remove(entity);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await _dbContext.SaveChangesAsync(cancellationToken);
    }
}

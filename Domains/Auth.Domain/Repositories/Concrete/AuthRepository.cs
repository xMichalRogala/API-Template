using System.Linq.Expressions;
using Auth.Domain.Repositories.Abstract;
using Auth.Domain.Schemas.Entities;
using Microsoft.EntityFrameworkCore;
using TemplateApi.Persistence.DbContexts.Auth;

namespace Auth.Domain.Repositories.Concrete
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

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

        public async Task Add(UserCredential entity) 
            => await _dbContext.AddAsync(entity);

        public async Task<UserCredential> Find(Expression<Func<UserCredential, bool>> predicate)
            => await _dbContext.UserCredentials.SingleOrDefaultAsync(predicate);

        public async Task<IEnumerable<UserCredential>> Where(Expression<Func<UserCredential, bool>> predicate)
            => await _dbContext.UserCredentials.Where(predicate).ToListAsync();

        public async Task<IEnumerable<UserCredential>> GetAll()
            => await _dbContext.UserCredentials.ToListAsync();

        public async Task<UserCredential> GetById(Guid id)
            => await _dbContext.UserCredentials.FirstOrDefaultAsync(x => x.Id == id);

        public void Update(UserCredential entity)
            => _dbContext.Update(entity);

        public void Delete(UserCredential entity)
            => _dbContext.Remove(entity);

        public async Task<int> SaveChangesAsync()
            => await _dbContext.SaveChangesAsync();
    }
}

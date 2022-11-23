using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TemplateApi.Domain.Auth.DAL.Abstract;
using TemplateApi.Domain.Auth.Entities;

namespace TemplateApi.Domain.Auth.DAL.Concrete
{
    public sealed class AuthRepository : IAuthRepository
    {
        private readonly AuthDbContext dbContext;

        public AuthRepository(AuthDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Add(UserCredential entity) 
            => await dbContext.AddAsync(entity);

        public async Task<UserCredential> Find(Func<UserCredential, bool> predicate)
            => await dbContext.UserCredentials.FindAsync(predicate);

        public async Task<IEnumerable<UserCredential>> Where(Expression<Func<UserCredential, bool>> predicate)
            => await dbContext.UserCredentials.Where(predicate).ToListAsync();

        public async Task<IEnumerable<UserCredential>> GetAll()
            => await dbContext.UserCredentials.ToListAsync();

        public async Task<UserCredential> GetById(Guid id)
            => await dbContext.UserCredentials.FirstOrDefaultAsync(x => x.Id == id);

        public void Update(UserCredential entity)
            => dbContext.Update(entity);

        public void Delete(UserCredential entity)
            => dbContext.Remove(entity);
    }
}

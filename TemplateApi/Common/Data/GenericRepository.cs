using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TemplateApi.Commons.Data.Repository;
using TemplateApi.Commons.Entity.Abstract;

namespace TemplateApi.Common.Data
{
    public class GenericRepository<T, TKey> : IGenericRepository<T, TKey> where T : EntityBase<TKey>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<T> _entities;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _entities = _dbContext.Set<T>();
        }

        public async Task Add(T entity) 
            => await _dbContext.AddAsync(entity);

        public void Delete(T entity)
        {
            _dbContext.Remove(entity);
        }

        public async Task<T> Find(Func<T, bool> predicate) 
            => await _dbContext.FindAsync<T>(predicate);

        public async Task<IEnumerable<T>> GetAll() 
            => await _entities.ToListAsync();

        public async Task<T> GetById(TKey id)
            => await _entities.SingleOrDefaultAsync(x => x.Id.Equals(id));

        public void Update(T entity)
            => _dbContext.Update(entity);

        public async Task<IEnumerable<T>> Where(Expression<Func<T, bool>> predicate)
            => await _entities.Where(predicate).ToListAsync();
    }
}

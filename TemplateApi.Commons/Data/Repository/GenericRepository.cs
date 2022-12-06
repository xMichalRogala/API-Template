using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TemplateApi.Commons.Data.Repository;
using TemplateApi.Commons.Entity.Abstract;

namespace TemplateApi.Common.Data
{
    public class GenericRepository<T, TKey> : IGenericRepository<T, TKey> where T : EntityBase<TKey>
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _entities;

        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext ?? 
                throw new ArgumentNullException(nameof(dbContext));
            _entities = _dbContext.Set<T>();
        }

        public async Task Add(T entity, CancellationToken cancellationToken = default) 
            => await _dbContext.AddAsync(entity, cancellationToken);

        public void Delete(T entity)
        {
            _dbContext.Remove(entity);
        }
        public void Update(T entity) => _dbContext.Update(entity);

        public async Task<T> Find(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken) 
            => await _entities.SingleOrDefaultAsync(predicate, cancellationToken);

        public async Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken = default) 
            => await _entities.ToListAsync(cancellationToken);

        public async Task<T> GetById(TKey id, CancellationToken cancellationToken = default)
            => await _entities.SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);

        public async Task<IEnumerable<T>> Where(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
            => await _entities.Where(predicate).ToListAsync(cancellationToken);
    }
}

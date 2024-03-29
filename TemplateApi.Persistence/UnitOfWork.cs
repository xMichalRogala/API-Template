﻿using Core.Domain.Schemas.Entities;
using TemplateApi.Application.Abstract;
using TemplateApi.Commons.Data.Repository;
using TemplateApi.Persistence.DbContexts.Application;

namespace TemplateApi.Persistence
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

        public async Task<bool> Complete(CancellationToken cancellationToken = default)
            => await _dbContext.SaveChangesAsync(cancellationToken) > 0;

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}

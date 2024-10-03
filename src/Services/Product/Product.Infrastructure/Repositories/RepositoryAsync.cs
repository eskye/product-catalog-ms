using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Product.Application.Interfaces.Repositories;
using Product.Infrastructure.Persistence.Contexts;

namespace Product.Infrastructure.Repositories
{
    public class RepositoryAsync<T> : IRepositoryAsync<T> where T : class
    {
        private readonly ProductDbContext _dbContext;

        public RepositoryAsync(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        } 

        public IQueryable<T> Table => _dbContext.Set<T>();

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            var addedEntity = await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
            return addedEntity.Entity;
        } 
        public Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }
        public async Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<T>().ToListAsync(cancellationToken);
        }

        public async Task<T> SingleAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<T>().SingleAsync(expression, cancellationToken);
        }

        public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<T>().SingleOrDefaultAsync(expression, cancellationToken);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(expression, cancellationToken);
        }

        public Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).CurrentValues.SetValues(entity);
            return Task.CompletedTask;
        }
    }
}


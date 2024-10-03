using System.Linq.Expressions;
namespace Product.Application.Interfaces.Repositories
{
    public interface IRepositoryAsync<T> where T : class
    {
        IQueryable<T> Table { get; }

        Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default);
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> expression,CancellationToken cancellationToken = default);

        Task<T> SingleAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);

        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}


using Product.Application.Interfaces.Repositories;
using Product.Infrastructure.Persistence.Contexts;

namespace Product.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
	{
        private readonly ProductDbContext _dbContext;
        private bool disposed;

        public UnitOfWork(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Commit(CancellationToken cancellationToken)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public Task Rollback()
        {
            _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //dispose managed resources
                    _dbContext.Dispose();
                }
            }
            //dispose unmanaged resources
            disposed = true;
        }
    }
}


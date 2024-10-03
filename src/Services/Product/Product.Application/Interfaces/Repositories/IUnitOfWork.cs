namespace Product.Application.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {  
        Task<int> Commit(CancellationToken cancellationToken);

        Task Rollback();
    }
}


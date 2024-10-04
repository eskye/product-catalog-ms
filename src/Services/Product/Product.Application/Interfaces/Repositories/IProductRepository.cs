using Catalog.Shared.Core;
using Product.Application.QueryFilters;
using ProductEntity = Product.Domain.Entities.Product;

namespace Product.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<(IReadOnlyList<ProductEntity> products, int totalRows)> GetPagedAsync(BasePageFilter pageFilter, ProductQueryFilter filter, CancellationToken cancellationToken = default);
        IRepositoryAsync<ProductEntity> Repo { get; }
    }
}


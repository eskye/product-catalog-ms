using Catalog.Shared.Core;
using Catalog.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Product.Application.Interfaces.Repositories;
using Product.Application.QueryFilters;
using Product.Infrastructure.QueryObjects;
using ProductEntity = Product.Domain.Entities.Product;

namespace Product.Infrastructure.Repositories
{

    public class ProductRepository : IProductRepository
	{
		private readonly IRepositoryAsync<ProductEntity> _productRepository;
        public ProductRepository(IRepositoryAsync<ProductEntity> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<(IReadOnlyList<ProductEntity> products, int totalRows)> GetPagedAsync(BasePageFilter pageFilter, ProductQueryFilter filter, CancellationToken cancellationToken = default)
        {
            //TODO:: Complete sorting implementation, partially implemented.

            var expression = new ProductQueryObject(filter).Expression;
            var query = _productRepository.Table.AsNoTrackingWithIdentityResolution().OrderByWhere(expression);
            var totalRowCount = await query.CountAsync(expression, cancellationToken);
            query = query.Select(p => new ProductEntity
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Unit = p.Unit,
                DateCreated = p.DateCreated
            }).Paginate(pageFilter.Page, pageFilter.PageSize);

            var products = await query.ToListAsync(cancellationToken);
            return (products, totalRowCount);
        } 
        public IRepositoryAsync<ProductEntity> Repo => _productRepository;
    }
}


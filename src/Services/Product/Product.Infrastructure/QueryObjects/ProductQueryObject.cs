using Catalog.Shared.Core;
using Product.Application.QueryFilters;
using ProductEntity = Product.Domain.Entities.Product;

namespace Product.Infrastructure.QueryObjects
{
	public class ProductQueryObject : BaseQueryObject<ProductEntity>
    {
		public ProductQueryObject(ProductQueryFilter filter)
		{
            if (filter == null) return;
              And(t => t.Active);

            if (!string.IsNullOrWhiteSpace(filter.Name))
                And(t => t.Name == filter.Name || t.Name.Contains(filter.Name));

            if (filter.Price.HasValue)
                And(t => t.Price == filter.Price.Value);

            if (filter.DateFrom.HasValue)
                And(t => t.DateCreated.Date >= filter.DateFrom.Value);

            if (filter.DateTo.HasValue)
                And(t => t.DateCreated.Date <= filter.DateTo.Value);
        }
	}
}


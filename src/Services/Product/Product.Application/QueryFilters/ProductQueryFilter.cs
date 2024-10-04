using System;
using Catalog.Shared.Core;

namespace Product.Application.QueryFilters
{
    public class ProductQueryFilter : BaseQueryFilter
	{
		public string Name { get; set; }
		public decimal? Price { get; set; }
        
    }
}


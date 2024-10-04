using Catalog.Shared.AppResponse;
using Product.Application.QueryFilters;
using Product.Application.Requests;
using Product.Application.Responses;

namespace Product.Application.Interfaces.Services
{
    public interface IProductService
	{
		Task<Response<CreateProductResponse>> AddAsync(ProductRequest request, CancellationToken cancellationToken);
		Task<Response<ProductResponse>> UpdateAsync(UpdateProductRequest request, CancellationToken cancellationToken);
		Task<Response<ProductResponse>> GetByIdAsync(int id, CancellationToken cancellationToken);
		Task<Response<PaginatedResponse<ProductResponse>>> GetAllAsync(ProductQueryFilter filter, CancellationToken cancellation);
        Task<Response<string>> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}


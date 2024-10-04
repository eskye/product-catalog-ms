using Catalog.Shared.AppResponse;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Interfaces.Services;
using Product.Application.QueryFilters;
using Product.Application.Requests;
using Product.Application.Responses;

namespace Product.Api.Controllers.v1
{
    public class ProductsController : BaseApiController
	{
		private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Get All Products Paginated
        /// </summary> 
        /// <returns>Status 200 OK</returns> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiErrorResponse))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<PaginatedResponse<ProductResponse>>))]
        public async Task<IActionResult> GetAll([FromQuery]ProductQueryFilter filter, CancellationToken cancellationToken)
        {
            var products = await _productService.GetAllAsync(filter, cancellationToken);
            return ApiResponse(products);
        }
         
        /// <summary>
        /// Add/Edit a Product
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Status 200 OK</returns> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiErrorResponse))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<ProductResponse>))]
        public async Task<IActionResult> Post([FromBody] ProductRequest request, CancellationToken cancellationToken)
        {
            return Created(await _productService.AddAsync(request, cancellationToken));
        }

        /// <summary>
        /// Add/Edit a Product
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Status 200 OK</returns> 
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiErrorResponse))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<ProductResponse>))]
        public async Task<IActionResult> Update([FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
        {
            return ApiResponse(await _productService.UpdateAsync(request, cancellationToken));
        }

        /// <summary>
        /// Get a Product
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK response</returns> 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiErrorResponse))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<ProductResponse>))]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            return ApiResponse(await _productService.GetByIdAsync(id, cancellationToken));
        }

        /// <summary>
        /// Delete a Product
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK response</returns> 
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiErrorResponse))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<string>))]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            return ApiResponse(await _productService.DeleteAsync(id, cancellationToken));
        } 
    }
}


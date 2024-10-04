using System;
using AutoMapper;
using Catalog.Shared.AppResponse;
using Catalog.Shared.Core;
using Microsoft.EntityFrameworkCore;
using Product.Application.Constants;
using Product.Application.Interfaces.Repositories;
using Product.Application.Interfaces.Services;
using Product.Application.QueryFilters;
using Product.Application.Requests;
using Product.Application.Responses;
using ProductEntity = Product.Domain.Entities.Product;

namespace Product.Infrastructure.Services
{
	public class ProductService : IProductService
	{ 
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<CreateProductResponse>> AddAsync(ProductRequest request, CancellationToken cancellationToken)
        {
            var exist = await _productRepository.Repo.Table.AsNoTrackingWithIdentityResolution().AnyAsync(x => x.Name == request.Name);
            if (exist)
            {
                return Response<CreateProductResponse>.Failed(CustomMessages.RecordNotFound);
            } 
            var createdRecord = await _productRepository.Repo.AddAsync(_mapper.Map<ProductEntity>(request), cancellationToken); 
            await _unitOfWork.Commit(cancellationToken); 
            return Response<CreateProductResponse>.Success(new CreateProductResponse(createdRecord.Id));
        }

        public async Task<Response<string>> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var product = await _productRepository.Repo.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (product == null) return Response<string>.Failed(CustomMessages.RecordNotFound);
            await _productRepository.Repo.DeleteAsync(product);
            await _unitOfWork.Commit(cancellationToken);
            return Response<string>.Success("Product deleted successfully.");

        }

        public async Task<Response<PaginatedResponse<ProductResponse>>> GetAllAsync(ProductQueryFilter filter, CancellationToken cancellation)
        {
            var (products, totalRows) = await _productRepository.GetPagedAsync(new BasePageFilter(filter.Size, filter.Page), filter, cancellation);
            var productResposes = _mapper.Map<IReadOnlyList<ProductResponse>>(products);
            return Response<PaginatedResponse<ProductResponse>>.Success(new PaginatedResponse<ProductResponse>(productResposes, new PaginationMetaData(filter.Page, filter.Size, totalRows)));
        }

        public async Task<Response<ProductResponse>> GetByIdAsync(int id, CancellationToken cancellation)
        {
            var product = await _productRepository.Repo.FirstOrDefaultAsync(x => x.Id == id, cancellation);
            if (product == null) return Response<ProductResponse>.Failed(CustomMessages.RecordNotFound);
            return Response<ProductResponse>.Success(_mapper.Map<ProductResponse>(product));
        }

        public async Task<Response<ProductResponse>> UpdateAsync(UpdateProductRequest request, CancellationToken cancellation)
        {
            var product = await _productRepository.Repo.FirstOrDefaultAsync(x => x.Id == request.Id, cancellation);
            if (product == null) return Response<ProductResponse>.Failed(CustomMessages.RecordNotFound);
            _mapper.Map(request, product);
            await _productRepository.Repo.UpdateAsync(product);
            await _unitOfWork.Commit(cancellation);
            return Response<ProductResponse>.Success(_mapper.Map<ProductResponse>(product));
        }
    }
}


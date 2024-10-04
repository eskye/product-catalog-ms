using AutoMapper;
using Product.Application.Requests;
using Product.Application.Responses;
using ProductEntity = Product.Domain.Entities.Product;

namespace Product.Application.Mappings
{
    public class AppMappingProfile : Profile
	{
		public AppMappingProfile()
		{
			CreateMap<UpdateProductRequest, ProductEntity>();
			CreateMap<ProductRequest, ProductEntity>();
			CreateMap<ProductEntity, ProductResponse>();
		}
	}
}


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders; 
using ProductEntity = Product.Domain.Entities.Product;

namespace Product.Infrastructure.Persistence.Configurations
{
	public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
    {  
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(300);
            builder.Property(p => p.Unit).HasMaxLength(20);
        }
    }
}


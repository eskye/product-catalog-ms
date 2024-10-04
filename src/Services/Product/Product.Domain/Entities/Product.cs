using Catalog.Shared.Domain.Common;

namespace Product.Domain.Entities;

public class Product : IBaseEntity<int>, IAuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Unit { get; set; }
    public Guid CreatedBy { get ; set ; }
    public DateTime DateCreated { get ; set ; }
    public Guid? LastModifiedBy { get ; set ; }
    public DateTime? LastModifiedDate { get ; set ; }
    public bool Active { get ; set ; }
}


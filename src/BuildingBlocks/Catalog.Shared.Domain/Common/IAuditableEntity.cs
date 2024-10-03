namespace Catalog.Shared.Domain.Common;

public interface IAuditableEntity : IActiveStatusAudit
{
    Guid CreatedBy { get; set; }
    DateTime DateCreated { get; set; }
    Guid? LastModifiedBy { get; set; }
    DateTime? LastModifiedDate { get; set; }
}

namespace Catalog.Shared.Domain.Common;

public class AuditableEntity : IAuditableEntity, IActiveStatusAudit
{
    public Guid CreatedBy { get; set; }
    public DateTime DateCreated { get; set; }
    public Guid? LastModifiedBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }
    public bool Active { get; set; }
}

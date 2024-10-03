namespace Catalog.Shared.Domain.Common;

public interface IActiveStatusAudit
{
    bool IsActive { get; set; }
}

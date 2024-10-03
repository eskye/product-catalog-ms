using System;

namespace Catalog.Shared.Application
{
    public interface ICurrentUserService
    {
        Guid UserId { get; set; }
    }
}


using System;

namespace Catalog.Shared.Application
{
    public interface ICurrentUser
    {
        Guid UserId { get; set; }
    }
}


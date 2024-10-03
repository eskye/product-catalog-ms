using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Catalog.Shared.Application
{
    public class CurrentUserService : ICurrentUserService
    {  
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _ = Guid.TryParse(httpContextAccessor.HttpContext?.User?.FindFirstValue("uid"), out var userId);
            UserId = userId;
        }

        public Guid UserId { get; set; }
    }
}


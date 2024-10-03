using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Catalog.Shared.Application
{
    public class CurrentUser : ICurrentUser
    {  
        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _ = Guid.TryParse(httpContextAccessor.HttpContext?.User?.FindFirstValue("uid"), out var userId);
            UserId = userId;
        }

        public Guid UserId { get; set; }
    }
}


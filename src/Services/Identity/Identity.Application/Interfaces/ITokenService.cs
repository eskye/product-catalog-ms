using System.Security.Claims;
using Identity.Application.Responses;
using Identity.Domain;

namespace Identity.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessTokenAsync(User user, int tokenExpirationMin, bool isRefreshToken = false, string sessionId = null);
        Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string expiredAccessToken);
        Task<AuthenticateResponse> GetAccessAndRefreshTokenAsync(User user);
    }
}


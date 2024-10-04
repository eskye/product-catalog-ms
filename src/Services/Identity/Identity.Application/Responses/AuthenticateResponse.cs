

namespace Identity.Application.Responses
{
    public record BaseAccessTokenResponse(string AccessToken, int ExpireInSeconds);
    public record AuthenticateResponse(string RefreshToken, bool IsAuthenticated, string AccessToken, int ExpireInSeconds, UserInfoResponse UserInfo) : BaseAccessTokenResponse(AccessToken, ExpireInSeconds);
    public record UserInfoResponse(string FirstName, string LastName, string Email, string PhoneNumber);
}


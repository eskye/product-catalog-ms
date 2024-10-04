using Catalog.Shared.AppResponse;
using Identity.Application.Requests;
using Identity.Application.Responses;

namespace Identity.Application.Interfaces
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Handles the authentication of user.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Returns the access token of authenticated user </returns>
        Task<Response<AuthenticateResponse>> AuthenticateAsync(AuthenticateRequest request);
        /// <summary>
        /// Refresh expired access token.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Returns the access token of authenticated user </returns>
        Task<Response<AuthenticateResponse>> RefreshTokenAsync(RefreshTokenRequest request);
    }
}


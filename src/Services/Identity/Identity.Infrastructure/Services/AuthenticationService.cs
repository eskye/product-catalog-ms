using Catalog.Shared.AppResponse;
using Catalog.Shared.Exceptions;
using Identity.Application.Constants;
using Identity.Application.Interfaces;
using Identity.Application.Requests;
using Identity.Application.Responses;
using Identity.Domain;
using Microsoft.AspNetCore.Identity;

namespace Identity.Infrastructure.Services.Interfaces
{

    public class AuthenticationService : IAuthenticationService
	{
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;

        public AuthenticationService(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<Response<AuthenticateResponse>> AuthenticateAsync(AuthenticateRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email) ?? throw new IdentityErrorException(CustomMessages.Invalid_UserName_Password);
            var passwordVerificationResult = _userManager.PasswordHasher.VerifyHashedPassword(user,
                user.PasswordHash, request.Password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
                throw new IdentityErrorException(CustomMessages.Invalid_UserName_Password);

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
                throw new IdentityErrorException($"Credentials for '{request.Email} aren't valid'.");
            var response = await _tokenService.GetAccessAndRefreshTokenAsync(user);
            return Response<AuthenticateResponse>.Success(response, "Authenticated successfully");
        }

        public async Task<Response<AuthenticateResponse>> RefreshTokenAsync(RefreshTokenRequest request)
        {
            var userPrincipal = await _tokenService.GetPrincipalFromExpiredToken(request.RefreshToken);
            var email = userPrincipal.Identity!.Name;
            var sessionId = userPrincipal.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.RefreshTokenId)?.Value; 
            var user = await _userManager.FindByEmailAsync(email) ?? throw new IdentityErrorException(CustomMessages.RefreshToken_Error);
            if (!string.Equals(user.RefreshToken, sessionId, StringComparison.InvariantCultureIgnoreCase))
                throw new IdentityErrorException(CustomMessages.RefreshToken_Error);
            //Generate the access and refresh token.
            var response = await _tokenService.GetAccessAndRefreshTokenAsync(user);
            return Response<AuthenticateResponse>.Success(response, "Access token refreshed successfully");
        }
    }
}


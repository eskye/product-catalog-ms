using Catalog.Shared.AppResponse;
using Identity.Application.Interfaces;
using Identity.Application.Requests;
using Identity.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers.v1
{
    public class AuthController : BaseApiController
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiErrorResponse))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<AuthenticateResponse>))]
        public async Task<ActionResult<Response<AuthenticateResponse>>> AuthenticateAsync([FromBody] AuthenticateRequest request)
        {
            return Ok(await _authenticationService.AuthenticateAsync(request));
        }

        [HttpPost("refreshToken")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiErrorResponse))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<AuthenticateResponse>))]
        public async Task<ActionResult<Response<AuthenticateResponse>>> RefreshTokenAsync([FromBody] RefreshTokenRequest request)
        {
            return Ok(await _authenticationService.RefreshTokenAsync(request));
        }
    }
}


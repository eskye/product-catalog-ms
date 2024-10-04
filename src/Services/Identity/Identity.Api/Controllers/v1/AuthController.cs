using System;
using Catalog.Shared.AppResponse;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers.v1
{
    public class AuthController : BaseApiController
    {
        public AuthController()
        {
        }

        //[HttpPost("authenticate")]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiErrorResponse))]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<AuthenticateResponse>))]
        //public async Task<ActionResult<Response<AuthenticateResponse>>> AuthenticateAsync([FromBody] AuthenticateRequest request)
        //{
        //    return Ok(await _authenticationService.AuthenticateAsync(request));
        //}

        //[HttpPost("refreshToken")]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiErrorResponse))]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<AuthenticateResponse>))]
        //public async Task<ActionResult<Response<AuthenticateResponse>>> RefreshTokenAsync([FromBody] RefreshTokenRequest request)
        //{
        //    return Ok(await _authenticationService.RefreshTokenAsync(request));
        //}
    }
}


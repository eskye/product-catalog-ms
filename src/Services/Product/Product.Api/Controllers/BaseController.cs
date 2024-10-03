﻿using System;
using Catalog.Shared.AppResponse;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Product.Api.Controllers
{
    [Authorize]
    [ApiKey]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseApiController : ControllerBase
    {

        [NonAction]
        protected CreatedResult Created(object value)
        {
            return base.Created("", value);
        }

        [NonAction]
        protected ActionResult ApiResponse<T>(ApiResponse<T> response) where T : class
        {
            return response.StatusCode switch
            {
                HttpStatusCode.OK => base.Ok(response),
                HttpStatusCode.BadRequest => base.BadRequest(response),
                HttpStatusCode.NotFound => base.NotFound(response),
                _ => throw new InvalidOperationException($"Unsupported Result Status {response.StatusCode}")
            };
        }

        [NonAction]
        protected ActionResult ApiResponse<T>(Response<T> response) where T : class
        {
            return response.ResultType switch
            {
                ResultType.Success => base.Ok(response),
                ResultType.Error => base.BadRequest(response),
                ResultType.Warning => base.BadRequest(response),
                ResultType.ValidationError => base.BadRequest(response),
                _ => base.BadRequest(response)
            };
        }
    }
}


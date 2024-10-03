using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using Catalog.Shared.Exceptions;
using Catalog.Shared.AppResponse;

namespace Catalog.Shared.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException, "Inner Exception");
                ApiErrorResponse responseModel;
                var response = context.Response;
                response.ContentType = "application/json";
                switch (ex)
                { 
                    case ValidationException exception: 
                        response.StatusCode = StatusCodes.Status400BadRequest;
                        responseModel = new ApiErrorResponse(exception.Error, exception.Message);
                        _logger.LogError(exception, exception!.Error);
                        break;
                    case UnauthorizedAccessException exception:
                        response.StatusCode = StatusCodes.Status401Unauthorized;
                        responseModel = new ApiErrorResponse(exception.Message, HttpStatusCode.Unauthorized);
                        break;  
                    default:
                        // unhandled error
                        response.StatusCode = StatusCodes.Status500InternalServerError;
                        responseModel = new ApiErrorResponse(AppCustomResponseMessage.GenericMessage, AppCustomResponseMessage.GenericMessage, HttpStatusCode.InternalServerError);
                        break;
                }
                _logger.LogError(ex, responseModel.Message);
                var result = JsonSerializer.Serialize(responseModel, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                await response.WriteAsync(result);
            }


        }
    }
}


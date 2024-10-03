using System.Net;

namespace Catalog.Shared.AppResponse
{
    public class ApiErrorResponse : ApiResponseBase
    {
        public ApiErrorResponse(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(false,
            message, statusCode)
        {
        }

        public ApiErrorResponse(string error, string message = "", HttpStatusCode statusCode = HttpStatusCode.BadRequest) :
            base(false, message, statusCode)
        {
            Error = error;
        }

        public string Error { get; }
    }
}


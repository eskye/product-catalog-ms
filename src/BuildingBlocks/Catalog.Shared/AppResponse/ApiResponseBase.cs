using System.Net;

namespace Catalog.Shared.AppResponse
{
    public class ApiResponseBase
    {
        protected ApiResponseBase(bool succeeded, string message, HttpStatusCode statusCode)
        {
            Succeeded = succeeded;
            Message = message;
            StatusCode = statusCode;
        }

        public bool Succeeded { get; }
        public string Message { get; }
        public HttpStatusCode StatusCode { get; }
    }
}


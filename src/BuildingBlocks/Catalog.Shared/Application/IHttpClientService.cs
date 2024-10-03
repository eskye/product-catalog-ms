namespace Catalog.Shared.Application
{
    public interface IHttpClientService
    {
        Task<HttpResponseMessage> MakeHttpRequest(object request, string baseAddress, string requestUri, HttpMethod method,
       bool isFormData, Dictionary<string, string>? headers = null, string? formContentType = null);

        Task<HttpResponseMessage> MakeHttpRequest(string baseAddress, string requestUri, HttpMethod method,List<KeyValuePair<string, string>>? parameters = null, Dictionary<string, string>? headers = null);

        Task<HttpResponseMessage> MakeHttpRequest(string baseAddress, HttpRequestMessage httpRequestMessage);
    }
}


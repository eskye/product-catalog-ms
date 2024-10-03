using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Text;
using Catalog.Shared.Extensions;

namespace Catalog.Shared.Application
{
    public class HttpClientService : IHttpClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpClient _httpClient;

        public HttpClientService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> MakeHttpRequest(object request, string baseAddress, string requestUri, HttpMethod method, bool isFormData, Dictionary<string, string>? headers = null, string? formContentType = null)
        {
            _httpClient = _httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(baseAddress);
            HttpResponseMessage response = null;

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")
            { CharSet = "utf-8" });

            if (headers != null)
                foreach (var header in headers)
                    _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            if (isFormData)
            {
                var formFile = (IFormFile)request;
                byte[] fileBytes;
                using (var ms = new MemoryStream())
                {
                    await formFile.CopyToAsync(ms);
                    fileBytes = ms.ToArray();
                }

                var byteArrayContent = new ByteArrayContent(fileBytes);
                byteArrayContent.Headers.Add("Content-Type", $"{formContentType}");

                response = await _httpClient.PostAsync(requestUri,
                    new MultipartFormDataContent { { byteArrayContent, formFile.Name, formFile.FileName } });
            }
            else
            {
                var data = request.Serialize();
                HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");

                if (method == HttpMethod.Post)
                {
                    response = await _httpClient.PostAsync(requestUri, content);
                }
                else if (method == HttpMethod.Put)
                {
                    response = await _httpClient.PutAsync(requestUri, content);
                }
                else if (method == HttpMethod.Get)
                {
                    var requestBody = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri($"{baseAddress}{requestUri}"),
                        Content = content
                    };

                    response = await _httpClient.SendAsync(requestBody);
                }
            }
            return response;
        }

        public async Task<HttpResponseMessage> MakeHttpRequest(string baseAddress, string requestUri, HttpMethod method, List<KeyValuePair<string, string>>? parameters = null, Dictionary<string, string>? headers = null)
        {
            _httpClient = _httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(baseAddress);

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")
            { CharSet = "utf-8" });

            if (headers != null)
                foreach (var header in headers)
                    _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);

            var body = parameters != null && parameters.Any() ? new FormUrlEncodedContent(parameters) : null;
            return await _httpClient.PostAsync(requestUri, body);
        }

        public async Task<HttpResponseMessage> MakeHttpRequest(string baseAddress, HttpRequestMessage httpRequestMessage)
        {
            _httpClient = _httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(baseAddress);

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")
            { CharSet = "utf-8" });
            return await _httpClient.SendAsync(httpRequestMessage);
        }
    }
}


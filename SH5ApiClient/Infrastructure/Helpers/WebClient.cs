using SH5ApiClient.Core.Requests;
using SH5ApiClient.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SH5ApiClient.Infrastructure.Helpers
{
    public class WebClient : IWebClient
    {
        private readonly HttpClient _httpClient;

        public WebClient(ConnectionParamSH5 connectionParam)
        {
            if (connectionParam is null)
                throw new ArgumentNullException(nameof(connectionParam));
            _httpClient = CreateHttpClient(connectionParam);
        }

        private static HttpClient CreateHttpClient(ConnectionParamSH5 connectionParam)
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            return new HttpClient(handler)
            {
                BaseAddress = new Uri($"http://{connectionParam.Address}:{connectionParam.Port}/")
            };
        }

        public Task<string> WebGetAsync(string url, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException($"\"{nameof(url)}\" не может быть пустым или содержать только пробел.", nameof(url));
            return WebGetInternalAsync(url, cancellationToken);
        }
        private async Task<string> WebGetInternalAsync(string url, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url, cancellationToken);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
        public Task<string> WebPostAsync(string request, CancellationToken cancellationToken)
        {
            return WebPostInternalAsync("api/sh5exec", request, cancellationToken);
        }
        public Task<string> WebPostAsync(RequestBase request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            string jsonRequest = request.CreateJsonRequest();
            return WebPostInternalAsync($"api/{request.Operation.Uri}", jsonRequest, cancellationToken);
        }
        private async Task<string> WebPostInternalAsync(string url, string request, CancellationToken cancellationToken)
        {
            try
            {
                HttpContent content = new StringContent(request, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(url, content, cancellationToken);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            catch (Exception ex)
            {
#if NETFRAMEWORK
                throw ex?.InnerException?.InnerException ?? ex;
#else
                throw;
#endif
            }
        }
    }
}

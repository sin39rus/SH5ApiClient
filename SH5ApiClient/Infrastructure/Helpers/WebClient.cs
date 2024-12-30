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
        public Task<string> WebGetAsync(string url, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException($"\"{nameof(url)}\" не может быть пустым или содержать только пробел.", nameof(url));
            return WebGetInternalAsync(url, cancellationToken);
        }
        private static async Task<string> WebGetInternalAsync(string url, CancellationToken cancellationToken)
        {
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(3);
            HttpResponseMessage response = await client.GetAsync(url, cancellationToken);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
        public Task<string> WebPostAsync(string request, ConnectionParamSH5 connectionParam, CancellationToken cancellationToken)
        {

            string url = $"http://{connectionParam.Address}:{connectionParam.Port}/api/sh5exec";
            return WebPostInternalAsync(url, request, cancellationToken);
        }
        public Task<string> WebPostAsync(RequestBase request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            string url = $"http://{request.ConnectionParam.Address}:{request.ConnectionParam.Port}/api/{request.Operation.Uri}";
            string jsonRequest = request.CreateJsonRequest();
            return WebPostInternalAsync(url, jsonRequest, cancellationToken);
        }
        private static async Task<string> WebPostInternalAsync(string url, string request, CancellationToken cancellationToken)
        {
            try
            {
                HttpClient client = new HttpClient(new HttpClientHandler()
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                });
                HttpContent content = new StringContent(request, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content, cancellationToken);
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

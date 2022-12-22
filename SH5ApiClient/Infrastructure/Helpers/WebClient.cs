using System.Net;
using System.Text;

namespace SH5ApiClient.Infrastructure.Helpers
{
    public class WebClient : IWebClient
    {
        public Task<string> WebGetAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException($"\"{nameof(url)}\" не может быть пустым или содержать только пробел.", nameof(url));
            return WebGetInternalAsync(url);
        }
        private static async Task<string> WebGetInternalAsync(string url)
        {
            HttpClient client = new();
            client.Timeout = TimeSpan.FromSeconds(3);
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            catch (TaskCanceledException)
            {
                throw new TaskCanceledException($"Запрос был отменен, так как истекло время ожидания {client.Timeout.Seconds} секунды.");
            }
        }
        public Task<string> WebPostAsync(string request, ConnectionParamSH5 connectionParam)
        {

            string url = $"http://{connectionParam.Address}:{connectionParam.Port}/api/sh5exec";
            return WebPostInternalAsync(url, request);
        }
        public Task<string> WebPostAsync(RequestBase request)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            string url = $"http://{request.ConnectionParam.Address}:{request.ConnectionParam.Port}/api/{request.Operation.Uri}";
            string jsonRequest = request.CreateJsonRequest();
            return WebPostInternalAsync(url, jsonRequest);
        }
        private static async Task<string> WebPostInternalAsync(string url, string request)
        {
            HttpClient client = new(new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            });
            HttpContent content = new StringContent(request, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
    }
}

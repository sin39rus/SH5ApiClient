
using SH5ApiClient.Core.Requests;
using SH5ApiClient.Models;
using System.Threading.Tasks;

namespace SH5ApiClient.Infrastructure.Helpers
{
    public interface IWebClient
    {
        Task<string> WebGetAsync(string url);
        Task<string> WebPostAsync(RequestBase request);
        Task<string> WebPostAsync(string request, ConnectionParamSH5 connectionParam);
    }
}
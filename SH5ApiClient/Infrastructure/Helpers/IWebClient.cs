
using SH5ApiClient.Core.Requests;
using System.Threading;
using System.Threading.Tasks;

namespace SH5ApiClient.Infrastructure.Helpers
{
    public interface IWebClient
    {
        Task<string> WebGetAsync(string url, CancellationToken cancellationToken);
        Task<string> WebPostAsync(RequestBase request, CancellationToken cancellationToken);
        Task<string> WebPostAsync(string request, CancellationToken cancellationToken);
    }
}

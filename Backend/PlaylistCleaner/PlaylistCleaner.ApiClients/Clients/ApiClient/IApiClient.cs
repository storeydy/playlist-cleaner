using Newtonsoft.Json.Linq;

namespace PlaylistCleaner.ApiClients.Clients.ApiClient;

internal interface IApiClient
{
    Task<T> SendRequestAsync<T>(HttpMethod method, string requestUri, string jwt, CancellationToken cancellationToken = default);
}

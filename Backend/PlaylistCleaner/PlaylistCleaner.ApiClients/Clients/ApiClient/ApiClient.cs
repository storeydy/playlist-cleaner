using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace PlaylistCleaner.ApiClients.Clients.ApiClient;

internal class ApiClient : IApiClient
{
    private readonly HttpClient _httpClient;
    public ApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://api.spotify.com/v1/");

    }

    public async Task<T> SendRequestAsync<T>(HttpMethod method, string requestUri, string jwt, CancellationToken cancellationToken = default)
    { 
        var requestMessage = new HttpRequestMessage(method, _httpClient.BaseAddress + requestUri);
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

        var response = await _httpClient.SendAsync(requestMessage, cancellationToken);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        return JsonConvert.DeserializeObject<T>(content);
    }
}

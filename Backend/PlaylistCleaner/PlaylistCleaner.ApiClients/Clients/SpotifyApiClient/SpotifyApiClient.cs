using AutoMapper;
using Newtonsoft.Json;
using PlaylistCleaner.ApiClients.Responses.SpotifyApiClientResults.GetUserProfile;
using System.Net.Http.Headers;

namespace PlaylistCleaner.ApiClients.Clients.SpotifyApiClient;

internal sealed class SpotifyApiClient : ISpotifyApiClient
{
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;

    public SpotifyApiClient(HttpClient httpClient, IMapper mapper)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://api.spotify.com/v1/");
        _mapper = mapper;
    }

    public async Task<GetCurrentUsersProfileResult> GetCurrentUsersProfileAsync(string jwt, CancellationToken cancellationToken = default)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"/v1/me");
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

        HttpResponseMessage response = await _httpClient.SendAsync(requestMessage, cancellationToken);
        HttpContent content = response.Content;

        var json = await content.ReadAsStringAsync(cancellationToken);
        
        GetCurrentUsersProfileResult result = JsonConvert.DeserializeObject<GetCurrentUsersProfileResult>(json);

        return result;
    }

    public async Task<GetUserProfileResult> GetUserProfileAsync(string userId, string jwt, CancellationToken cancellationToken = default)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"users/{userId}");
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

        var response = await _httpClient.SendAsync(requestMessage, cancellationToken);

        return _mapper.Map<GetUserProfileResult>(response);
    }
}

using AutoMapper;
using PlaylistCleaner.ApiClients.Responses.SpotifyApiClientResponses.GetUserProfile;
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

    public async Task<GetUserProfileResponse> GetUserProfileAsync(string userId, string jwt, CancellationToken cancellationToken = default)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"users/{userId}");
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

        var response = await _httpClient.SendAsync(requestMessage, cancellationToken);

        return _mapper.Map<GetUserProfileResponse>(response);
    }
}

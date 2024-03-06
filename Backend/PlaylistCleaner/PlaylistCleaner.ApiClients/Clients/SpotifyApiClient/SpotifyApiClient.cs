using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlaylistCleaner.ApiClients.Responses.SpotifyApiClientResults.GetCurrentUsersProfile;
using PlaylistCleaner.ApiClients.Responses.SpotifyApiClientResults.GetUserProfile;
using PlaylistCleaner.ApiClients.Responses.SpotifyApiClientResults.GetUsersPlaylists;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;

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

    public async Task<GetUsersPlaylistsResult> GetUserPlaylistsAsync(string userId, string jwt, CancellationToken cancellationToken = default)
    {
        dynamic jsonParsed = await GetPageOfPlaylistDataAsJsonString(userId, jwt, 0, cancellationToken);
        int pageNumber = 1;
        while (jsonParsed.next != null)
        {
            dynamic nextPage = await GetPageOfPlaylistDataAsJsonString(userId, jwt, 20 * pageNumber, cancellationToken);
            jsonParsed.items.Merge(nextPage.items);
            jsonParsed.next = nextPage.next;
            pageNumber++;
        }

        GetUsersPlaylistsResult result = jsonParsed.ToObject<GetUsersPlaylistsResult>();

        return result;
    }

    private async Task<JObject> GetPageOfPlaylistDataAsJsonString(string userId, string jwt, int playlistOffset, CancellationToken cancellationToken = default)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"/v1/users/{userId}/playlists?offset={playlistOffset}");
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

        HttpResponseMessage response = await _httpClient.SendAsync(requestMessage, cancellationToken);
        HttpContent content = response.Content;
        string json = await content.ReadAsStringAsync(cancellationToken);

        return JObject.Parse(json);
    }
}

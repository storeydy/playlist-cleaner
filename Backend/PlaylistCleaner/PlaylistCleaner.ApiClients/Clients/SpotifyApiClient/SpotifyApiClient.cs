using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlaylistCleaner.ApiClients.Responses.SpotifyApiClientResults.GetPlaylist;
using PlaylistCleaner.ApiClients.Responses.SpotifyApiClientResults.GetCurrentUsersProfile;
using PlaylistCleaner.ApiClients.Responses.SpotifyApiClientResults.GetUserProfile;
using PlaylistCleaner.ApiClients.Responses.SpotifyApiClientResults.GetUsersPlaylists;
using System.Net.Http.Headers;
using PlaylistCleaner.ApiClients.Responses.SpotifyApiClientResults.GetPlaylistItems;

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
        int pageNumber = 1;
        dynamic jsonParsed = await GetPageOfPlaylistsDataAsJsonString(userId, 0, jwt, cancellationToken);
        while (jsonParsed.next != null)
        {
            dynamic nextPage = await GetPageOfPlaylistsDataAsJsonString(userId, 20 * pageNumber, jwt, cancellationToken);
            jsonParsed.items.Merge(nextPage.items);
            jsonParsed.next = nextPage.next;
            pageNumber++;
        }

        GetUsersPlaylistsResult result = jsonParsed.ToObject<GetUsersPlaylistsResult>();

        return result;
    }

    private async Task<JObject> GetPageOfPlaylistsDataAsJsonString(string userId, int playlistOffset, string jwt, CancellationToken cancellationToken = default)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"/v1/users/{userId}/playlists?offset={playlistOffset}");
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

        HttpResponseMessage response = await _httpClient.SendAsync(requestMessage, cancellationToken);
        HttpContent content = response.Content;
        string json = await content.ReadAsStringAsync(cancellationToken);

        return JObject.Parse(json);
    }

    public async Task<GetPlaylistResult> GetPlaylistAsync(string userId, string playlistId, string jwt, CancellationToken cancellationToken = default)
    {
        int pageNumber = 1;
        dynamic jsonPlaylistMetadataParsed = await GetPlaylistMetadataAsJsonStringAsync(playlistId, jwt, cancellationToken);

        dynamic jsonPlaylistTracksParsed = await GetPageOfPlaylistItemsAsJsonString(playlistId, 0, jwt, cancellationToken);
        while (jsonPlaylistTracksParsed.next != null)
        {
            dynamic nextPage = await GetPageOfPlaylistItemsAsJsonString(playlistId, pageNumber * 100, jwt, cancellationToken);
            jsonPlaylistTracksParsed.items.Merge(nextPage.items);
            jsonPlaylistTracksParsed.next = nextPage.next;
            pageNumber++;
        }

        jsonPlaylistMetadataParsed.tracks = jsonPlaylistTracksParsed.items;

        GetPlaylistResult result = jsonPlaylistMetadataParsed.ToObject<GetPlaylistResult>();

        return result;
    }

    public async Task<GetPlaylistItemsResult> GetPlaylistItemsAsync(string playlistId, string jwt, CancellationToken cancellationToken = default)
    {
        int pageNumber = 1;
        dynamic jsonParsed = await GetPageOfPlaylistItemsAsJsonString(playlistId, 0, jwt, cancellationToken);

        while (jsonParsed.next != null)
        {
            dynamic nextPage = await GetPageOfPlaylistItemsAsJsonString(playlistId, 20 * pageNumber, jwt, cancellationToken);
            jsonParsed.items.Merge(nextPage.items);
            jsonParsed.next = nextPage.next;
            pageNumber++;
        }

        GetPlaylistItemsResult result = jsonParsed.ToObject<GetPlaylistItemsResult>();

        return result;
    }

    private async Task<JObject> GetPlaylistMetadataAsJsonStringAsync(string playlistId, string jwt, CancellationToken cancellationToken = default)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"/v1/playlists/{playlistId}");
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

        HttpResponseMessage response = await _httpClient.SendAsync(requestMessage, cancellationToken);
        HttpContent content = response.Content;
        string json = await content.ReadAsStringAsync(cancellationToken);

        return JObject.Parse(json);
    }

    private async Task<JObject> GetPageOfPlaylistItemsAsJsonString(string playlistId, int playlistItemsOffset, string jwt, CancellationToken cancellationToken = default)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"/v1/playlists/{playlistId}/tracks?offset={playlistItemsOffset}");
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

        HttpResponseMessage response = await _httpClient.SendAsync(requestMessage, cancellationToken);
        HttpContent content = response.Content;
        string json = await content.ReadAsStringAsync(cancellationToken);

        return JObject.Parse(json);
    }
}

using Newtonsoft.Json.Linq;
using PlaylistCleaner.ApiClients.Responses.SpotifyApiClientResults.GetPlaylist;
using PlaylistCleaner.ApiClients.Responses.SpotifyApiClientResults.GetCurrentUsersProfile;
using PlaylistCleaner.ApiClients.Responses.SpotifyApiClientResults.GetUserProfile;
using PlaylistCleaner.ApiClients.Responses.SpotifyApiClientResults.GetUsersPlaylists;
using PlaylistCleaner.ApiClients.Extensions;
using PlaylistCleaner.ApiClients.Clients.ApiClient;

namespace PlaylistCleaner.ApiClients.Clients.SpotifyApiClient;

internal sealed class SpotifyApiClient : ISpotifyApiClient
{
    private readonly IApiClient _apiClient;
    public SpotifyApiClient(IApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<GetCurrentUsersProfileResult> GetCurrentUsersProfileAsync(string jwt, CancellationToken cancellationToken = default)
    {
        string requestUri = "me";
        var result = await _apiClient.SendRequestAsync<GetCurrentUsersProfileResult>(HttpMethod.Get, requestUri, jwt, cancellationToken);
        return result;
    }

    public async Task<GetUserProfileResult> GetUserProfileAsync(string userId, string jwt, CancellationToken cancellationToken = default)
    {
        string requestUri = $"users/{userId}";
        var result = await _apiClient.SendRequestAsync<GetUserProfileResult>(HttpMethod.Get, requestUri, jwt, cancellationToken);
        return result;
    }

    public async Task<GetUsersPlaylistsResult> GetUserPlaylistsAsync(string userId, string jwt, CancellationToken cancellationToken = default)
    {
        int offsetValue = 20;
        var requestUri = $"users/{userId}/playlists";
        var jsonParsed = await _apiClient.SendRequestAsync<JObject>(HttpMethod.Get, requestUri, jwt, cancellationToken); 
        while (!jsonParsed["next"].IsNullOrEmpty())
        {
            string offsetParameter = $"?offset={offsetValue}";
            var nextPageOfPlaylists = await _apiClient.SendRequestAsync<JObject>(HttpMethod.Get, requestUri + offsetParameter, jwt, cancellationToken);
            jsonParsed["items"] = new JArray(jsonParsed["items"].Concat(nextPageOfPlaylists["items"]));
            jsonParsed["next"] = nextPageOfPlaylists["next"];
            offsetValue += 20;
        }

        GetUsersPlaylistsResult result = jsonParsed.ToObject<GetUsersPlaylistsResult>();

        return result;
    }

    public async Task<GetPlaylistResult> GetPlaylistAsync(string playlistId, string jwt, CancellationToken cancellationToken = default, int trackLimit = 50)
    {
        var playlistRequestUri = $"playlists/{playlistId}";
       
        var playlistFull = await _apiClient.SendRequestAsync<JObject>(HttpMethod.Get, playlistRequestUri, jwt, cancellationToken);

        var playlistTracks = await GetPlaylistTracks(playlistId, jwt, trackLimit, cancellationToken);

        playlistFull["tracks"] = playlistTracks["items"];

        GetPlaylistResult result = playlistFull.ToObject<GetPlaylistResult>();

        return result;
    }

    private async Task<JObject> GetPlaylistTracks(string playlistId, string jwt, int trackLimit, CancellationToken cancellationToken = default)
    {
        int offsetValue = 50;
        var playlistTracksRequestUri = $"playlists/{playlistId}/tracks?limit=50";
        var jsonPlaylistTracks = await _apiClient.SendRequestAsync<JObject>(HttpMethod.Get, playlistTracksRequestUri, jwt, cancellationToken);
        while (jsonPlaylistTracks["items"].Count() < trackLimit || !jsonPlaylistTracks["next"].IsNullOrEmpty())
        {
            string offsetParameter = $"&offset={offsetValue}";
            var nextPageOfTracks = await _apiClient.SendRequestAsync<JObject>(HttpMethod.Get, playlistTracksRequestUri + offsetParameter, jwt, cancellationToken);
            jsonPlaylistTracks["items"] = new JArray(jsonPlaylistTracks["items"].Concat(nextPageOfTracks["items"]));
            jsonPlaylistTracks["next"] = nextPageOfTracks["next"];
            offsetValue += 50;
        }

        return jsonPlaylistTracks;
    }
}

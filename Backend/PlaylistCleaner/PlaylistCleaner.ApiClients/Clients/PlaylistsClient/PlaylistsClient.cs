using Newtonsoft.Json.Linq;
using PlaylistCleaner.ApiClients.Responses.SpotifyApiClientResults.GetPlaylist;
using System.Net.Http.Json;

namespace PlaylistCleaner.ApiClients.Clients.PlaylistClient;

internal sealed class PlaylistsClient : IPlaylistsClient
{
    private readonly HttpClient _httpClient;

    public PlaylistsClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<GetPlaylistResult> GetPlaylistAsync(string playlistId, CancellationToken cancellationToken = default)
    {
        var playlistRequestUri = $"{playlistId}";

        var playlist = await _httpClient.GetFromJsonAsync<GetPlaylistResult>(playlistRequestUri, cancellationToken);

        return playlist;
    }
    
    public async Task<JObject> GetPlaylistTracks(string playlistId, int trackLimit, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}

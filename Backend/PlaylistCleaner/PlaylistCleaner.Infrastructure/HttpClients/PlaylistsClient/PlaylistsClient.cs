using PlaylistCleaner.Infrastructure.Results.PlaylistsClientResults;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace PlaylistCleaner.Infrastructure.Clients.PlaylistClient;

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
    
    public async Task<GetPlaylistTracksResult> GetPlaylistTracksAsync(string playlistId, CancellationToken cancellationToken = default)
    {
        int offsetValue = 50;
        var playlistTracksRequestUri = $"{playlistId}/tracks?limit=50";
        var jsonPlaylistTracks = await _httpClient.GetFromJsonAsync<GetPlaylistTracksResult>(playlistTracksRequestUri, cancellationToken);
        bool isAnotherPageOfResults = jsonPlaylistTracks.next == null ? false : true;

        while (isAnotherPageOfResults)
        {
            string offsetParameter = $"&offset={offsetValue}";
            var nextPageOfTracks = await _httpClient.GetFromJsonAsync<GetPlaylistTracksResult>(playlistTracksRequestUri + offsetParameter, cancellationToken);
            jsonPlaylistTracks.items.AddRange(nextPageOfTracks.items);
            if (nextPageOfTracks.next == null)
            {
                isAnotherPageOfResults = false;
            }
            offsetValue += 50;
        }

        return jsonPlaylistTracks;
    }

    public async Task DeleteTrackFromPlaylistAsync(string playlistId, string trackId, CancellationToken cancellationToken = default)
    {
        var playlistTrackDeletionCommandUri = $"{playlistId}/tracks";
        var jsonTracks = new JsonArray();
        var trackObject = new JsonObject{ ["uri"] = $"spotify:track:{trackId}"};
        jsonTracks.Add(trackObject);

        var myObject = new JsonObject{ ["tracks"] = jsonTracks };
        HttpRequestMessage request = new HttpRequestMessage
        {
            Content = JsonContent.Create(myObject),
            Method = HttpMethod.Delete,
            RequestUri = new Uri(playlistTrackDeletionCommandUri, UriKind.Relative)
        };

        var response = await _httpClient.SendAsync(request, cancellationToken);
    }
}

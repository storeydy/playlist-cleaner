using PlaylistCleaner.ApiClients.Responses.PlaylistsClientResults.GetPlaylist;
using PlaylistCleaner.ApiClients.Responses.PlaylistsClientResults.GetPlaylistTracks;
using PlaylistCleaner.ApiClients.Results.PlaylistsClientResults.GetPlaylistDuplicates;
using PlaylistCleaner.ApiClients.Services.DuplicateDetectorService;
using System.Net.Http.Json;

namespace PlaylistCleaner.ApiClients.Clients.PlaylistClient;

internal sealed class PlaylistsClient : IPlaylistsClient
{
    private readonly HttpClient _httpClient;
    private readonly IDuplicateDetectorService _duplicateDetectorService;

    public PlaylistsClient(HttpClient httpClient, IDuplicateDetectorService duplicateDetectorService)
    {
        _httpClient = httpClient;
        _duplicateDetectorService = duplicateDetectorService;
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

    public async Task<GetPlaylistDuplicatesResult> GetPlaylistDuplicatesAsync(string playlistId, CancellationToken cancellationToken = default)
    {
        var tracks = await GetPlaylistTracksAsync(playlistId, cancellationToken);

        var duplicates = await _duplicateDetectorService.GetDuplicatesFromPlaylistTracksAsync(tracks.items, cancellationToken);

        return duplicates;
    }
}

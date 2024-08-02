using PlaylistCleaner.Infrastructure.Exceptions;
using PlaylistCleaner.Infrastructure.Results.PlaylistsClientResults;
using PlaylistCleaner.Infrastructure.Utils.SpotifyHttpClientUtils;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace PlaylistCleaner.Infrastructure.Clients.PlaylistClient;

internal sealed class PlaylistsClient : IPlaylistsClient
{
    private readonly HttpClient _httpClient;
    private readonly SpotifyHttpClientUtils _spotifyHttpClientUtils;

    public PlaylistsClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _spotifyHttpClientUtils = new SpotifyHttpClientUtils(httpClient);
    }

    public async Task<GetPlaylistResult> GetPlaylistAsync(string playlistId, CancellationToken cancellationToken = default)
    {
        var playlistRequestUri = $"{playlistId}";

        try
        {
            var response = await _httpClient.GetAsync(playlistRequestUri, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                throw new SpotifyApiHttpException(response.StatusCode + content);
            }

            var playlist = await response.Content.ReadFromJsonAsync<GetPlaylistResult>(cancellationToken: cancellationToken);
            if (playlist == null)
            {
                throw new EntityNotFoundException(HttpStatusCode.NoContent + ", Playlist not found or response body is empty.");
            }

            return playlist;
        }
        catch (HttpRequestException ex)
        {
            throw new SpotifyApiHttpException(HttpStatusCode.ServiceUnavailable + ", An error occurred while sending the request. Exception " + ex );
        }
    }
    
    public async Task<GetPlaylistTracksResult> GetPlaylistTracksAsync(string playlistId, CancellationToken cancellationToken = default)
    {
        var items = await _spotifyHttpClientUtils.FetchAllItemsAsync<GetPlaylistTracksResult, GetPlaylistTracksResultPlaylistTrack>(
            $"{playlistId}/tracks?limit=50",
            response => response.items,
            response => response.next,
            cancellationToken
        );

        return new GetPlaylistTracksResult(null, items);
    }

    public async Task DeleteTrackFromPlaylistAsync(string playlistId, string trackId, int trackIndex, CancellationToken cancellationToken = default)
    {
        var playlistTrackDeletionCommandUri = $"{playlistId}/tracks";
        var jsonTracks = new JsonArray();
        var trackObject = new JsonObject{ ["uri"] = $"spotify:track:{trackId}", ["positions"] = new JsonArray { trackIndex } };
        jsonTracks.Add(trackObject);

        var myObject = new JsonObject{ ["tracks"] = jsonTracks };
        HttpRequestMessage request = new HttpRequestMessage
        {
            Content = JsonContent.Create(myObject),
            Method = HttpMethod.Delete,
            RequestUri = new Uri(playlistTrackDeletionCommandUri, UriKind.Relative)
        };

        var response = await _httpClient.SendAsync(request, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new SpotifyApiHttpException(response.StatusCode + content);
        }
    }

    public async Task AddTrackToPlaylistAsync(string playlistId, string trackId, int trackIndex, CancellationToken cancellationToken = default)
    {
        var playlistTracksAdditionCommandUri = $"{playlistId}/tracks";
        var trackObject = new JsonObject { ["uris"] = new JsonArray() { $"spotify:track:{trackId}" }, ["position"] = trackIndex };
        var response = await _httpClient.PostAsync(playlistTracksAdditionCommandUri, JsonContent.Create(trackObject), cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new SpotifyApiHttpException(response.StatusCode + content);
        }
    }
}

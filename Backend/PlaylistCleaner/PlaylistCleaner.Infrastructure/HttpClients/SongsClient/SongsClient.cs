using PlaylistCleaner.Infrastructure.Exceptions;
using PlaylistCleaner.Infrastructure.Results.PlaylistsClientResults;
using PlaylistCleaner.Infrastructure.Results.SongsClientResults;
using System.Net;
using System.Net.Http.Json;

namespace PlaylistCleaner.Infrastructure.HttpClients.SongsClient;

internal sealed class SongsClient : ISongsClient
{
    private readonly HttpClient _httpClient;

    public SongsClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<GetSongResult> GetSongAsync(string songId, CancellationToken cancellationToken = default)
    {
        var songRequestUri = $"{songId}";

        try
        {
            var response = await _httpClient.GetAsync(songRequestUri, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                throw new SpotifyApiHttpException(response.StatusCode + content);
            }

            var song = await response.Content.ReadFromJsonAsync<GetSongResult>(cancellationToken: cancellationToken);
            if (song == null)
            {
                throw new EntityNotFoundException(HttpStatusCode.NoContent + ", Song not found or response body is empty.");
            }

            return song;
        }

        catch (HttpRequestException ex)
        {
            throw new SpotifyApiHttpException(HttpStatusCode.ServiceUnavailable + ", An error occurred while sending the request. Exception " + ex);
        }

    }
}

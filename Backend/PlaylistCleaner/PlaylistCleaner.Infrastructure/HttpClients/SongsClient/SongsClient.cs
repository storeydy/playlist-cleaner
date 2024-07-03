using PlaylistCleaner.Infrastructure.Results.SongsClientResults;
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

        var song = await _httpClient.GetFromJsonAsync<GetSongResult>(songRequestUri, cancellationToken);

        return song;
    }
}

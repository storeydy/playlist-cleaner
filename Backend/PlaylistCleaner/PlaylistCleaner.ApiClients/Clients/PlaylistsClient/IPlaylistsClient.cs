using Newtonsoft.Json.Linq;
using PlaylistCleaner.ApiClients.Responses.SpotifyApiClientResults.GetPlaylist;

namespace PlaylistCleaner.ApiClients.Clients.PlaylistClient;

public interface IPlaylistsClient
{
    Task<GetPlaylistResult> GetPlaylistAsync(string playlistId, CancellationToken cancellationToken = default);

    // TODO: add type
    Task<JObject> GetPlaylistTracks(string playlistId, int trackLimit, CancellationToken cancellationToken = default);
}
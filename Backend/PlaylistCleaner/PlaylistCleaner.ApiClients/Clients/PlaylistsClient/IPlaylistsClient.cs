using Newtonsoft.Json.Linq;
using PlaylistCleaner.ApiClients.Responses.PlaylistsClientResponses.GetPlaylist;
using PlaylistCleaner.ApiClients.Responses.PlaylistsClientResponses.GetPlaylistTracks;

namespace PlaylistCleaner.ApiClients.Clients.PlaylistClient;

public interface IPlaylistsClient
{
    Task<GetPlaylistResult> GetPlaylistAsync(string playlistId, CancellationToken cancellationToken = default);

    Task<GetPlaylistTracksResult> GetPlaylistTracksAsync(string playlistId, CancellationToken cancellationToken = default);
}
using PlaylistCleaner.ApiClients.Responses.PlaylistsClientResults.GetPlaylist;
using PlaylistCleaner.ApiClients.Responses.PlaylistsClientResults.GetPlaylistTracks;
using PlaylistCleaner.ApiClients.Results.PlaylistsClientResults.GetPlaylistDuplicates;

namespace PlaylistCleaner.ApiClients.Clients.PlaylistClient;

public interface IPlaylistsClient
{
    Task<GetPlaylistResult> GetPlaylistAsync(string playlistId, CancellationToken cancellationToken = default);

    Task<GetPlaylistTracksResult> GetPlaylistTracksAsync(string playlistId, CancellationToken cancellationToken = default);

    Task<GetPlaylistDuplicatesResult> GetPlaylistDuplicatesAsync(string playlistId, CancellationToken cancellationToken = default);
}
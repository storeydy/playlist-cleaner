using PlaylistCleaner.Infrastructure.Results.PlaylistsClientResults;

namespace PlaylistCleaner.Infrastructure.Clients.PlaylistClient;

public interface IPlaylistsClient
{
    Task<GetPlaylistResult> GetPlaylistAsync(string playlistId, CancellationToken cancellationToken = default);

    Task<GetPlaylistTracksResult> GetPlaylistTracksAsync(string playlistId, CancellationToken cancellationToken = default);
}
using PlaylistCleaner.Infrastructure.Results.PlaylistsClientResults;

namespace PlaylistCleaner.Infrastructure.Clients.PlaylistClient;

public interface IPlaylistsClient
{
    Task<GetPlaylistResult> GetPlaylistAsync(string playlistId, CancellationToken cancellationToken = default);

    Task<GetPlaylistTracksResult> GetPlaylistTracksAsync(string playlistId, CancellationToken cancellationToken = default);

    Task DeleteTrackFromPlaylistAsync(string playlistId, string trackId, int trackIndex, CancellationToken cancellationToken = default);

    Task AddTrackToPlaylistAsync(string playlistId, string trackId, int trackIndex, CancellationToken cancellationToken = default);
}
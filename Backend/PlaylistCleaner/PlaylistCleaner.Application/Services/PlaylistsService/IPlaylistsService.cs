using PlaylistCleaner.Application.Results.Services.PlaylistsServiceResults;
using PlaylistCleaner.Application.Results.Utils.DuplicateDetector;

namespace PlaylistCleaner.Application.Services.PlaylistsService;

public interface IPlaylistsService
{
    Task<GetPlaylistDTO> GetPlaylistAsync(string playlistId, CancellationToken cancellationToken);

    Task<GetPlaylistTracksDTO> GetPlaylistTracksAsync(string playlistId, CancellationToken cancellationToken = default);

    Task<GetPlaylistDuplicatesDTO> GetPlaylistDuplicatesAsync(string playlistId, CancellationToken cancellationToken = default);

    Task DeleteTrackFromPlaylistAsync(string playlistId, string trackId, int trackIndex, CancellationToken cancellationToken = default);
}

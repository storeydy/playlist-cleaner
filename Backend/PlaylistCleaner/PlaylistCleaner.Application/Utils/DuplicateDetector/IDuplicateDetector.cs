using PlaylistCleaner.Application.Results.Utils.DuplicateDetector;
using PlaylistCleaner.Infrastructure.Results.PlaylistsClientResults;

namespace PlaylistCleaner.Application.Utils.DuplicateDetector;

public interface IDuplicateDetector
{
    GetPlaylistDuplicatesDTO GetDuplicatesFromPlaylistTracks(List<GetPlaylistTracksResultPlaylistTrack> tracks, CancellationToken cancellationToken = default);
}

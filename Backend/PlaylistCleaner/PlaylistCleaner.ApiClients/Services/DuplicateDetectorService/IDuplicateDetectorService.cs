using PlaylistCleaner.ApiClients.Responses.PlaylistsClientResults.GetPlaylistTracks;
using PlaylistCleaner.ApiClients.Results.PlaylistsClientResults.GetPlaylistDuplicates;

namespace PlaylistCleaner.ApiClients.Services.DuplicateDetectorService;

public interface IDuplicateDetectorService
{
    Task<GetPlaylistDuplicatesResult> GetDuplicatesFromPlaylistTracksAsync(List<GetPlaylistTracksResultPlaylistTrack> tracks, CancellationToken cancellationToken = default);
}
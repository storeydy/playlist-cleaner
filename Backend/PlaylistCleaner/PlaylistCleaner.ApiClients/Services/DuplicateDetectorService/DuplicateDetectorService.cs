using PlaylistCleaner.ApiClients.Responses.PlaylistsClientResults.GetPlaylistTracks;
using PlaylistCleaner.ApiClients.Results.PlaylistsClientResults.GetPlaylistDuplicates;

namespace PlaylistCleaner.ApiClients.Services.DuplicateDetectorService;

internal sealed class DuplicateDetectorService : IDuplicateDetectorService
{
    public async Task<GetPlaylistDuplicatesResult> GetDuplicatesFromPlaylistTracksAsync(List<GetPlaylistTracksResultPlaylistTrack> tracks, CancellationToken cancellationToken = default)
    {
        var duplicateTrackGroups = tracks
            .SelectMany(t => t.track.artists.Select(a => new { t.track.id, t.track.name, t.track.duration_ms, ArtistName = a.name }))
            .GroupBy(x => new { x.name, x.duration_ms, x.ArtistName })
            .Where(g => g.Count() > 1)
            .SelectMany(g => g.Select(x => new { x.id, x.name, x.duration_ms, Artists = string.Join(",", g.Select(y => y.ArtistName).Distinct().OrderBy(name => name)) }))
            .GroupBy(x => new { x.name, x.duration_ms, x.Artists })
            .Where(g => g.Count() > 1)
            .ToList();

        ICollection<GetPlaylistDuplicatesResultDuplicate> finalGroupedDuplicates = duplicateTrackGroups
            .Select(group => new GetPlaylistDuplicatesResultDuplicate(group.Select(t => t.id).ToList()))
            .ToList();

        return new GetPlaylistDuplicatesResult(finalGroupedDuplicates);
    }
}

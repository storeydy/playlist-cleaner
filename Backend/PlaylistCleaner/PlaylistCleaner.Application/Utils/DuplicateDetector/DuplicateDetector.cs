using PlaylistCleaner.Application.Results.Utils.DuplicateDetector;
using PlaylistCleaner.Infrastructure.Results.PlaylistsClientResults;

namespace PlaylistCleaner.Application.Utils.DuplicateDetector;

internal sealed class DuplicateDetector : IDuplicateDetector
{
    public GetPlaylistDuplicatesDTO GetDuplicatesFromPlaylistTracks(List<GetPlaylistTracksResultPlaylistTrack> tracks, CancellationToken cancellationToken = default)
    {
        var duplicateTrackGroups = tracks
                    .SelectMany(t => t.track.artists.Select(a => new { t.track.id, t.track.name, t.track.duration_ms, ArtistName = a.name }))
                    .GroupBy(x => new { x.name, x.duration_ms, x.ArtistName })
                    .Where(g => g.Count() > 1)
                    .SelectMany(g => g.Select(x => new { x.id, x.name, x.duration_ms, Artists = string.Join(",", g.Select(y => y.ArtistName).Distinct().OrderBy(name => name)) }))
                    .GroupBy(x => new { x.name, x.duration_ms, x.Artists })
                    .Where(g => g.Count() > 1)
                    .ToList();

        ICollection<GetPlaylistDuplicatesDTODuplicate> finalGroupedDuplicates = duplicateTrackGroups
            .Select(group => new GetPlaylistDuplicatesDTODuplicate(group.Select(t => t.id).ToList()))
            .ToList();

        return new GetPlaylistDuplicatesDTO(finalGroupedDuplicates);
    }
}

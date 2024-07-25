namespace PlaylistCleaner.Infrastructure.Results.PlaylistsClientResults;

public sealed record GetPlaylistTracksResult(string? next, List<GetPlaylistTracksResultPlaylistTrack> items);

public sealed record GetPlaylistTracksResultPlaylistTrack(DateTime added_at, GetPlaylistTracksResultPlaylistTrackAddedBy added_by, bool is_local, GetPlaylistTracksResultPlaylistTrackData track, int position = -1);

public sealed record GetPlaylistTracksResultPlaylistTrackAddedBy(string id);

public sealed record GetPlaylistTracksResultPlaylistTrackData(GetPlaylistTracksResultPlaylistTrackAlbum album, ICollection<GetPlaylistTracksResultPlaylistTrackArtist> artists, int duration_ms, bool @explicit, string id, string name, string type);

public sealed record GetPlaylistTracksResultPlaylistTrackAlbum(string id, ICollection<GetPlaylistTracksResultPlaylistTrackAlbumImageObject> images, string name);

public sealed record GetPlaylistTracksResultPlaylistTrackAlbumImageObject(string url, int height, int width);

public sealed record GetPlaylistTracksResultPlaylistTrackArtist(string id, string name);
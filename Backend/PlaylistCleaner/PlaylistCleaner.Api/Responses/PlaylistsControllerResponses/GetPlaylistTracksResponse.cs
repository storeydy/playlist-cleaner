namespace PlaylistCleaner.Api.Responses.PlaylistsControllerResponses;

public sealed record GetPlaylistTracksResponse(string? next, List<GetPlaylistTracksResponsePlaylistTrack> items);

public sealed record GetPlaylistTracksResponsePlaylistTrack(DateTime added_at, GetPlaylistTracksResponsePlaylistTrackAddedBy added_by, bool is_local, GetPlaylistTracksResponsePlaylistTrackData track);

public sealed record GetPlaylistTracksResponsePlaylistTrackAddedBy(string id);

public sealed record GetPlaylistTracksResponsePlaylistTrackData(GetPlaylistTracksResponsePlaylistTrackAlbum album, ICollection<GetPlaylistTracksResponsePlaylistTrackArtist> artists, int duration_ms, bool @explicit, string id, string name, string type);

public sealed record GetPlaylistTracksResponsePlaylistTrackAlbum(string id, ICollection<GetPlaylistTracksResponsePlaylistTrackAlbumImageObject> images, string name);

public sealed record GetPlaylistTracksResponsePlaylistTrackAlbumImageObject(string url, int height, int width);

public sealed record GetPlaylistTracksResponsePlaylistTrackArtist(string id, string name);
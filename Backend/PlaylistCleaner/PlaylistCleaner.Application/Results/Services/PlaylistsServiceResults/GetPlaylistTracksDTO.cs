namespace PlaylistCleaner.Application.Results.Services.PlaylistsServiceResults;

public sealed record GetPlaylistTracksDTO(string? next, List<GetPlaylistTracksDTOPlaylistTrack> items);

public sealed record GetPlaylistTracksDTOPlaylistTrack(DateTime added_at, GetPlaylistTracksDTOPlaylistTrackAddedBy added_by, bool is_local, GetPlaylistTracksDTOPlaylistTrackData track);

public sealed record GetPlaylistTracksDTOPlaylistTrackAddedBy(string id);

public sealed record GetPlaylistTracksDTOPlaylistTrackData(GetPlaylistTracksDTOPlaylistTrackAlbum album, ICollection<GetPlaylistTracksDTOPlaylistTrackArtist> artists, int duration_ms, bool @explicit, string id, string name, string type);

public sealed record GetPlaylistTracksDTOPlaylistTrackAlbum(string id, ICollection<GetPlaylistTracksDTOPlaylistTrackAlbumImageObject> images, string name);

public sealed record GetPlaylistTracksDTOPlaylistTrackAlbumImageObject(string url, int height, int width);

public sealed record GetPlaylistTracksDTOPlaylistTrackArtist(string id, string name);
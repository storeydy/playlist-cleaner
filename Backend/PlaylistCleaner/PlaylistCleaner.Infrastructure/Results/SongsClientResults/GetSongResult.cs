namespace PlaylistCleaner.Infrastructure.Results.SongsClientResults;

public sealed record GetSongResult(GetSongResultAlbum album, ICollection<GetSongResultArtist> artists, ICollection<string> available_markets, int duration_ms, bool @explicit, GetSongResultExternalIds external_ids, string href, string id, bool is_playable, GetSongResultRestrictions restrictions, string name, int popularity, string uri, bool is_local);

public sealed record GetSongResultAlbum(string album_type, ICollection<string> available_markets, string id, ICollection<GetSongResultAlbumImage> images, string name, string release_date, GetSongResultAlbumRestrictions restrictions, ICollection<GetSongResultAlbumArtist> artists);

public sealed record GetSongResultAlbumImage(string url, int? height, int? width);

public sealed record GetSongResultAlbumRestrictions(string reason);

public sealed record GetSongResultAlbumArtist(string id, string name);

public sealed record GetSongResultArtist(string id, string name);

public sealed record GetSongResultExternalIds(string isrc, string ean, string upc);

public sealed record GetSongResultRestrictions(string reason);
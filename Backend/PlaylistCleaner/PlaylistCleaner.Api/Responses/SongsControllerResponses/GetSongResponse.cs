namespace PlaylistCleaner.Api.Responses.SongsControllerResponses;

public sealed record GetSongResponse(GetSongResponseAlbum album, ICollection<GetSongResponseArtist> artists, ICollection<string> available_markets, int duration_ms, bool @explicit, GetSongResponseExternalIds external_ids, string href, string id, bool is_playable, ICollection<string> restrictions, string name, int popularity, string uri, bool is_local);

public sealed record GetSongResponseAlbum(string album_type, ICollection<string> available_markets, string id, ICollection<GetSongResponseAlbumImage> images, string name, string release_date, ICollection<string> restrictions, ICollection<GetSongResponseAlbumArtist> artists);

public sealed record GetSongResponseAlbumImage(string url, int? height, int? width);

public sealed record GetSongResponseAlbumArtist(string id, string name);

public sealed record GetSongResponseArtist(string id, string name);

public sealed record GetSongResponseExternalIds(string isrc, string ean, string upc);

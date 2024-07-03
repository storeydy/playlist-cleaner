namespace PlaylistCleaner.Application.Results.Services.SongsService;

public sealed record GetSongDTO(GetSongDTOAlbum album, ICollection<GetSongDTOArtist> artists, ICollection<string> available_markets, int duration_ms, bool @explicit, GetSongDTOExternalIds external_ids, string href, string id, bool is_playable, ICollection<string> restrictions, string name, int popularity, string uri, bool is_local);

public sealed record GetSongDTOAlbum(string album_type, ICollection<string> available_markets, string id, ICollection<GetSongDTOAlbumImage> images, string name, string release_date, ICollection<string> restrictions, ICollection<GetSongDTOAlbumArtist> artists);

public sealed record GetSongDTOAlbumImage(string url, int? height, int? width);

public sealed record GetSongDTOAlbumArtist(string id, string name);

public sealed record GetSongDTOArtist(string id, string name);

public sealed record GetSongDTOExternalIds(string isrc, string ean, string upc);

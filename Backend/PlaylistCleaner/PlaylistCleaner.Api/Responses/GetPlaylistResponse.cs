namespace PlaylistCleaner.Api.Responses;

public sealed record GetPlaylistResponse(bool collaborative, string description, GetPlaylistResponseExternalUrls external_urls, string href, string id, ICollection<GetPlaylistResponseImageObject> images, string name, GetPlaylistResponsePlaylistOwner owner, bool Public, string snapshot_id, ICollection<GetPlaylistResponseTrack> tracks, string type, string uri);

public sealed record GetPlaylistResponsePlaylistOwner(GetPlaylistResponseExternalUrls external_urls, GetPlaylistResponseFollowers followers, string href, string id, string type, string uri, string display_name);

public sealed record GetPlaylistResponseExternalIds(string isrc, string ean, string upc);

public sealed record GetPlaylistResponseTrack(DateTime added_at, GetPlaylistResponseTrackAddedBy added_by, bool is_local, GetPlaylistResponseTrackData track);

public sealed record GetPlaylistResponseTrackAddedBy(GetPlaylistResponseExternalUrls external_urls, GetPlaylistResponseFollowers followers, string href, string id, string type, string uri);

public sealed record GetPlaylistResponseTrackData(GetPlaylistResponseAlbum album, ICollection<GetPlaylistResponseArtist> artists, ICollection<string> available_markets, int disc_number, int duration_ms, bool @explicit, GetPlaylistResponseExternalIds external_ids, GetPlaylistResponseExternalUrls external_urls, string href, string id, bool is_playable, GetPlaylistResponseRestrictions restrictions, string name, int popularity, string? preview_url, int track_number, string type, string uri, bool is_local);

public sealed record GetPlaylistResponseAlbum(string album_type, int total_tracks, ICollection<string> available_markets, GetPlaylistResponseExternalUrls external_urls, string href, string id, ICollection<GetPlaylistResponseImageObject> images, string name, string release_date, string release_date_precision, GetPlaylistResponseRestrictions
    restrictions, string type, string uri, ICollection<GetPlaylistResponseAlbumArtist> artists);

public sealed record GetPlaylistResponseExternalUrls(string spotify);

public sealed record GetPlaylistResponseImageObject(string url, int? height, int? width);

public sealed record GetPlaylistResponseRestrictions(string reason);

public sealed record GetPlaylistResponseAlbumArtist(GetPlaylistResponseExternalUrls external_urls, string href, string id, string name, string type, string uri);

public sealed record GetPlaylistResponseArtist(GetPlaylistResponseExternalUrls external_urls, GetPlaylistResponseFollowers followers, ICollection<string> genres, string href, string id, ICollection<GetPlaylistResponseImageObject> images, string name, int popularity, string type, string uri);

public sealed record GetPlaylistResponseFollowers(string href, int total);
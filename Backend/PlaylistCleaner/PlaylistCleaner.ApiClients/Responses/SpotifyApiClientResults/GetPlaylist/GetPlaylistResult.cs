namespace PlaylistCleaner.ApiClients.Responses.SpotifyApiClientResults.GetPlaylist;

public sealed record GetPlaylistResult(bool collaborative, string description, GetPlaylistResultExternalUrls external_urls, string href, string id, ICollection<GetPlaylistResultImageObject> images, string name, GetPlaylistResultPlaylistOwner owner, bool Public, string snapshot_id, ICollection<GetPlaylistResultTrack> tracks, string type, string uri);

public sealed record GetPlaylistResultPlaylistOwner(GetPlaylistResultExternalUrls external_urls, GetPlaylistResultFollowers followers, string href, string id, string type, string uri, string display_name);

public sealed record GetPlaylistResultExternalIds(string isrc, string ean, string upc);

public sealed record GetPlaylistResultTrack(DateTime added_at, GetPlaylistResultTrackAddedBy added_by, bool is_local, GetPlaylistResultTrackData track);

public sealed record GetPlaylistResultTrackAddedBy(GetPlaylistResultExternalUrls external_urls, GetPlaylistResultFollowers followers, string href, string id, string type, string uri);

public sealed record GetPlaylistResultTrackData(GetPlaylistResultAlbum album, ICollection<GetPlaylistResultArtist> artists, ICollection<string> available_markets, int disc_number, int duration_ms, bool @explicit, GetPlaylistResultExternalIds external_ids, GetPlaylistResultExternalUrls external_urls, string href, string id, bool is_playable, GetPlaylistResultRestrictions restrictions, string name, int popularity, string? preview_url, int track_number, string type, string uri, bool is_local);

public sealed record GetPlaylistResultAlbum(string album_type, int total_tracks, ICollection<string> available_markets, GetPlaylistResultExternalUrls external_urls, string href, string id, ICollection<GetPlaylistResultImageObject> images, string name, string release_date, string release_date_precision, GetPlaylistResultRestrictions restrictions, string type, string uri, ICollection<GetPlaylistResultAlbumArtist> artists);

public sealed record GetPlaylistResultExternalUrls(string spotify);

public sealed record GetPlaylistResultImageObject(string url, int? height, int? width);

public sealed record GetPlaylistResultRestrictions(string reason);

public sealed record GetPlaylistResultAlbumArtist(GetPlaylistResultExternalUrls external_urls, string href, string id, string name, string type, string uri);

public sealed record GetPlaylistResultArtist(GetPlaylistResultExternalUrls external_urls, GetPlaylistResultFollowers followers, ICollection<string> genres, string href, string id, ICollection<GetPlaylistResultImageObject> images, string name, int popularity, string type, string uri);

public sealed record GetPlaylistResultFollowers(string href, int total);
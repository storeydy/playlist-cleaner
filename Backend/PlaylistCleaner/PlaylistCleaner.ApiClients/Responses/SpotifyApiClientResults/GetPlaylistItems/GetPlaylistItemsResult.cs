namespace PlaylistCleaner.ApiClients.Responses.SpotifyApiClientResults.GetPlaylistItems;

public sealed record GetPlaylistItemsResult(string href, int limit, string? next, int offset, string? previous, int total, ICollection<PlaylistTrackObject> items);

public sealed record PlaylistTrackObject(DateTime added_at, AddedBy added_by, bool is_local, TrackObject track);

public sealed record AddedBy(External_Urls external_urls, Followers followers, string href, string id, string type, string uri);

public sealed record TrackObject(Album album, ICollection<ArtistObject> artists, ICollection<string> available_markets, int disc_number, int duration_ms, bool @explicit, External_Ids external_ids, External_Urls external_urls, string href, string id, bool is_playable, Restrictions restrictions, string name, int popularity, string? preview_url, int track_number, string type, string uri, bool is_local);

public sealed record Album(string album_type, int total_tracks, ICollection<string> available_markets, External_Urls external_urls, string href, string id, ICollection<ImageObject> images, string name, string release_date, string release_date_precision, Restrictions restrictions, string type, string uri, ICollection<SimplifiedArtistObject> artists);

public sealed record External_Urls(string spotify);

public sealed record External_Ids(string isrc, string ean, string upc);

public sealed record ImageObject(string url, int height, int width);

public sealed record Restrictions(string reason);

public sealed record SimplifiedArtistObject(External_Urls external_urls, string href, string id, string name, string type, string uri);

public sealed record ArtistObject(External_Urls external_urls, Followers followers, ICollection<string> genres, string href, string id, ICollection<ImageObject> images, string name, int popularity, string type, string uri);

public sealed record Followers(string href, int total);
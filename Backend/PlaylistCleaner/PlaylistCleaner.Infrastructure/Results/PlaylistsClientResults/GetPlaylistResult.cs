namespace PlaylistCleaner.Infrastructure.Results.PlaylistsClientResults;

public sealed record GetPlaylistResult(bool collaborative, string description, GetPlaylistResultExternalUrls external_urls, string href, string id, ICollection<GetPlaylistResultImageObject> images, string name, GetPlaylistResultPlaylistOwner owner, GetPlaylistResultFollowers followers, bool Public, string snapshot_id, GetPlaylistResultTrack tracks, string type, string uri);

public sealed record GetPlaylistResultPlaylistOwner(GetPlaylistResultExternalUrls external_urls, GetPlaylistResultFollowers followers, string href, string id, string type, string uri, string display_name);

public sealed record GetPlaylistResultExternalIds(string isrc, string ean, string upc);

public sealed record GetPlaylistResultTrack(int total);

public sealed record GetPlaylistResultExternalUrls(string spotify);

public sealed record GetPlaylistResultImageObject(string url, int? height, int? width);

public sealed record GetPlaylistResultFollowers(string href, int total);
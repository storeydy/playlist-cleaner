namespace PlaylistCleaner.Api.Responses.PlaylistsControllerResponses;

public sealed record GetPlaylistResponse(bool collaborative, string description, GetPlaylistResponseExternalUrls external_urls, string href, string id, GetPlaylistResponseFollowers followers, ICollection<GetPlaylistResponseImageObject> images, string name, GetPlaylistResponsePlaylistOwner owner, bool Public, string snapshot_id, GetPlaylistResponseTrack tracks, string type, string uri);

public sealed record GetPlaylistResponsePlaylistOwner(GetPlaylistResponseExternalUrls external_urls, GetPlaylistResponseFollowers followers, string href, string id, string type, string uri, string display_name);

public sealed record GetPlaylistResponseExternalIds(string isrc, string ean, string upc);

public sealed record GetPlaylistResponseTrack(int total);

public sealed record GetPlaylistResponseExternalUrls(string spotify);

public sealed record GetPlaylistResponseImageObject(string url, int? height, int? width);

public sealed record GetPlaylistResponseFollowers(string href, int total);
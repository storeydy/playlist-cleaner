namespace PlaylistCleaner.ApiClients.Responses.SpotifyApiClientResults.GetUsersPlaylists;

public sealed record GetUsersPlaylistsResult(string href, int limit, string next, int offset, string previous, int total, IList<SimplifiedPlaylistObject> items);

public sealed record SimplifiedPlaylistObject(bool collaborative, string description, external_urls external_urls, string href, string id, ICollection<ImageObject> images, string name, GetUsersPlaylistResultPlaylistOwner owner, bool Public, string snapshot_id, GetUsersPlaylistResultPlaylistTracks tracks, string type, string uri);

public sealed record external_urls(string spotify);

public sealed record ImageObject(string url, int? height, int? width);

public sealed record GetUsersPlaylistResultPlaylistOwner(GetUsersPlaylistResultPlaylistOwnerExternalUrls external_urls, GetUsersPlaylistResultPlaylistOwnerFollowers followers, string href, string id, string type, string uri, string display_name);

public sealed record GetUsersPlaylistResultPlaylistOwnerFollowers(string href, int total);
public sealed record GetUsersPlaylistResultPlaylistOwnerExternalUrls(string spotify);

public sealed record GetUsersPlaylistResultPlaylistTracks(string href, int total);
namespace PlaylistCleaner.Api.Responses;

public sealed record GetUsersPlaylistsResponse(string href, int limit, string next, int offset, string previous, int total, ICollection<GetUsersPlaylistResponsePlaylist> items);

public sealed record GetUsersPlaylistResponsePlaylist(bool collaborative, string description, GetUsersPlaylistResponsePlaylistExternalUrls external_urls, string href, string id, ICollection<GetUsersPlaylistResponsePlaylistImages> images, string name, GetUsersPlaylistResponsePlaylistOwner owner, bool Public, string snapshot_id, GetUsersPlaylistResponsePlaylistTracks tracks, string type, string uri);

public sealed record GetUsersPlaylistResponsePlaylistExternalUrls(string spotify);

public sealed record GetUsersPlaylistResponsePlaylistImages(string url, int height, int width);

public sealed record GetUsersPlaylistResponsePlaylistOwner(GetUsersPlaylistResponsePlaylistOwnerExternalUrls external_urls, GetUsersPlaylistResponsePlaylistOwnerFollowers followers, string href, string id, string type, string uri, string display_name);

public sealed record GetUsersPlaylistResponsePlaylistOwnerFollowers(string href, int total);
public sealed record GetUsersPlaylistResponsePlaylistOwnerExternalUrls(string spotify);

public sealed record GetUsersPlaylistResponsePlaylistTracks(string href, int total);

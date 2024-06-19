namespace PlaylistCleaner.Application.Results.Services.PlaylistsServiceResults;

public sealed record GetPlaylistDTO(bool collaborative, string description, GetPlaylistDTOExternalUrls external_urls, string href, string id, ICollection<GetPlaylistDTOImageObject> images, string name, GetPlaylistDTOPlaylistOwner owner, GetPlaylistDTOFollowers followers, bool Public, string snapshot_id, GetPlaylistDTOTrack tracks, string type, string uri);

public sealed record GetPlaylistDTOPlaylistOwner(GetPlaylistDTOExternalUrls external_urls, GetPlaylistDTOFollowers followers, string href, string id, string type, string uri, string display_name);

public sealed record GetPlaylistDTOExternalIds(string isrc, string ean, string upc);

public sealed record GetPlaylistDTOTrack(int total);

public sealed record GetPlaylistDTOExternalUrls(string spotify);

public sealed record GetPlaylistDTOImageObject(string url, int? height, int? width);

public sealed record GetPlaylistDTOFollowers(string href, int total);
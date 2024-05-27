namespace PlaylistCleaner.Api.Responses.UsersControllerResponses;

public sealed record GetCurrentUsersProfileResponse(string country, string display_name, string email, GetCurrentUsersProfileResponseExplicitContent explicit_content, string spotify_external_url, GetCurrentUsersProfileResponseFollower? followers, string href, string id, ICollection<GetCurrentUsersProfileResponseImage> images, string product, string type, string uri);

public sealed record GetCurrentUsersProfileResponseImage(string url, int height, int width);

public sealed record GetCurrentUsersProfileResponseExplicitContent(bool filter_enabled, bool filter_locked);

public sealed record GetCurrentUsersProfileResponseFollower(string href, int total);

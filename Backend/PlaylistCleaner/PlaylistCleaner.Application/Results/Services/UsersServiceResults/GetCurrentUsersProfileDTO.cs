namespace PlaylistCleaner.Application.Results.Services.UsersServiceResults;

public sealed record GetCurrentUsersProfileDTO(string country, string display_name, string email, GetCurrentUsersProfileDTOExplicit_Content? explicit_content, string spotify_external_url, GetCurrentUsersProfileDTOFollowers? followers, string href, string id, ICollection<GetCurrentUsersProfileDTOImageObject> images, string product, string type, string uri);

public sealed record GetCurrentUsersProfileDTOImageObject(string url, int height, int width);

public sealed record GetCurrentUsersProfileDTOFollowers(string href, int total);

public sealed record GetCurrentUsersProfileDTOExplicit_Content(bool filter_enabled, bool filter_locked);

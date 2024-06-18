namespace PlaylistCleaner.ApiClients.Responses.UserProfileClientResults.GetCurrentUsersProfile;

public sealed record GetCurrentUsersProfileResult(string country, string display_name, string email, Explicit_Content? explicit_content, External_Urls? external_urls, Followers? followers, string href, string id, ICollection<ImageObject> images, string product, string type, string uri);

public sealed record ImageObject(string url, int height, int width);

public sealed record Followers(string href, int total);

public sealed record External_Urls(string spotify);

public sealed record Explicit_Content(bool filter_enabled, bool filter_locked);

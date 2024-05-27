namespace PlaylistCleaner.ApiClients.Responses.UsersClientResponses.GetUserProfile;

public sealed record GetUserProfileResult(string displayName, ICollection<string> externalUrls, GetUserProfileResultFollower followers, string href, string id, ICollection<GetUserProfileResultImage> images, string type, string uri);

public sealed record GetUserProfileResultImage(string url, int height, int width);

public sealed record GetUserProfileResultFollower(string href, int total);

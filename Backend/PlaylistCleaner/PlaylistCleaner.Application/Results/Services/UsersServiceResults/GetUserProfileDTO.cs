namespace PlaylistCleaner.Application.Results.Services.UsersServiceResults;

public sealed record GetUserProfileDTO(string displayName, ICollection<string> externalUrls, GetUserProfileDTOFollower followers, string href, string id, ICollection<GetUserProfileDTOImage> images, string type, string uri);

public sealed record GetUserProfileDTOImage(string url, int height, int width);

public sealed record GetUserProfileDTOFollower(string href, int total);
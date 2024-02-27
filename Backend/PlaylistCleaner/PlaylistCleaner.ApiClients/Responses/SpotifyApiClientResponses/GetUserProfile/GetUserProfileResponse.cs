namespace PlaylistCleaner.ApiClients.Responses.SpotifyApiClientResponses.GetUserProfile
{
    public sealed record GetUserProfileResponse(string displayName, ICollection<string> externalUrls, GetUserProfileResponseFollower followers, string href, string id, ICollection<GetUserProfileResponseImage> images, string type, string uri);

    public sealed record GetUserProfileResponseImage(string url, int height, int width);

    public sealed record GetUserProfileResponseFollower(string href, int total);
}

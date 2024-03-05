using PlaylistCleaner.ApiClients.Responses.SpotifyApiClientResults.GetUserProfile;

namespace PlaylistCleaner.ApiClients.Clients.SpotifyApiClient;

public interface ISpotifyApiClient
{
    Task<GetUserProfileResult> GetUserProfileAsync(string userId, string jwt, CancellationToken cancellationToken = default);

    Task<GetCurrentUsersProfileResult> GetCurrentUsersProfileAsync(string jwt, CancellationToken cancellationToken = default);
}

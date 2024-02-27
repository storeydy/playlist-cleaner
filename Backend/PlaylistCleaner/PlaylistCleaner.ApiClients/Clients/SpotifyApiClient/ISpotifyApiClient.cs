using PlaylistCleaner.ApiClients.Responses.SpotifyApiClientResponses.GetUserProfile;

namespace PlaylistCleaner.ApiClients.Clients.SpotifyApiClient;

public interface ISpotifyApiClient
{
    Task<GetUserProfileResponse> GetUserProfileAsync(string userId, string jwt, CancellationToken cancellationToken = default);
}

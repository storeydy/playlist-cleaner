using PlaylistCleaner.ApiClients.Responses.UserProfileClientResults.GetCurrentUsersProfile;

namespace PlaylistCleaner.ApiClients.Clients.UserProfilesClient;

public interface IUserProfilesClient
{
    Task<GetCurrentUsersProfileResult> GetCurrentUsersProfileAsync(CancellationToken cancellationToken = default);
}
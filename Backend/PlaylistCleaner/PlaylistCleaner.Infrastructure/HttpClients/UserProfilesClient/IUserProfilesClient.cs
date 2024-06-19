using PlaylistCleaner.Infrastructure.Results.UserProfileClientResults;

namespace PlaylistCleaner.Infrastructure.Clients.UserProfilesClient;

public interface IUserProfilesClient
{
    Task<GetCurrentUsersProfileResult> GetCurrentUsersProfileAsync(CancellationToken cancellationToken = default);
}
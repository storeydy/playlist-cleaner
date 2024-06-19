using PlaylistCleaner.Infrastructure.Results.UsersClientResults;

namespace PlaylistCleaner.Infrastructure.Clients.UsersClient;

public interface IUsersClient
{
    Task<GetUserProfileResult> GetUserProfileAsync(string userId, CancellationToken cancellationToken = default);

    Task<GetUsersPlaylistsResult> GetUserPlaylistsAsync(string userId, CancellationToken cancellationToken = default);
}

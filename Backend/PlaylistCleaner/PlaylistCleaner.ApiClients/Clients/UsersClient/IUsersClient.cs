using PlaylistCleaner.ApiClients.Responses.UsersClientResponses.GetUserProfile;
using PlaylistCleaner.ApiClients.Responses.UsersClientResponses.GetUsersPlaylists;

namespace PlaylistCleaner.ApiClients.Clients.UsersClient;

public interface IUsersClient
{
    Task<GetUserProfileResult> GetUserProfileAsync(string userId, CancellationToken cancellationToken = default);

    Task<GetUsersPlaylistsResult> GetUserPlaylistsAsync(string userId, CancellationToken cancellationToken = default);
}

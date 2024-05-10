using PlaylistCleaner.ApiClients.Responses.SpotifyApiClientResults.GetUserProfile;
using PlaylistCleaner.ApiClients.Responses.SpotifyApiClientResults.GetUsersPlaylists;

namespace PlaylistCleaner.ApiClients.Clients.UsersClient;

public interface IUsersClient
{
    Task<GetUserProfileResult> GetUserProfileAsync(string userId, CancellationToken cancellationToken = default);

    Task<GetUsersPlaylistsResult> GetUserPlaylistsAsync(string userId, CancellationToken cancellationToken = default);
}

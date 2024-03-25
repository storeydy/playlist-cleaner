using PlaylistCleaner.ApiClients.Responses.SpotifyApiClientResults.GetCurrentUsersProfile;
using PlaylistCleaner.ApiClients.Responses.SpotifyApiClientResults.GetPlaylist;
using PlaylistCleaner.ApiClients.Responses.SpotifyApiClientResults.GetUserProfile;
using PlaylistCleaner.ApiClients.Responses.SpotifyApiClientResults.GetUsersPlaylists;

namespace PlaylistCleaner.ApiClients.Clients.SpotifyApiClient;

public interface ISpotifyApiClient
{
    Task<GetUserProfileResult> GetUserProfileAsync(string userId, string jwt, CancellationToken cancellationToken = default);

    Task<GetCurrentUsersProfileResult> GetCurrentUsersProfileAsync(string jwt, CancellationToken cancellationToken = default);

    Task<GetUsersPlaylistsResult> GetUserPlaylistsAsync(string userId, string jwt, CancellationToken cancellationToken = default);

    Task<GetPlaylistResult> GetPlaylistAsync(string playlistId, string jwt, CancellationToken cancellationToken = default, int trackLimit = 50);
}

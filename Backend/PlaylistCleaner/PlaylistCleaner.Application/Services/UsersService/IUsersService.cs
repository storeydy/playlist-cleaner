using PlaylistCleaner.Application.Results.Services.UsersServiceResults;

namespace PlaylistCleaner.Application.Services.UsersService;

public interface IUsersService
{
    Task<GetCurrentUsersProfileDTO> GetCurrentUsersProfileAsync(CancellationToken cancellationToken = default);

    Task<GetUserProfileDTO> GetUserProfileAsync(string userId, CancellationToken cancellationToken = default);

    Task<GetUserPlaylistsDTO> GetUserPlaylistsAsync(string userId, CancellationToken cancellationToken = default);
}

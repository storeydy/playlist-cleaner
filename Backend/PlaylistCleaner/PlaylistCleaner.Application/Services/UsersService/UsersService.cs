using AutoMapper;
using PlaylistCleaner.Application.Results.Services.UsersServiceResults;
using PlaylistCleaner.Infrastructure.Clients.UserProfilesClient;
using PlaylistCleaner.Infrastructure.Clients.UsersClient;

namespace PlaylistCleaner.Application.Services.UsersService;

internal sealed class UsersService : IUsersService
{
    private readonly IUsersClient _usersClient;
    private readonly IUserProfilesClient _userProfilesClient;
    private readonly IMapper _mapper;

    public UsersService(IUsersClient usersClient, IUserProfilesClient userProfilesClient, IMapper mapper)
    {
        _usersClient = usersClient;
        _userProfilesClient = userProfilesClient;
        _mapper = mapper;
    }

    public async Task<GetCurrentUsersProfileDTO> GetCurrentUsersProfileAsync(CancellationToken cancellationToken = default)
    {
        var result = await _userProfilesClient.GetCurrentUsersProfileAsync(cancellationToken);

        return _mapper.Map<GetCurrentUsersProfileDTO>(result);
    }

    public async Task<GetUserPlaylistsDTO> GetUserPlaylistsAsync(string userId, CancellationToken cancellationToken = default)
    {
        var result = await _usersClient.GetUserPlaylistsAsync(userId, cancellationToken);

        return _mapper.Map<GetUserPlaylistsDTO>(result);
    }

    public async Task<GetUserProfileDTO> GetUserProfileAsync(string userId, CancellationToken cancellationToken = default)
    {
        var result = await _usersClient.GetUserProfileAsync(userId, cancellationToken);

        return _mapper.Map<GetUserProfileDTO>(result);
    }
}

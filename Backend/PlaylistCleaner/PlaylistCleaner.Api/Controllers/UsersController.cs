using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlaylistCleaner.Api.Responses;
using PlaylistCleaner.ApiClients.Clients.UserProfileClient;
using PlaylistCleaner.ApiClients.Clients.UsersClient;

namespace PlaylistCleaner.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[AllowAnonymous]
[Route("/api/v{version:apiVersion}/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserProfileClient _userProfileClient;
    private readonly IUsersClient _usersClient;
    private readonly IMapper _mapper;

    public UsersController(IUserProfileClient userProfileClient, IUsersClient usersClient, IMapper mapper)
    {
        _userProfileClient = userProfileClient;
        _usersClient = usersClient;
        _mapper = mapper;
    }

    [HttpGet("{userId}/profile")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetUserProfileResponse>> GetUserProfileAsync(string userId, CancellationToken cancellationToken = default)
    { 
        var result = await _usersClient.GetUserProfileAsync(userId, cancellationToken);

        var response = _mapper.Map<GetUserProfileResponse>(result);
        return Ok(response);
    }

    [HttpGet("me")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GetCurrentUsersProfileResponse>> GetCurrentUsersProfileAsync(CancellationToken cancellationToken = default)
    {
        var result = await _userProfileClient.GetCurrentUsersProfileAsync(cancellationToken);

        var response = _mapper.Map<GetCurrentUsersProfileResponse>(result);

        return Ok(response);
    }

    [HttpGet("{userId}/playlists")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetUsersPlaylistsResponse>> GetUsersPlaylistAsync(string userId, CancellationToken cancellationToken = default)
    {
        var result = await _usersClient.GetUserPlaylistsAsync(userId, cancellationToken);

        var response = _mapper.Map<GetUsersPlaylistsResponse>(result);

        return Ok(response);
    }
}

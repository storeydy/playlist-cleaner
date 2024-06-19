using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlaylistCleaner.Api.Responses.UsersControllerResponses;
using PlaylistCleaner.Application.Services.UsersService;

namespace PlaylistCleaner.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[AllowAnonymous]
[Route("/api/v{version:apiVersion}/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;
    private readonly IMapper _mapper;

    public UsersController(IUsersService usersService, IMapper mapper)
    {
        _usersService = usersService;
        _mapper = mapper;
    }

    [HttpGet("{userId}/profile")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetUserProfileResponse>> GetUserProfileAsync(string userId, CancellationToken cancellationToken = default)
    { 
        var result = await _usersService.GetUserProfileAsync(userId, cancellationToken);

        var response = _mapper.Map<GetUserProfileResponse>(result);
        return Ok(response);
    }

    [HttpGet("me")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GetCurrentUsersProfileResponse>> GetCurrentUsersProfileAsync(CancellationToken cancellationToken = default)
    {
        var result = await _usersService.GetCurrentUsersProfileAsync(cancellationToken);

        var response = _mapper.Map<GetCurrentUsersProfileResponse>(result);

        return Ok(response);
    }

    [HttpGet("{userId}/playlists")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetUsersPlaylistsResponse>> GetUsersPlaylistAsync(string userId, CancellationToken cancellationToken = default)
    {
        var result = await _usersService.GetUserPlaylistsAsync(userId, cancellationToken);

        var response = _mapper.Map<GetUsersPlaylistsResponse>(result);

        return Ok(response);
    }
}

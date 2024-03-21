using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlaylistCleaner.Api.Extensions;
using PlaylistCleaner.Api.Responses;
using PlaylistCleaner.ApiClients.Clients.SpotifyApiClient;

namespace PlaylistCleaner.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[AllowAnonymous]
[Route("/api/v{version:apiVersion}/[controller]")]
public class PlaylistsController : ControllerBase
{
    private readonly ISpotifyApiClient _apiClient;
    private readonly IMapper _mapper;

    public PlaylistsController(ISpotifyApiClient apiClient, IMapper mapper)
    {
        _apiClient = apiClient;
        _mapper = mapper;
    }

    [HttpGet("{userId}/playlists")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetUsersPlaylistsResponse>> GetUsersPlaylistAsync(string userId, CancellationToken cancellationToken = default)
    {
        string token = TokenExtensions.ExtractTokenFromHeaders(HttpContext.Request.Headers);

        var result = await _apiClient.GetUserPlaylistsAsync(userId, token, cancellationToken);

        var response = _mapper.Map<GetUsersPlaylistsResponse>(result);

        return Ok(response);
    }

    [HttpGet("{playlistId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GetPlaylistResponse>> GetPlaylistAsync(string playlistId, CancellationToken cancellationToken = default)
    {
        string token = TokenExtensions.ExtractTokenFromHeaders(HttpContext.Request.Headers);

        var result = await _apiClient.GetPlaylistAsync(playlistId, token, cancellationToken);

        var response = _mapper.Map<GetPlaylistResponse>(result);

        return Ok(response);
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlaylistCleaner.Api.Responses;
using PlaylistCleaner.ApiClients.Clients.PlaylistClient;

namespace PlaylistCleaner.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[AllowAnonymous]
[Route("/api/v{version:apiVersion}/[controller]")]
public class PlaylistsController : ControllerBase
{
    private readonly IPlaylistsClient _playlistClient;
    private readonly IMapper _mapper;

    public PlaylistsController(IPlaylistsClient playlistClient, IMapper mapper)
    {
        _playlistClient = playlistClient;
        _mapper = mapper;
    }

    [HttpGet("{playlistId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GetPlaylistResponse>> GetPlaylistAsync(string playlistId, CancellationToken cancellationToken = default)
    {
        var playlist = await _playlistClient.GetPlaylistAsync(playlistId, cancellationToken);

        var response = _mapper.Map<GetPlaylistResponse>(playlist);

        return Ok(response);
    }
}

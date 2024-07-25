using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlaylistCleaner.Api.Responses.PlaylistsControllerResponses;
using PlaylistCleaner.Application.Services.PlaylistsService;

namespace PlaylistCleaner.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[AllowAnonymous]
[Route("/api/v{version:apiVersion}/[controller]")]
public class PlaylistsController : ControllerBase
{
    private readonly IPlaylistsService _playlistsService;
    private readonly IMapper _mapper;

    public PlaylistsController(IPlaylistsService playlistsService, IMapper mapper)
    {
        _playlistsService = playlistsService;
        _mapper = mapper;
    }

    [HttpGet("{playlistId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GetPlaylistResponse>> GetPlaylistAsync(string playlistId, CancellationToken cancellationToken = default)
    {
        var playlist = await _playlistsService.GetPlaylistAsync(playlistId, cancellationToken);

        var response = _mapper.Map<GetPlaylistResponse>(playlist);

        return Ok(response);
    }

    [HttpGet("{playlistId}/tracks")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GetPlaylistTracksResponse>> GetPlaylistTracksAsync(string playlistId, CancellationToken cancellationToken = default)
    {
        var playlistTracks = await _playlistsService.GetPlaylistTracksAsync(playlistId, cancellationToken);

        var response = _mapper.Map<GetPlaylistTracksResponse>(playlistTracks);

        return Ok(response);
    }

    [HttpGet("{playlistId}/duplicates")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GetPlaylistDuplicatesResponse>> GetPlaylistDuplicatesAsync(string playlistId, CancellationToken cancellationToken = default)
    {
        var playlistTracks = await _playlistsService.GetPlaylistDuplicatesAsync(playlistId, cancellationToken);

        var response = _mapper.Map<GetPlaylistDuplicatesResponse>(playlistTracks);

        return Ok(response);
    }

    [HttpDelete("{playlistId}/tracks/{trackId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteTrackFromPlaylistAsync([FromRoute]string playlistId, [FromRoute]string trackId, [FromQuery]int trackIndex, CancellationToken cancellationToken = default)
    {
        await _playlistsService.DeleteTrackFromPlaylistAsync(playlistId, trackId, trackIndex, cancellationToken);

        return NoContent();
    }
}

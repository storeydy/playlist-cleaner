using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlaylistCleaner.Api.Responses.SongsControllerResponses;
using PlaylistCleaner.Application.Services.SongsService;

namespace PlaylistCleaner.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[AllowAnonymous]
[Route("/api/v{version:apiVersion}/[controller]")]
public class SongsController : ControllerBase
{
    private readonly ISongsService _songsService;
    private readonly IMapper _mapper;

    public SongsController(ISongsService songsService, IMapper mapper)
    {
        _songsService = songsService;
        _mapper = mapper;
    }

    [HttpGet("{songId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GetSongResponse>> GetSongAsync(string songId, CancellationToken cancellationToken = default)
    {
        var song = await _songsService.GetSongAsync(songId, cancellationToken);

        var response = _mapper.Map<GetSongResponse>(song);

        return Ok(response);
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlaylistCleaner.Api.Responses;
using PlaylistCleaner.ApiClients.Clients.SpotifyApiClient;

namespace PlaylistCleaner.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[AllowAnonymous]
[Route("/api/v{version:apiVersion}/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ISpotifyApiClient _apiClient;
    private readonly IMapper _mapper;
    
    public UsersController(ISpotifyApiClient apiClient, IMapper mapper)
    {
        _apiClient = apiClient;
        _mapper = mapper;
    }

    [HttpGet("{userId}/profile")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
     public async Task<ActionResult<GetUserProfileResponse>> GetUserProfileAsync(string userId, CancellationToken cancellationToken = default)
    {
        string? requestAuthHeader = HttpContext.Request.Headers.Authorization;

        string token = requestAuthHeader.Substring(requestAuthHeader.IndexOf("Bearer ") + 7);

        var result = await _apiClient.GetUserProfileAsync(userId, token, cancellationToken);

        var response = _mapper.Map<GetUserProfileResponse>(result);
        return Ok(response);
    }

    [HttpGet("me")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetCurrentUsersProfileResponse>> GetCurrentUsersProfileAsync(CancellationToken cancellationToken = default)
    {
        string? requestAuthHeader = HttpContext.Request.Headers.Authorization;

        string token = requestAuthHeader.Substring(requestAuthHeader.IndexOf("Bearer ") + 7);

        var result = await _apiClient.GetCurrentUsersProfileAsync(token, cancellationToken);

        var response = _mapper.Map<GetCurrentUsersProfileResponse>(result);

        return Ok(response);
    }
}

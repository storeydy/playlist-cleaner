using AutoMapper;
using PlaylistCleaner.Application.Results.Services.SongsService;
using PlaylistCleaner.Infrastructure.HttpClients.SongsClient;

namespace PlaylistCleaner.Application.Services.SongsService;

internal sealed class SongsService : ISongsService
{
    private readonly ISongsClient _songsClient;
    private readonly IMapper _mapper;

    public SongsService(ISongsClient songsClient, IMapper mapper)
    {
        _songsClient = songsClient;
        _mapper = mapper;
    }

    public async Task<GetSongDTO> GetSongAsync(string songId, CancellationToken cancellationToken = default)
    {
        var result = await _songsClient.GetSongAsync(songId, cancellationToken);

        return _mapper.Map<GetSongDTO>(result);
    }
}

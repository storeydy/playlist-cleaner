using PlaylistCleaner.Application.Results.Services.SongsService;

namespace PlaylistCleaner.Application.Services.SongsService;

public interface ISongsService
{
    Task<GetSongDTO> GetSongAsync(string songId, CancellationToken cancellationToken = default);
}

using PlaylistCleaner.Infrastructure.Results.SongsClientResults;

namespace PlaylistCleaner.Infrastructure.HttpClients.SongsClient;

public interface ISongsClient
{
    Task<GetSongResult> GetSongAsync(string songId, CancellationToken cancellationToken = default);
}

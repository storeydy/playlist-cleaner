using AutoMapper;
using PlaylistCleaner.Application.Results.Services.PlaylistsServiceResults;
using PlaylistCleaner.Application.Results.Utils.DuplicateDetector;
using PlaylistCleaner.Application.Utils.DuplicateDetector;
using PlaylistCleaner.Infrastructure.Clients.PlaylistClient;

namespace PlaylistCleaner.Application.Services.PlaylistsService;

internal sealed class PlaylistsService : IPlaylistsService
{
    private readonly IPlaylistsClient _playlistsClient;
    private readonly IDuplicateDetector _duplicateDetector;
    private readonly IMapper _mapper;

    public PlaylistsService(IPlaylistsClient playlistsClient, IDuplicateDetector duplicateDetector, IMapper mapper)
    {
        _playlistsClient = playlistsClient;
        _duplicateDetector = duplicateDetector;
        _mapper = mapper;
    }
    public async Task<GetPlaylistDTO> GetPlaylistAsync(string playlistId, CancellationToken cancellationToken)
    {
        var result = await _playlistsClient.GetPlaylistAsync(playlistId, cancellationToken);

        return _mapper.Map<GetPlaylistDTO>(result);
    }

    public async Task<GetPlaylistDuplicatesDTO> GetPlaylistDuplicatesAsync(string playlistId, CancellationToken cancellationToken = default)
    {
        var playlistTracks = await _playlistsClient.GetPlaylistTracksAsync(playlistId, cancellationToken);

        var duplicates = _duplicateDetector.GetDuplicatesFromPlaylistTracks(playlistTracks.items);

        return duplicates;
    }

    public async Task<GetPlaylistTracksDTO> GetPlaylistTracksAsync(string playlistId, CancellationToken cancellationToken = default)
    {
        var result = await _playlistsClient.GetPlaylistTracksAsync(playlistId, cancellationToken);

        return _mapper.Map<GetPlaylistTracksDTO>(result);
    }

    public async Task DeleteTrackFromPlaylistAsync(string playlistId, string trackId, int trackIndex, CancellationToken cancellationToken = default)
    {
        await _playlistsClient.DeleteTrackFromPlaylistAsync(playlistId, trackId, trackIndex, cancellationToken);
    }
}

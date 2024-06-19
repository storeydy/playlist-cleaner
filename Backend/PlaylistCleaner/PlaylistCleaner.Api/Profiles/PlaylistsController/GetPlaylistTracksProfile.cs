using AutoMapper;
using PlaylistCleaner.Api.Responses.PlaylistsControllerResponses;
using PlaylistCleaner.Application.Results.Services.PlaylistsServiceResults;

namespace PlaylistCleaner.Api.Profiles.PlaylistsController;

internal sealed class GetPlaylistTracksProfile : Profile
{
    public GetPlaylistTracksProfile()
    {
        CreateMap<GetPlaylistTracksDTOPlaylistTrackArtist, GetPlaylistTracksResponsePlaylistTrackArtist>();
        CreateMap<GetPlaylistTracksDTOPlaylistTrackAlbumImageObject, GetPlaylistTracksResponsePlaylistTrackAlbumImageObject>();
        CreateMap<GetPlaylistTracksDTOPlaylistTrackAlbum, GetPlaylistTracksResponsePlaylistTrackAlbum>();
        CreateMap<GetPlaylistTracksDTOPlaylistTrackData, GetPlaylistTracksResponsePlaylistTrackData>();
        CreateMap<GetPlaylistTracksDTOPlaylistTrackAddedBy, GetPlaylistTracksResponsePlaylistTrackAddedBy>();
        CreateMap<GetPlaylistTracksDTOPlaylistTrack, GetPlaylistTracksResponsePlaylistTrack>();
        CreateMap<GetPlaylistTracksDTO, GetPlaylistTracksResponse>();
    }
}

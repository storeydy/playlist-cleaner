using AutoMapper;
using PlaylistCleaner.Application.Results.Services.PlaylistsServiceResults;
using PlaylistCleaner.Infrastructure.Results.PlaylistsClientResults;

namespace PlaylistCleaner.Application.Profiles.PlaylistsServiceProfiles;

internal sealed class GetPlaylistTracksProfile : Profile
{
    public GetPlaylistTracksProfile()
    {
        CreateMap<GetPlaylistTracksResultPlaylistTrackArtist, GetPlaylistTracksDTOPlaylistTrackArtist>();
        CreateMap<GetPlaylistTracksResultPlaylistTrackAlbumImageObject, GetPlaylistTracksDTOPlaylistTrackAlbumImageObject>();
        CreateMap<GetPlaylistTracksResultPlaylistTrackAlbum, GetPlaylistTracksDTOPlaylistTrackAlbum>();
        CreateMap<GetPlaylistTracksResultPlaylistTrackData, GetPlaylistTracksDTOPlaylistTrackData>();
        CreateMap<GetPlaylistTracksResultPlaylistTrackAddedBy, GetPlaylistTracksDTOPlaylistTrackAddedBy>();
        CreateMap<GetPlaylistTracksResultPlaylistTrack, GetPlaylistTracksDTOPlaylistTrack>();
        CreateMap<GetPlaylistTracksResult, GetPlaylistTracksDTO>();
    }
}

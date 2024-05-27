using AutoMapper;
using PlaylistCleaner.Api.Responses.PlaylistsControllerResponses;
using PlaylistCleaner.ApiClients.Responses.PlaylistsClientResponses.GetPlaylistTracks;

namespace PlaylistCleaner.Api.Profiles;

internal class GetPlaylistTracksProfile : Profile
{
    public GetPlaylistTracksProfile()
    {
        CreateMap<GetPlaylistTracksResultPlaylistTrackArtist, GetPlaylistTracksResponsePlaylistTrackArtist>();
        CreateMap<GetPlaylistTracksResultPlaylistTrackAlbumImageObject, GetPlaylistTracksResponsePlaylistTrackAlbumImageObject>();
        CreateMap<GetPlaylistTracksResultPlaylistTrackAlbum, GetPlaylistTracksResponsePlaylistTrackAlbum>();
        CreateMap<GetPlaylistTracksResultPlaylistTrackData, GetPlaylistTracksResponsePlaylistTrackData>();
        CreateMap<GetPlaylistTracksResultPlaylistTrackAddedBy, GetPlaylistTracksResponsePlaylistTrackAddedBy>();
        CreateMap<GetPlaylistTracksResultPlaylistTrack, GetPlaylistTracksResponsePlaylistTrack>();
        CreateMap<GetPlaylistTracksResult, GetPlaylistTracksResponse>();
    }
}

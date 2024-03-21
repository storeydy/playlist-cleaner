using AutoMapper;
using PlaylistCleaner.Api.Responses;
using PlaylistCleaner.ApiClients.Responses.SpotifyApiClientResults.GetPlaylist;

namespace PlaylistCleaner.Api.Profiles;

internal sealed class GetPlaylistProfile : Profile
{
    public GetPlaylistProfile()
    {
        CreateMap<GetPlaylistResultExternalUrls, GetPlaylistResponseExternalUrls>();
        CreateMap<GetPlaylistResultExternalIds, GetPlaylistResponseExternalIds>();
        CreateMap<GetPlaylistResultImageObject, GetPlaylistResponseImageObject>();
        CreateMap<GetPlaylistResultFollowers, GetPlaylistResponseFollowers>();

        CreateMap<GetPlaylistResultArtist, GetPlaylistResponseArtist>()
            .ForCtorParam(nameof(GetPlaylistResponseArtist.images), opt => opt.MapFrom(s => s.images));
        CreateMap<GetPlaylistResultPlaylistOwner, GetPlaylistResponsePlaylistOwner>()
            .ForCtorParam(nameof(GetPlaylistResponsePlaylistOwner.external_urls), opt => opt.MapFrom(s => s.external_urls))
            .ForCtorParam(nameof(GetPlaylistResponsePlaylistOwner.followers), opt => opt.MapFrom(s => s.followers));

        CreateMap<GetPlaylistResultTrackAddedBy, GetPlaylistResponseTrackAddedBy>()
            .ForCtorParam(nameof(GetPlaylistResponseTrackAddedBy.external_urls), opt => opt.MapFrom(s => s.external_urls))
            .ForCtorParam(nameof(GetPlaylistResponseTrackAddedBy.followers), opt => opt.MapFrom(s => s.followers));

        CreateMap<GetPlaylistResultRestrictions, GetPlaylistResponseRestrictions>();

        CreateMap<GetPlaylistResultTrack, GetPlaylistResponseTrack>()
            .ForCtorParam(nameof(GetPlaylistResponseTrack.added_by), opt => opt.MapFrom(s => s.added_by));

        CreateMap<GetPlaylistResultAlbumArtist, GetPlaylistResponseAlbumArtist>()
            .ForCtorParam(nameof(GetPlaylistResponseAlbumArtist.external_urls), opt => opt.MapFrom(s => s.external_urls));


        CreateMap<GetPlaylistResultAlbum, GetPlaylistResponseAlbum>()
            .ForCtorParam(nameof(GetPlaylistResponseAlbum.external_urls), opt => opt.MapFrom(s => s.external_urls))
            .ForCtorParam(nameof(GetPlaylistResponseAlbum.images), opt => opt.MapFrom(s => s.images))
            .ForCtorParam(nameof(GetPlaylistResponseAlbum.restrictions), opt => opt.MapFrom(s => s.restrictions))
            .ForCtorParam(nameof(GetPlaylistResponseAlbum.artists), opt => opt.MapFrom(s => s.artists));



        CreateMap<GetPlaylistResultTrackData, GetPlaylistResponseTrackData>()
            .ForCtorParam(nameof(GetPlaylistResponseTrackData.album), opt => opt.MapFrom(s => s.album))
            .ForCtorParam(nameof(GetPlaylistResponseTrackData.artists), opt => opt.MapFrom(s => s.artists))
            .ForCtorParam(nameof(GetPlaylistResponseTrackData.external_ids), opt => opt.MapFrom(s => s.external_ids));

        CreateMap<GetPlaylistResult, GetPlaylistResponse>()
            .ForCtorParam(nameof(GetPlaylistResponse.external_urls), opt => opt.MapFrom(s => s.external_urls))
            .ForCtorParam(nameof(GetPlaylistResponse.images), opt => opt.MapFrom(s => s.images))
            .ForCtorParam(nameof(GetPlaylistResponse.owner), opt => opt.MapFrom(s => s.owner))
            .ForCtorParam(nameof(GetPlaylistResponse.tracks), opt => opt.MapFrom(s => s.tracks));
    }
}

using AutoMapper;
using PlaylistCleaner.Api.Responses.PlaylistsControllerResponses;
using PlaylistCleaner.ApiClients.Responses.PlaylistsClientResults.GetPlaylist;

namespace PlaylistCleaner.Api.Profiles;

internal sealed class GetPlaylistProfile : Profile
{
    public GetPlaylistProfile()
    {
        CreateMap<GetPlaylistResultExternalUrls, GetPlaylistResponseExternalUrls>();
        CreateMap<GetPlaylistResultExternalIds, GetPlaylistResponseExternalIds>();
        CreateMap<GetPlaylistResultImageObject, GetPlaylistResponseImageObject>();
        CreateMap<GetPlaylistResultFollowers, GetPlaylistResponseFollowers>();
        CreateMap<GetPlaylistResultPlaylistOwner, GetPlaylistResponsePlaylistOwner>()
            .ForCtorParam(nameof(GetPlaylistResponsePlaylistOwner.external_urls), opt => opt.MapFrom(s => s.external_urls))
            .ForCtorParam(nameof(GetPlaylistResponsePlaylistOwner.followers), opt => opt.MapFrom(s => s.followers));

        CreateMap<GetPlaylistResultTrack, GetPlaylistResponseTrack>();

        CreateMap<GetPlaylistResult, GetPlaylistResponse>()
            .ForCtorParam(nameof(GetPlaylistResponse.external_urls), opt => opt.MapFrom(s => s.external_urls))
            .ForCtorParam(nameof(GetPlaylistResponse.images), opt => opt.MapFrom(s => s.images))
            .ForCtorParam(nameof(GetPlaylistResponse.owner), opt => opt.MapFrom(s => s.owner))
            .ForCtorParam(nameof(GetPlaylistResponse.tracks), opt => opt.MapFrom(s => s.tracks))
            .ForCtorParam(nameof(GetPlaylistResponse.followers), opt => opt.MapFrom(s => s.followers));
    }
}

using AutoMapper;
using PlaylistCleaner.Api.Responses.PlaylistsControllerResponses;
using PlaylistCleaner.Application.Results.Services.PlaylistsServiceResults;

namespace PlaylistCleaner.Api.Profiles.PlaylistsController;

internal sealed class GetPlaylistProfile : Profile
{
    public GetPlaylistProfile()
    {
        CreateMap<GetPlaylistDTOExternalUrls, GetPlaylistResponseExternalUrls>();
        CreateMap<GetPlaylistDTOExternalIds, GetPlaylistResponseExternalIds>();
        CreateMap<GetPlaylistDTOImageObject, GetPlaylistResponseImageObject>();
        CreateMap<GetPlaylistDTOFollowers, GetPlaylistResponseFollowers>();
        CreateMap<GetPlaylistDTOPlaylistOwner, GetPlaylistResponsePlaylistOwner>()
            .ForCtorParam(nameof(GetPlaylistResponsePlaylistOwner.external_urls), opt => opt.MapFrom(s => s.external_urls))
            .ForCtorParam(nameof(GetPlaylistResponsePlaylistOwner.followers), opt => opt.MapFrom(s => s.followers));

        CreateMap<GetPlaylistDTOTrack, GetPlaylistResponseTrack>();

        CreateMap<GetPlaylistDTO, GetPlaylistResponse>()
            .ForCtorParam(nameof(GetPlaylistResponse.external_urls), opt => opt.MapFrom(s => s.external_urls))
            .ForCtorParam(nameof(GetPlaylistResponse.images), opt => opt.MapFrom(s => s.images))
            .ForCtorParam(nameof(GetPlaylistResponse.owner), opt => opt.MapFrom(s => s.owner))
            .ForCtorParam(nameof(GetPlaylistResponse.tracks), opt => opt.MapFrom(s => s.tracks))
            .ForCtorParam(nameof(GetPlaylistResponse.followers), opt => opt.MapFrom(s => s.followers));
    }
}

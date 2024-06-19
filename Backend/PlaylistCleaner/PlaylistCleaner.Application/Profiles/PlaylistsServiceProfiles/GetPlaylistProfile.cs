using AutoMapper;
using PlaylistCleaner.Application.Results.Services.PlaylistsServiceResults;
using PlaylistCleaner.Infrastructure.Results.PlaylistsClientResults;

namespace PlaylistCleaner.Application.Profiles.PlaylistsServiceProfiles;

internal sealed class GetPlaylistProfile : Profile
{
    public GetPlaylistProfile()
    {
        CreateMap<GetPlaylistResultExternalUrls, GetPlaylistDTOExternalUrls>();
        CreateMap<GetPlaylistResultExternalIds, GetPlaylistDTOExternalIds>();
        CreateMap<GetPlaylistResultImageObject, GetPlaylistDTOImageObject>();
        CreateMap<GetPlaylistResultFollowers, GetPlaylistDTOFollowers>();
        CreateMap<GetPlaylistResultPlaylistOwner, GetPlaylistDTOPlaylistOwner>()
            .ForCtorParam(nameof(GetPlaylistDTOPlaylistOwner.external_urls), opt => opt.MapFrom(s => s.external_urls))
            .ForCtorParam(nameof(GetPlaylistDTOPlaylistOwner.followers), opt => opt.MapFrom(s => s.followers));

        CreateMap<GetPlaylistResultTrack, GetPlaylistDTOTrack>();

        CreateMap<GetPlaylistResult, GetPlaylistDTO>()
            .ForCtorParam(nameof(GetPlaylistDTO.external_urls), opt => opt.MapFrom(s => s.external_urls))
            .ForCtorParam(nameof(GetPlaylistDTO.images), opt => opt.MapFrom(s => s.images))
            .ForCtorParam(nameof(GetPlaylistDTO.owner), opt => opt.MapFrom(s => s.owner))
            .ForCtorParam(nameof(GetPlaylistDTO.tracks), opt => opt.MapFrom(s => s.tracks))
            .ForCtorParam(nameof(GetPlaylistDTO.followers), opt => opt.MapFrom(s => s.followers));
    }
}

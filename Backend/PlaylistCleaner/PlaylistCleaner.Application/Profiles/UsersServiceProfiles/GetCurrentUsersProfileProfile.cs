using AutoMapper;
using PlaylistCleaner.Application.Results.Services.UsersServiceResults;
using PlaylistCleaner.Infrastructure.Results.UserProfileClientResults;

namespace PlaylistCleaner.Application.Profiles.UsersServiceProfiles;

internal sealed class GetCurrentUsersProfileProfile : Profile
{
    public GetCurrentUsersProfileProfile()
    {
        CreateMap<ImageObject, GetCurrentUsersProfileDTOImageObject>();

        CreateMap<Followers, GetCurrentUsersProfileDTOFollowers>();

        CreateMap<External_Urls, string>()
            .ConstructUsing(s => s.spotify);

        CreateMap<Explicit_Content, GetCurrentUsersProfileDTOExplicit_Content>();

        CreateMap<GetCurrentUsersProfileResult, GetCurrentUsersProfileDTO>()
        .ForCtorParam(nameof(GetCurrentUsersProfileDTO.explicit_content), opt => opt.MapFrom(s => s.explicit_content))
        .ForCtorParam(nameof(GetCurrentUsersProfileDTO.spotify_external_url), opt => opt.MapFrom(s => s.external_urls))
        .ForCtorParam(nameof(GetCurrentUsersProfileDTO.followers), opt => opt.MapFrom(s => s.followers))
        .ForCtorParam(nameof(GetCurrentUsersProfileDTO.images), opt => opt.MapFrom(s => s.images));
    }
}

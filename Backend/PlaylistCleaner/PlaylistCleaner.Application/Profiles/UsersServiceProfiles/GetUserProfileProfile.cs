using AutoMapper;
using PlaylistCleaner.Application.Results.Services.UsersServiceResults;
using PlaylistCleaner.Infrastructure.Results.UsersClientResults;

namespace PlaylistCleaner.Application.Profiles.UsersServiceProfiles;

internal sealed class GetUserProfileProfile : Profile
{
    public GetUserProfileProfile()
    {
        CreateMap<GetUserProfileResultImage, GetUserProfileDTOImage>();
        CreateMap<GetUserProfileResultFollower, GetUserProfileDTOFollower>();
        CreateMap<GetUserProfileResult, GetUserProfileDTO>();
    }
}

using AutoMapper;
using PlaylistCleaner.Api.Responses.UsersControllerResponses;
using PlaylistCleaner.Application.Results.Services.UsersServiceResults;

namespace PlaylistCleaner.Api.Profiles.UsersControllerProfiles;

internal sealed class GetCurrentUsersProfileProfile : Profile
{
    public GetCurrentUsersProfileProfile()
    {
        CreateMap<GetCurrentUsersProfileDTOImageObject, GetCurrentUsersProfileResponseImage>();

        CreateMap<GetCurrentUsersProfileDTOFollowers, GetCurrentUsersProfileResponseFollower>();

        CreateMap<GetCurrentUsersProfileDTOExplicit_Content, GetCurrentUsersProfileResponseExplicitContent>();

        CreateMap<GetCurrentUsersProfileDTO, GetCurrentUsersProfileResponse>();
    }
}
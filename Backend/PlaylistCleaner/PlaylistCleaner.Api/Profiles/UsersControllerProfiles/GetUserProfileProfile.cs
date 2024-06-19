using AutoMapper;
using PlaylistCleaner.Api.Responses.UsersControllerResponses;
using PlaylistCleaner.Application.Results.Services.UsersServiceResults;

namespace PlaylistCleaner.Api.Profiles.UsersControllerProfiles;

internal sealed class GetUserProfileProfile : Profile
{
    public GetUserProfileProfile()
    {
        CreateMap<GetUserProfileDTO, GetUserProfileResponse>();
    }
}

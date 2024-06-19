using AutoMapper;
using PlaylistCleaner.Api.Responses.UsersControllerResponses;
using PlaylistCleaner.Application.Results.Services.UsersServiceResults;

namespace PlaylistCleaner.Api.Profiles.UsersControllerProfiles;

internal sealed class GetUsersPlaylistsProfile : Profile
{
    public GetUsersPlaylistsProfile()
    {
        CreateMap<GetUserPlaylistsDTO, GetUsersPlaylistsResponse>();
    }
}

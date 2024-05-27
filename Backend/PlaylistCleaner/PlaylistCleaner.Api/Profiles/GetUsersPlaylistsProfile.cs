using AutoMapper;
using PlaylistCleaner.Api.Responses.UsersControllerResponses;
using PlaylistCleaner.ApiClients.Responses.UsersClientResponses.GetUsersPlaylists;

namespace PlaylistCleaner.Api.Profiles;

internal sealed class GetUsersPlaylistsProfile : Profile
{
    public GetUsersPlaylistsProfile()
    {
        CreateMap<SimplifiedPlaylistObject, string>()
            .ConstructUsing(s => s.id);

        CreateMap<GetUsersPlaylistsResult, GetUsersPlaylistsResponse>()
            .ForCtorParam(nameof(GetUsersPlaylistsResponse.playlist_ids), opt => opt.MapFrom(s => s.items));
    }
}

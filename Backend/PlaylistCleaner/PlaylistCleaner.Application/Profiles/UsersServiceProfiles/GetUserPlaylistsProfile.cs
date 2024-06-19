using AutoMapper;
using PlaylistCleaner.Application.Results.Services.UsersServiceResults;
using PlaylistCleaner.Infrastructure.Results.UsersClientResults;

namespace PlaylistCleaner.Application.Profiles.UsersServiceProfiles;

internal sealed class GetUserPlaylistsProfile : Profile
{
    public GetUserPlaylistsProfile()
    {
        CreateMap<SimplifiedPlaylistObject, string>()
            .ConstructUsing(s => s.id);

        CreateMap<GetUsersPlaylistsResult, GetUserPlaylistsDTO>()
            .ForCtorParam(nameof(GetUserPlaylistsDTO.playlist_ids), opt => opt.MapFrom(s => s.items));
    }
}

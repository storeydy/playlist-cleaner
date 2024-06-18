using AutoMapper;
using PlaylistCleaner.Api.Responses.PlaylistsControllerResponses;
using PlaylistCleaner.ApiClients.Results.PlaylistsClientResults.GetPlaylistDuplicates;

namespace PlaylistCleaner.Api.Profiles;

internal class GetPlaylistDuplicatesProfile : Profile
{
    public GetPlaylistDuplicatesProfile()
    {
        CreateMap<GetPlaylistDuplicatesResultDuplicate, GetPlaylistDuplicatesResponseDuplicate>();

        CreateMap<GetPlaylistDuplicatesResult, GetPlaylistDuplicatesResponse>();
    }
}

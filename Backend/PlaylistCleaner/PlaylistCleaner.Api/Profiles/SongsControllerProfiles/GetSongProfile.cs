using AutoMapper;
using PlaylistCleaner.Api.Responses.SongsControllerResponses;
using PlaylistCleaner.Application.Results.Services.SongsService;

namespace PlaylistCleaner.Api.Profiles.SongsControllerProfiles;

public class GetSongProfile : Profile
{
    public GetSongProfile()
    {
        CreateMap<GetSongDTOAlbum, GetSongResponseAlbum>();
        CreateMap<GetSongDTOAlbumArtist, GetSongResponseAlbumArtist>();
        CreateMap<GetSongDTOAlbumImage, GetSongResponseAlbumImage>();
        CreateMap<GetSongDTOArtist, GetSongResponseArtist>();
        CreateMap<GetSongDTOExternalIds, GetSongResponseExternalIds>();
        CreateMap<GetSongDTO, GetSongResponse>();
    }
}

using AutoMapper;
using PlaylistCleaner.Application.Results.Services.SongsService;
using PlaylistCleaner.Infrastructure.Results.SongsClientResults;

namespace PlaylistCleaner.Application.Profiles.SongsServiceProfiles;

internal sealed class GetSongProfile : Profile
{
    public GetSongProfile()
    {
        CreateMap<GetSongResultExternalIds, GetSongDTOExternalIds>();
        CreateMap<GetSongResultRestrictions, string>()
            .ConvertUsing(r => r.reason);
        CreateMap<GetSongResultAlbumRestrictions, string>()
            .ConvertUsing(r => r.reason);
        CreateMap<GetSongResultAlbumArtist, GetSongDTOAlbumArtist>();
        CreateMap<GetSongResultArtist, GetSongDTOArtist>();
        CreateMap<GetSongResultAlbumImage, GetSongDTOAlbumImage>();
        CreateMap<GetSongResultAlbum, GetSongDTOAlbum>();
        CreateMap<GetSongResult, GetSongDTO>()
            .ForCtorParam(nameof(GetSongDTO.restrictions), opt => opt.MapFrom(s => s.restrictions))
            .ForCtorParam(nameof(GetSongDTO.album.restrictions), opt => opt.MapFrom(s => s.album.restrictions));
    }
}

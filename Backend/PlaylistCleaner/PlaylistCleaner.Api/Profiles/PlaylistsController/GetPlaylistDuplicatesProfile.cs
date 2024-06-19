﻿using AutoMapper;
using PlaylistCleaner.Api.Responses.PlaylistsControllerResponses;
using PlaylistCleaner.Application.Results.Utils.DuplicateDetector;

namespace PlaylistCleaner.Api.Profiles.PlaylistsController;

internal class GetPlaylistDuplicatesProfile : Profile
{
    public GetPlaylistDuplicatesProfile()
    {
        CreateMap<GetPlaylistDuplicatesDTODuplicate, GetPlaylistDuplicatesResponseDuplicate>();

        CreateMap<GetPlaylistDuplicatesDTO, GetPlaylistDuplicatesResponse>();
    }
}

﻿namespace PlaylistCleaner.Application.Results.Utils.DuplicateDetector;

public sealed record GetPlaylistDuplicatesDTO(ICollection<GetPlaylistDuplicatesDTODuplicate> duplicateTrackSets);

public sealed record GetPlaylistDuplicatesDTODuplicate(ICollection<string> duplicateTrackIds);
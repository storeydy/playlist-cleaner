﻿namespace PlaylistCleaner.ApiClients.Results.PlaylistsClientResults.GetPlaylistDuplicates;

public sealed record GetPlaylistDuplicatesResult(ICollection<GetPlaylistDuplicatesResultDuplicate> duplicateTrackSets);

public sealed record GetPlaylistDuplicatesResultDuplicate(ICollection<string> duplicateTrackIds);
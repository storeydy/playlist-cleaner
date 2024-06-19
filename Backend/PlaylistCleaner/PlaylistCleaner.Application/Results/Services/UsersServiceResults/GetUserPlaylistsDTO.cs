namespace PlaylistCleaner.Application.Results.Services.UsersServiceResults;

public sealed record GetUserPlaylistsDTO(ICollection<string> playlist_ids);
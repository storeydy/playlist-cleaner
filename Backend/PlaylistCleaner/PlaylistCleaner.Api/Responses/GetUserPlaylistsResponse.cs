namespace PlaylistCleaner.Api.Responses;

public sealed record GetUsersPlaylistsResponse(ICollection<string> playlist_ids);
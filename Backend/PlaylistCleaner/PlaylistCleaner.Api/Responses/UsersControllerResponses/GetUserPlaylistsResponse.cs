namespace PlaylistCleaner.Api.Responses.UsersControllerResponses;

public sealed record GetUsersPlaylistsResponse(ICollection<string> playlist_ids);
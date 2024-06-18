namespace PlaylistCleaner.ApiClients.Responses.UsersClientResults.GetUsersPlaylists;

public sealed record GetUsersPlaylistsResult(string next, List<SimplifiedPlaylistObject> items);

public sealed record SimplifiedPlaylistObject(string id);
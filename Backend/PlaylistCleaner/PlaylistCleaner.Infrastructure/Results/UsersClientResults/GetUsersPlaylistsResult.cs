namespace PlaylistCleaner.Infrastructure.Results.UsersClientResults;

public sealed record GetUsersPlaylistsResult(string next, List<SimplifiedPlaylistObject> items);

public sealed record SimplifiedPlaylistObject(string id);
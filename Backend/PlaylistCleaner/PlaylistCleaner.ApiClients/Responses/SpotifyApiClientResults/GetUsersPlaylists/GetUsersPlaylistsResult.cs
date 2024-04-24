namespace PlaylistCleaner.ApiClients.Responses.SpotifyApiClientResults.GetUsersPlaylists;

public sealed record GetUsersPlaylistsResult(string href, int limit, string next, int offset, string previous, int total, IList<SimplifiedPlaylistObject> items);

public sealed record SimplifiedPlaylistObject(string id);
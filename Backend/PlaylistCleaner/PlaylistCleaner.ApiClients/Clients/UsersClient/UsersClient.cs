using PlaylistCleaner.ApiClients.Responses.UsersClientResponses.GetUserProfile;
using PlaylistCleaner.ApiClients.Responses.UsersClientResponses.GetUsersPlaylists;
using System.Net.Http.Json;

namespace PlaylistCleaner.ApiClients.Clients.UsersClient;

internal sealed class UsersClient : IUsersClient
{
    private readonly HttpClient _httpClient;

    public UsersClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<GetUserProfileResult> GetUserProfileAsync(string userId, CancellationToken cancellationToken = default)
    {
        var result = await _httpClient.GetFromJsonAsync<GetUserProfileResult>(userId, cancellationToken);
        return result;
    }

    public async Task<GetUsersPlaylistsResult> GetUserPlaylistsAsync(string userId, CancellationToken cancellationToken = default)
    {
        int offsetValue = 50;
        var requestUri = $"{userId}/playlists?limit=50";

        var jsonParsed = await _httpClient.GetFromJsonAsync<GetUsersPlaylistsResult>(requestUri, cancellationToken);
        bool isAnotherPageOfResults = jsonParsed.next == null ? false : true;

        while (isAnotherPageOfResults)
        {
            string offsetParameter = $"&offset={offsetValue}";
            var nextPageOfPlaylists = await _httpClient.GetFromJsonAsync<GetUsersPlaylistsResult>(requestUri + offsetParameter, cancellationToken);
            jsonParsed.items.AddRange(nextPageOfPlaylists.items);
            if (nextPageOfPlaylists.next == null)
            {
                isAnotherPageOfResults = false;
            }
            offsetValue += 50;
        }

        return jsonParsed;
    }

}

using PlaylistCleaner.ApiClients.Responses.SpotifyApiClientResults.GetCurrentUsersProfile;
using System.Net.Http.Json;

namespace PlaylistCleaner.ApiClients.Clients.UserProfileClient;

internal sealed class UserProfileClient : IUserProfileClient
{
    private readonly HttpClient _httpClient;

    public UserProfileClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<GetCurrentUsersProfileResult> GetCurrentUsersProfileAsync(CancellationToken cancellationToken = default)
    {
        
        var response = await _httpClient.GetFromJsonAsync<GetCurrentUsersProfileResult>("me", cancellationToken);

        return response;
    }
}

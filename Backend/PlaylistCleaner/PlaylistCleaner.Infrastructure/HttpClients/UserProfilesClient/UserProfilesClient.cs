using PlaylistCleaner.Infrastructure.Results.UserProfileClientResults;
using System.Net.Http.Json;

namespace PlaylistCleaner.Infrastructure.Clients.UserProfilesClient;

internal sealed class UserProfilesClient : IUserProfilesClient
{
    private readonly HttpClient _httpClient;

    public UserProfilesClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<GetCurrentUsersProfileResult> GetCurrentUsersProfileAsync(CancellationToken cancellationToken = default)
    {
        
        var response = await _httpClient.GetFromJsonAsync<GetCurrentUsersProfileResult>("me", cancellationToken);

        return response;
    }
}

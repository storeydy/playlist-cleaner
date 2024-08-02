using PlaylistCleaner.Infrastructure.Exceptions;
using PlaylistCleaner.Infrastructure.Results.UserProfileClientResults;
using System.Net;
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
        var profileRequestUri = "me";
        try
        {
            var response = await _httpClient.GetAsync(profileRequestUri, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                throw new SpotifyApiHttpException(response.StatusCode + content);
            }

            var profile = await response.Content.ReadFromJsonAsync<GetCurrentUsersProfileResult>(cancellationToken: cancellationToken);
            if (profile == null)
            {
                throw new EntityNotFoundException(HttpStatusCode.NoContent + ", Profile not found or response body is empty.");
            }

            return profile;
        }
        catch (HttpRequestException ex)
        {
            throw new SpotifyApiHttpException(HttpStatusCode.ServiceUnavailable + ", An error occurred while sending the request. Exception " + ex);
        }
    }
}

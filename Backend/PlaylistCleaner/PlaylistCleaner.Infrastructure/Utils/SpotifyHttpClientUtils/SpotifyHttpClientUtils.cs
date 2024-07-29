using PlaylistCleaner.Infrastructure.Exceptions;
using System.Net;
using System.Net.Http.Json;

namespace PlaylistCleaner.Infrastructure.Utils.SpotifyHttpClientUtils;

internal sealed class SpotifyHttpClientUtils
{
    private readonly HttpClient _httpClient;

    public SpotifyHttpClientUtils(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<TItem>> FetchAllItemsAsync<TResponse, TItem>(string initialRequestUri, Func<TResponse, List<TItem>> getItems, Func<TResponse, string?> getNextPage, CancellationToken cancellationToken = default)
    {
        var allItems = new List<TItem>();
        string? nextPage = initialRequestUri;

        do
        {
            var response = await FetchPageAsync<TResponse>(nextPage, cancellationToken);
            var items = getItems(response);
            allItems.AddRange(items);
            nextPage = getNextPage(response);
        } while (!string.IsNullOrEmpty(nextPage));

        return allItems;
    }

    private async Task<TResponse> FetchPageAsync<TResponse>(string requestUri, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _httpClient.GetAsync(requestUri, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                throw new SpotifyApiHttpException(response.StatusCode + content);
            }

            var result = await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken);
            return result ?? throw new SpotifyApiHttpException(HttpStatusCode.NoContent + ", Response body is null or empty.");
        }
        catch (HttpRequestException ex)
        {
            throw new SpotifyApiHttpException(HttpStatusCode.ServiceUnavailable + ", An error occurred while sending the request. Exception: " + ex);
        }
    }
}

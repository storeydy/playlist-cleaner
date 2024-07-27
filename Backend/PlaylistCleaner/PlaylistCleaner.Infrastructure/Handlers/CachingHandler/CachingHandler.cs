using Microsoft.Extensions.Caching.Memory;

namespace PlaylistCleaner.Infrastructure.Handlers.CachingHandler;

internal class CachingHandler : DelegatingHandler
{
    private readonly IMemoryCache _memoryCache;
    private readonly long _maxCacheableResponseInBytes;

    public CachingHandler(IMemoryCache memoryCache, long maxCacheableResponseInBytes)
    {
        _memoryCache = memoryCache;
        _maxCacheableResponseInBytes = maxCacheableResponseInBytes;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (request.Method == HttpMethod.Get)
        {
            var cacheKey = request.RequestUri.ToString();

            if (_memoryCache.TryGetValue(cacheKey, out HttpResponseMessage cachedResponse))
            {
                return cachedResponse;
            }

            var response = await base.SendAsync(request, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var responseSize = await GetResponseSizeInBytesAsync(response);
                if (responseSize <= _maxCacheableResponseInBytes)
                {
                    _memoryCache.Set(cacheKey, response, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
                        Size = responseSize
                    });
                }

            }

            return response;
        }

        return await base.SendAsync(request, cancellationToken);
    }

    private async Task<long> GetResponseSizeInBytesAsync(HttpResponseMessage response)
    {
        var responseContent = await response.Content.ReadAsByteArrayAsync();
        return responseContent.Length;
    }
}

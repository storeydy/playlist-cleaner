using Microsoft.Extensions.Caching.Memory;
using System.Net;

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

            if (_memoryCache.TryGetValue(cacheKey, out CachedContent cachedContent))
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(cachedContent.Content)
                };

                foreach (var header in cachedContent.Headers)
                {
                    response.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }

                foreach (var contentHeader in cachedContent.ContentHeaders)
                {
                    response.Content.Headers.TryAddWithoutValidation(contentHeader.Key, contentHeader.Value);
                }

                return response;
            }

            var responseFromServer = await base.SendAsync(request, cancellationToken);

            if (responseFromServer.IsSuccessStatusCode)
            {
                var responseContent = await responseFromServer.Content.ReadAsByteArrayAsync();
                var responseSize = responseContent.Length;

                if (responseSize <= _maxCacheableResponseInBytes)
                {
                    cachedContent = new CachedContent
                    {
                        Content = responseContent,
                        Headers = responseFromServer.Headers.ToDictionary(h => h.Key, h => h.Value.ToList()),
                        ContentHeaders = responseFromServer.Content.Headers.ToDictionary(h => h.Key, h => h.Value.ToList())
                    };

                    _memoryCache.Set(cacheKey, cachedContent, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
                        Size = responseSize
                    });
                }

            }

            return responseFromServer;
        }

        return await base.SendAsync(request, cancellationToken);
    }

    private class CachedContent
    {
        public byte[] Content { get; set; }
        public Dictionary<string, List<string>> Headers { get; set; }
        public Dictionary<string, List<string>> ContentHeaders { get; set; }
    }
}

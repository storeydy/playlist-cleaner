using PlaylistCleaner.Api.Exceptions;

namespace PlaylistCleaner.Api.Extensions;

internal static class TokenExtensions
{
    internal static string ExtractTokenFromHeaders(this IHeaderDictionary httpHeaders)
    {
        string? token = null;
        string? authHeader = httpHeaders.Authorization;
        if (authHeader != null)
        {
            token = authHeader.Replace("Bearer ", "");
        }

        if (token == null)
        {
            throw new TokenNotFoundException("No access token found in headers of HTTP request");
        };

        return token;

    }
}

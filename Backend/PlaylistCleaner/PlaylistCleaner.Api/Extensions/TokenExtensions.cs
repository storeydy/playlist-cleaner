using PlaylistCleaner.Api.Exceptions;

namespace PlaylistCleaner.Api.Extensions;

internal static class TokenExtensions
{
    internal static string ExtractTokenFromHeaders(this IHeaderDictionary httpHeaders)
    {
        try
        {
            string authHeader = httpHeaders.Authorization;
            string token = authHeader.Replace("Bearer ", "");
            return token;
        }
        catch(NullReferenceException)
        {
            throw new TokenNotFoundException("No token found in headers of HTTP request");
        }


    }
}

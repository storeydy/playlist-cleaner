using PlaylistCleaner.ApiClients.Exceptions;

namespace PlaylistCleaner.ApiClients.Handlers.AuthorizationHandler;

internal class AuthorizationHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
    HttpRequestMessage request,
    CancellationToken cancellationToken)
    {
        if (request.Headers.Authorization == null)
        {
            throw new UnauthorizedAccessException("No Authorization header present.");
        }

        if (!request.Headers.Authorization.Scheme.Equals("Bearer", StringComparison.OrdinalIgnoreCase))
        {
            throw new TokenNotFoundException("Bearer token missing in Authorization header.");
        }

        return await base.SendAsync(request, cancellationToken);
    }
}

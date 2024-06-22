using PlaylistCleaner.Infrastructure.Exceptions;
using Microsoft.Extensions.Logging;

namespace PlaylistCleaner.Infrastructure.Handlers.AuthorizationHandler;

internal class AuthorizationHandler : DelegatingHandler
{
    private readonly ILogger _logger;
    public AuthorizationHandler(ILoggerFactory logger)
    {
        _logger = logger.CreateLogger<AuthorizationHandler>();
    }
    protected override async Task<HttpResponseMessage> SendAsync(
    HttpRequestMessage request,
    CancellationToken cancellationToken)
    {
        if (request.Headers.Authorization == null)
        {
            _logger.LogError("No Authorization header present. Request headers: ${headers}", request.Headers);
            throw new UnauthorizedAccessException("No Authorization header present.");
        }

        if (!request.Headers.Authorization.Scheme.Equals("Bearer", StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogError("Bearer token missing in Authorization header. Request headers: ${headers}", request.Headers);
            throw new TokenNotFoundException("Bearer token missing in Authorization header.");
        }

        return await base.SendAsync(request, cancellationToken);
    }
}

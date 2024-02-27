using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics;

namespace PlaylistCleaner.Api.HealthChecks;

internal sealed class VersionHealthCheck : IHealthCheck
{
    private static string _version = GetVersion();

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var data = new Dictionary<string, object> { { "version", _version } };

        return Task.FromResult(HealthCheckResult.Healthy(null, data));
    }

    private static string GetVersion()
    {
        var fileVersionInfo = FileVersionInfo.GetVersionInfo(typeof(Program).Assembly.Location);
        var version = fileVersionInfo.ProductVersion;

        return version ?? string.Empty;
    }
}

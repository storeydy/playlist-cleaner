using Hellang.Middleware.ProblemDetails;
using PlaylistCleaner.Infrastructure.Exceptions;
using PlaylistCleaner.Application.Extensions;
using PlaylistCleaner.Infrastructure.Extensions;

namespace PlaylistCleaner.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddControllers()
            .AddApplicationPart(typeof(ServiceCollectionExtensions).Assembly);
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddProblemDetails(o =>
        {
            o.MapToStatusCode<UnauthorizedAccessException>(StatusCodes.Status401Unauthorized);
            o.MapToStatusCode<TokenNotFoundException>(StatusCodes.Status401Unauthorized);
        });

        services.AddApplicationDependencies();

        services.AddInfrastructureDependencies();

        return services;
    }
}

using AutoMapper;
using PlaylistCleaner.ApiClients.Clients.SpotifyApiClient;

namespace PlaylistCleaner.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddControllers()
            .AddApplicationPart(typeof(ServiceCollectionExtensions).Assembly);
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        
        services.AddHttpClient<ISpotifyApiClient, SpotifyApiClient>();

        return services;
    }
}

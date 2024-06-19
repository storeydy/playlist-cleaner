using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlaylistCleaner.Infrastructure.Clients.PlaylistClient;
using PlaylistCleaner.Infrastructure.Clients.UserProfilesClient;
using PlaylistCleaner.Infrastructure.Clients.UsersClient;
using PlaylistCleaner.Infrastructure.Handlers.AuthorizationHandler;
using Polly;
using Polly.Extensions.Http;

namespace PlaylistCleaner.Infrastructure.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
    {
        services.AddHttpClient<IUsersClient, UsersClient>(o =>
        {
            o.BaseAddress = new Uri("https://api.spotify.com/v1/users/");
        })
            .AddHeaderPropagation()
            .AddHttpMessageHandler<AuthorizationHandler>();

        services.AddHttpClient<IUserProfilesClient, UserProfilesClient>(o =>
        {
            o.BaseAddress = new Uri("https://api.spotify.com/v1/");
        })
            .AddHeaderPropagation()
            .AddHttpMessageHandler<AuthorizationHandler>();

        services.AddHttpClient<IPlaylistsClient, PlaylistsClient>(o =>
        {
            o.BaseAddress = new Uri("https://api.spotify.com/v1/playlists/");
        })
            .AddPolicyHandler(GetRetryPolicy())
            .AddHeaderPropagation()
            .AddHttpMessageHandler<AuthorizationHandler>();

        services.AddHeaderPropagation(o => o.Headers.Add("Authorization"));

        services.AddTransient<AuthorizationHandler>();

        return services;
    }

    static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PlaylistCleaner.Infrastructure.Clients.PlaylistClient;
using PlaylistCleaner.Infrastructure.Clients.UserProfilesClient;
using PlaylistCleaner.Infrastructure.Clients.UsersClient;
using PlaylistCleaner.Infrastructure.Handlers.AuthorizationHandler;
using PlaylistCleaner.Infrastructure.Handlers.CachingHandler;
using PlaylistCleaner.Infrastructure.HttpClients.SongsClient;
using Polly;
using Polly.Extensions.Http;
using Serilog;
using System;
using System.Net;

namespace PlaylistCleaner.Infrastructure.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
    {
        services.AddHttpClient<IUsersClient, UsersClient>(o =>
        {
            o.BaseAddress = new Uri("https://api.spotify.com/v1/users/");
        })
            .AddPolicyHandler(GetRetryPolicy())
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

        services.AddHttpClient<ISongsClient, SongsClient>(o =>
        {
            o.BaseAddress = new Uri("https://api.spotify.com/v1/tracks/");
        })
            .AddHeaderPropagation()
            .AddHttpMessageHandler<CachingHandler>()
            .AddHttpMessageHandler<AuthorizationHandler>();

        services.AddMemoryCache(options =>
        {
            options.SizeLimit = 1024 * 1024 * 50;
        });

        services.AddHeaderPropagation(o => o.Headers.Add("Authorization"));

        services.AddTransient<CachingHandler>(options =>
        {
            var cache = options.GetRequiredService<IMemoryCache>();
            return new CachingHandler(cache, 1024 * 1024);
        });
        services.AddTransient<AuthorizationHandler>();

        return services;
    }

    static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        int maxRetries = 5;
        var maxRetryDelay = TimeSpan.FromSeconds(30);

        return Policy
            .HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.TooManyRequests && r.Headers.RetryAfter != null)
            .WaitAndRetryAsync(
            maxRetries,
            retryAttempt =>
            {
                var jitter = TimeSpan.FromMilliseconds(new Random().Next(0, 1000));
                var retryAfter = retryAttempt <= maxRetries ? TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)) + jitter : maxRetryDelay;
                return retryAfter;
            },
            (result, timeSpan, retryCount, context) =>
            {
                Log.Information($"Retry {retryCount} for {context.PolicyKey} at {context.OperationKey}, " +
                                    $"due to {result.Result.StatusCode} - {result.Result.ReasonPhrase}. " +
                                    $"Waiting {timeSpan.TotalSeconds} seconds before next retry.");
            });
    }
}

using Hellang.Middleware.ProblemDetails;
using PlaylistCleaner.ApiClients.Clients.UserProfileClient;
using PlaylistCleaner.ApiClients.Handlers.AuthorizationHandler;
using PlaylistCleaner.ApiClients.Exceptions;
using PlaylistCleaner.ApiClients.Clients.PlaylistClient;
using PlaylistCleaner.ApiClients.Clients.UsersClient;
using Polly;
using Polly.Extensions.Http;

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
            o.OnBeforeWriteDetails = (_, details) => details.Type = null;
            o.MapToStatusCode<TokenNotFoundException>(StatusCodes.Status401Unauthorized);
        });

        services.AddHeaderPropagation(o => o.Headers.Add("Authorization"));

        services.AddTransient<AuthorizationHandler>();

        services.AddHttpClient<IUsersClient, UsersClient>(o =>
        {
            o.BaseAddress = new Uri("https://api.spotify.com/v1/users/");
        })
            .AddHeaderPropagation()
            .AddHttpMessageHandler<AuthorizationHandler>();

        services.AddHttpClient<IUserProfileClient, UserProfileClient>(o =>
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


        return services;
    }

    static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,
                                                                        retryAttempt)));
    }
}

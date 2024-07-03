using Microsoft.Extensions.DependencyInjection;
using PlaylistCleaner.Application.Services.PlaylistsService;
using PlaylistCleaner.Application.Services.SongsService;
using PlaylistCleaner.Application.Services.UsersService;
using PlaylistCleaner.Application.Utils.DuplicateDetector;

namespace PlaylistCleaner.Application.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddTransient<IPlaylistsService, PlaylistsService>();
        services.AddTransient<IUsersService, UsersService>();
        services.AddTransient<ISongsService, SongsService>();
        services.AddTransient<IDuplicateDetector, DuplicateDetector>();

        return services;
    }
}

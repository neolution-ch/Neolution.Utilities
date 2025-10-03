namespace Neolution.Utilities.AspNetCore.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// The IServiceCollection extension methods.
/// </summary>
public static class IServiceCollectionExtensions
{
    /// <summary>
    /// Adds the strongly typed options.
    /// </summary>
    /// <typeparam name="TOptions">The type of the options.</typeparam>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    public static void AddOptions<TOptions>(this IServiceCollection services, IConfiguration configuration)
        where TOptions : class
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);
        services.Configure<TOptions>(configuration.GetSection<TOptions>());
    }
}

namespace Neolution.Utilities.Extensions;

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
    /// <param name="serviceCollection">The service collection.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The service collection</returns>
    public static IServiceCollection AddOptions<TOptions>(this IServiceCollection serviceCollection, IConfiguration configuration)
        where TOptions : class
    {
        ArgumentNullException.ThrowIfNull(serviceCollection);
        ArgumentNullException.ThrowIfNull(configuration);
        serviceCollection.Configure<TOptions>(configuration.GetSection<TOptions>());
        return serviceCollection;
    }
}

namespace Neolution.Utilities.AspNetCore.Extensions;

using Microsoft.Extensions.Configuration;

/// <summary>
/// The IConfiguration extension methods.
/// </summary>
public static class IConfigurationExtensions
{
    /// <summary>
    /// Gets the strongly typed options.
    /// </summary>
    /// <typeparam name="TOptions">The options type.</typeparam>
    /// <param name="config">The configuration.</param>
    /// <returns>The options.</returns>
    public static TOptions GetOptions<TOptions>(this IConfiguration config)
    {
        ArgumentNullException.ThrowIfNull(config);
        return config.GetSection(typeof(TOptions).Name).Get<TOptions>()
            ?? throw new InvalidOperationException($"Could not find configuration section '{typeof(TOptions).Name}'");
    }

    /// <summary>
    /// Gets the section by the specified options type.
    /// </summary>
    /// <typeparam name="T">The options type.</typeparam>
    /// <param name="config">The configuration.</param>
    /// <returns>The configuration section.</returns>
    public static IConfigurationSection GetSection<T>(this IConfiguration config)
    {
        ArgumentNullException.ThrowIfNull(config);
        return config.GetSection(typeof(T).Name);
    }
}

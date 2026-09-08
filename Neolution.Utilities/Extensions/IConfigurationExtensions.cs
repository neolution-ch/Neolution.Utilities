namespace Neolution.Utilities.Extensions;

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
        var section = config.GetSection(typeof(TOptions).Name);
        if (!section.Exists())
        {
            throw new InvalidOperationException($"Could not find configuration section '{typeof(TOptions).Name}'");
        }

        return section.Get<TOptions>() ?? throw new InvalidOperationException($"Could not create '{typeof(TOptions).Name}'");
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

    /// <summary>
    /// Gets the value with the specified key.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <typeparam name="TEnum">The type of the enum.</typeparam>
    /// <param name="config">The configuration.</param>
    /// <param name="key">The key.</param>
    /// <returns>The value.</returns>
    public static T? GetValue<T, TEnum>(this IConfiguration config, TEnum key)
        where TEnum : Enum
    {
        ArgumentNullException.ThrowIfNull(config);
        return config.GetValue<T>(key.ToString());
    }

    /// <summary>
    /// Gets the value with the specified key.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <typeparam name="TEnum">The type of the enum.</typeparam>
    /// <param name="config">The configuration.</param>
    /// <param name="key">The key.</param>
    /// <param name="defaultValue">The default value to use if no value is found.</param>
    /// <returns>The value.</returns>
    public static T? GetValue<T, TEnum>(this IConfiguration config, TEnum key, T defaultValue)
        where TEnum : Enum
    {
        ArgumentNullException.ThrowIfNull(config);
        return config.GetValue<T>(key.ToString(), defaultValue);
    }
}

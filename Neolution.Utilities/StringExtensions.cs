namespace Neolution.Utilities;

/// <summary>
/// String extensions.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Truncates the string to the specified maximum length.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="maxLength">The maximum length.</param>
    /// <returns>The truncated string</returns>
    public static string? Truncate(this string? value, int maxLength)
    {
        return Truncate(value, maxLength, string.Empty);
    }

    /// <summary>
    /// Truncates the string to the specified maximum length, appending a suffix if necessary.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="maxLength">The maximum length.</param>
    /// <param name="suffix">The suffix to append if the string is truncated.</param>
    /// <returns>The truncated string</returns>
    public static string? Truncate(this string? value, int maxLength, string suffix)
    {
        if (string.IsNullOrEmpty(value) || value.Length <= maxLength)
        {
            return value;
        }

        return $"{value[..maxLength]}{suffix}";
    }
}

namespace Neolution.Utilities.Extensions;

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
        return value.Truncate(maxLength, string.Empty);
    }

    /// <summary>
    /// Truncates the string to the specified maximum length, appending a suffix if necessary.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="maxLength">The maximum length.</param>
    /// <param name="suffix">The suffix to append if the string is truncated. The suffix length must not exceed <paramref name="maxLength"/>.</param>
    /// <returns>The truncated string.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="maxLength"/> is negative.</exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="suffix"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="suffix"/> is longer than <paramref name="maxLength"/>.</exception>
    public static string? Truncate(this string? value, int maxLength, string suffix)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(maxLength);
        ArgumentNullException.ThrowIfNull(suffix);

        if (suffix.Length > maxLength)
        {
            throw new ArgumentException("Suffix length must not exceed maxLength.", nameof(suffix));
        }

        if (string.IsNullOrEmpty(value) || value.Length <= maxLength)
        {
            return value;
        }

        return $"{value[..(maxLength - suffix.Length)]}{suffix}";
    }
}

namespace Neolution.Utilities.Extensions;

using System.Text;

/// <summary>
/// StringBuilder Extensions
/// </summary>
public static class StringBuilderExtensions
{
    /// <summary>
    /// Appends the line with the specified padding
    /// </summary>
    /// <param name="stringBuilder">The string builder</param>
    /// <param name="value">The value</param>
    /// <param name="padding">The padding</param>
    /// <returns>A reference to this instance after the append operation has completed.</returns>
    public static StringBuilder AppendLine(this StringBuilder stringBuilder, string? value, int padding)
    {
        ArgumentNullException.ThrowIfNull(stringBuilder);
        ArgumentOutOfRangeException.ThrowIfNegative(padding);
        return stringBuilder.Append(' ', padding).AppendLine(value);
    }
}

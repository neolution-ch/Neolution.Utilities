namespace Neolution.Utilities.UnitTests;

using Xunit;

/// <summary>
/// Unit tests for <see cref="StringExtensions"/>.
/// </summary>
public class StringExtensionsTests
{
    /// <summary>
    /// Truncate returns the expected result.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="maxLength">The maximum length.</param>
    /// <param name="expected">The expected.</param>
    [Theory]
    [InlineData(null, 5, null)]
    [InlineData("", 5, "")]
    [InlineData("abc", 5, "abc")]
    [InlineData("abcdef", 3, "abc")]
    [InlineData("abcdef", 6, "abcdef")]
    public void Truncate_ReturnsExpectedResult(string? value, int maxLength, string? expected)
    {
        // Act
        var result = value.Truncate(maxLength);

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Truncate with suffix returns the expected result.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="maxLength">The maximum length.</param>
    /// <param name="suffix">The suffix.</param>
    /// <param name="expected">The expected.</param>
    [Theory]
    [InlineData(null, 5, "...", null)]
    [InlineData("", 5, "...", "")]
    [InlineData("abc", 5, "...", "abc")]
    [InlineData("abcdef", 3, "...", "abc...")]
    [InlineData("abcdef", 6, "...", "abcdef")]
    [InlineData("abcdef", 4, "-", "abcd-")]
    [InlineData("abcdef", 0, "!", "!")]
    public void Truncate_WithSuffix_ReturnsExpectedResult(string? value, int maxLength, string suffix, string? expected)
    {
        // Act
        var result = value.Truncate(maxLength, suffix);

        // Assert
        Assert.Equal(expected, result);
    }
}

namespace Neolution.Utilities.UnitTests.Extensions;

using Neolution.Utilities.Extensions;
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
    /// Truncate with suffix returns the expected result when suffix length does not exceed maxLength.
    /// maxLength represents the total length of the resulting string.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="maxLength">The maximum (final) length.</param>
    /// <param name="suffix">The suffix.</param>
    /// <param name="expected">The expected.</param>
    [Theory]
    [InlineData(null, 5, "...", null)]
    [InlineData("", 5, "...", "")]
    [InlineData("abc", 5, "...", "abc")] // no truncation because length <= maxLength
    [InlineData("abcdef", 6, "...", "abcdef")] // no truncation
    [InlineData("abcdef", 3, "...", "...")] // suffix length == maxLength => only suffix
    [InlineData("abcdef", 4, "-", "abc-")] // keep 3 chars + '-'
    [InlineData("abcdef", 4, "", "abcd")] // empty suffix behaves like plain truncate
    [InlineData("abcdef", 0, "", "")] // zero length with empty suffix
    public void Truncate_WithSuffix_ReturnsExpectedResult(string? value, int maxLength, string suffix, string? expected)
    {
        // Act
        var result = value.Truncate(maxLength, suffix);

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Truncating with a negative maxLength throws an <see cref="ArgumentOutOfRangeException"/>.
    /// </summary>
    [Fact]
    public void Truncate_NegativeMaxLength_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => "abc".Truncate(-1));
        Assert.Throws<ArgumentOutOfRangeException>(() => "abc".Truncate(-5, ".."));
    }

    /// <summary>
    /// Truncating with a null suffix throws an <see cref="ArgumentNullException"/>.
    /// </summary>
    [Fact]
    public void Truncate_NullSuffix_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => "abc".Truncate(2, null!));
    }

    /// <summary>
    /// Truncating with a suffix longer than maxLength throws an <see cref="ArgumentException"/>.
    /// </summary>
    [Fact]
    public void Truncate_SuffixLongerThanMaxLength_Throws()
    {
        Assert.Throws<ArgumentException>(() => "abcdef".Truncate(3, "...."));
        Assert.Throws<ArgumentException>(() => "abcdef".Truncate(0, "."));
        Assert.Throws<ArgumentException>(() => "abcdef".Truncate(2, "..."));
    }

    /// <summary>
    /// When the value length equals maxLength and a suffix is provided (shorter than maxLength), the value is returned unchanged.
    /// </summary>
    [Fact]
    public void Truncate_ValueLengthEqualsMaxLength_WithSuffix_ReturnsUnchanged()
    {
        var value = "12345";
        var result = value.Truncate(5, "...");
        Assert.Equal("12345", result);
    }

    /// <summary>
    /// Result equals the suffix when suffix length == maxLength.
    /// </summary>
    [Fact]
    public void Truncate_SuffixLengthEqualsMaxLength_ReturnsOnlySuffix()
    {
        var result = "abcdef".Truncate(3, "...");
        Assert.Equal("...", result);
    }

    /// <summary>
    /// Truncation works with Unicode surrogate pairs (keeping whole emoji, not splitting) when maxLength accommodates suffix + complete emoji.
    /// </summary>
    [Fact]
    public void Truncate_UnicodeCharacters()
    {
        var value = "😀😃😄😁"; // 4 emoji (each surrogate pair)
        var result = value.Truncate(5, "..."); // keep one emoji (2 code units) + '...'
        Assert.Equal("😀...", result);
    }

    /// <summary>
    /// Truncation of a long string returns the expected prefix plus suffix with total length == maxLength.
    /// </summary>
    [Fact]
    public void Truncate_LongString()
    {
        var value = new string('x', 1000);
        var result = value.Truncate(10, "..."); // keep 7 + '...'
        Assert.Equal(new string('x', 7) + "...", result);
        Assert.Equal(10, result!.Length);
    }
}

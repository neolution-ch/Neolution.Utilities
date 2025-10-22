namespace Neolution.Utilities.UnitTests.Extensions;

using System.Text;

/// <summary>
/// Unit tests for the <see cref="StringBuilderExtensions"/> class.
/// </summary>
public class StringBuilderExtensionsTests
{
    /// <summary>
    /// Test that given the null string builder when AppendLine called then throws argument null exception.
    /// </summary>
    [Fact]
    public void GivenNullStringBuilder_WhenAppendLineCalled_ThenThrowsArgumentNullException()
    {
        // Arrange
        StringBuilder? sb = null;

        // Act
        var act = () => sb!.AppendLine("value", 0);

        // Assert
        var ex = Should.Throw<ArgumentNullException>(act);
        ex.ParamName.ShouldBe("stringBuilder");
    }

    /// <summary>
    /// Test that given the negative padding when AppendLine called then throws argument out of range exception.
    /// </summary>
    [Fact]
    public void GivenNegativePadding_WhenAppendLineCalled_ThenThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var sb = new StringBuilder();

        // Act
        var act = () => sb.AppendLine("value", -1);

        // Assert
        var ex = Should.Throw<ArgumentOutOfRangeException>(act);
        ex.ParamName.ShouldBe("padding");
    }

    /// <summary>
    /// Test that given value and padding when AppendLine called then appends expected padded line.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="padding">The padding.</param>
    [Theory]
    [InlineData("Test", 0)]
    [InlineData("Hello", 4)]
    [InlineData("", 3)]
    [InlineData(null, 2)]
    public void GivenValueAndPadding_WhenAppendLineCalled_ThenAppendsWithPadding(string? value, int padding)
    {
        // Arrange
        var sb = new StringBuilder();

        // Act
        sb.AppendLine(value, padding);

        // Assert
        var expected = new string(' ', padding) + (value ?? string.Empty) + Environment.NewLine;
        sb.ToString().ShouldBe(expected);
    }

    /// <summary>
    /// Test that given multiple calls when AppendLine called then appends sequentially and returns same instance.
    /// </summary>
    [Fact]
    public void GivenMultipleCalls_WhenAppendLineCalled_ThenAppendsSequentiallyAndReturnsSameInstance()
    {
        // Arrange
        var sb = new StringBuilder();

        // Act
        var returned1 = sb.AppendLine("First", 1);
        var returned2 = sb.AppendLine("Second", 2);
        var returned3 = sb.AppendLine(null, 0);

        // Assert
        returned1.ShouldBeSameAs(sb);
        returned2.ShouldBeSameAs(sb);
        returned3.ShouldBeSameAs(sb);

        var expected =
            " First" + Environment.NewLine +
            "  Second" + Environment.NewLine +
            Environment.NewLine; // last call: padding 0 + null value => just newline
        sb.ToString().ShouldBe(expected);
    }
}

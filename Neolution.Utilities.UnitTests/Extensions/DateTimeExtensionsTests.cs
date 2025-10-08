namespace Neolution.Utilities.UnitTests.Extensions;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Unit tests for the <see cref="DateTimeExtensions"/> class.
/// </summary>
public class DateTimeExtensionsTests
{
    /// <summary>
    /// Tests the parameter-less <see cref="DateTimeExtensions.IsMidnight(DateTime)"/> method which requires the time to be exactly 00:00:00.
    /// </summary>
    /// <param name="year">The year component.</param>
    /// <param name="month">The month component.</param>
    /// <param name="day">The day component.</param>
    /// <param name="hour">The hour component.</param>
    /// <param name="minute">The minute component.</param>
    /// <param name="second">The second component.</param>
    /// <param name="expected">Expected result indicating whether it is exactly midnight.</param>
    [Theory]
    [InlineData(2025, 1, 10, 0, 0, 0, true)] // Exact midnight
    [InlineData(2025, 1, 10, 0, 0, 1, false)] // Midnight + 1 second
    [InlineData(2025, 1, 10, 0, 1, 0, false)] // 00:01
    [InlineData(2025, 1, 10, 1, 0, 0, false)] // 01:00
    [InlineData(2025, 1, 10, 23, 59, 59, false)] // End of day just before midnight
    public void GivenDateTime_WhenIsMidnightCalled_ThenReturnsExpected(
        int year, int month, int day, int hour, int minute, int second, bool expected)
    {
        // Arrange
        var dateTime = new DateTime(year, month, day, hour, minute, second, DateTimeKind.Unspecified);

        // Act
        var result = dateTime.IsMidnight();

        // Assert
        result.ShouldBe(expected);
    }

    /// <summary>
    /// Tests <see cref="DateTimeExtensions.IsMidnight(DateTime, bool)"/> with the ignoreSeconds flag.
    /// Ensures midnight detection works both strictly and when seconds are ignored.
    /// </summary>
    /// <param name="hour">Hour component.</param>
    /// <param name="minute">Minute component.</param>
    /// <param name="second">Second component.</param>
    /// <param name="ignoreSeconds">Flag indicating whether to ignore seconds.</param>
    /// <param name="expected">Expected result given the flag.</param>
    [Theory]
    [InlineData(0, 0, 0, false, true)] // Exact midnight strict
    [InlineData(0, 0, 1, false, false)] // Not exact midnight strict
    [InlineData(0, 0, 1, true, true)] // Midnight with seconds ignored
    [InlineData(0, 1, 0, true, false)] // Minute > 0 cannot be midnight
    [InlineData(1, 0, 0, true, false)] // Hour > 0 cannot be midnight
    public void GivenDateTimeAndIgnoreSecondsFlag_WhenIsMidnightCalled_ThenReturnsExpected(
        int hour, int minute, int second, bool ignoreSeconds, bool expected)
    {
        // Arrange
        var dateTime = new DateTime(2025, 5, 20, hour, minute, second, DateTimeKind.Unspecified);

        // Act
        var result = dateTime.IsMidnight(ignoreSeconds);

        // Assert
        result.ShouldBe(expected);
    }

    /// <summary>
    /// Tests <see cref="DateTimeExtensions.IsEndOfMonth(DateTime)"/> with various months including leap year February.
    /// </summary>
    /// <param name="year">Year component.</param>
    /// <param name="month">Month component.</param>
    /// <param name="day">Day component.</param>
    /// <param name="expected">Expected result (true if end of month).</param>
    [Theory]
    [InlineData(2025, 1, 31, true)] // 31-day month end
    [InlineData(2025, 1, 30, false)]
    [InlineData(2024, 2, 29, true)] // Leap year February
    [InlineData(2024, 2, 28, false)]
    [InlineData(2025, 2, 28, true)] // Non-leap year February
    [InlineData(2025, 2, 27, false)]
    [InlineData(2025, 4, 30, true)] // 30-day month end
    [InlineData(2025, 4, 29, false)]
    public void GivenDateTime_WhenIsEndOfMonthCalled_ThenReturnsExpected(
        int year, int month, int day, bool expected)
    {
        // Arrange
        var dateTime = new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Unspecified);

        // Act
        var result = dateTime.IsEndOfMonth();

        // Assert
        result.ShouldBe(expected);
    }

    /// <summary>
    /// Gets the test data for <see cref="GivenTargetAndRange_WhenIsInRangeCalled_ThenReturnsExpected"/> method.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1201:Elements should appear in the correct order", Justification = "It's better to have test data beside the test method")]
    public static TheoryData<DateTime, DateTime, DateTime, bool> GivenTargetAndRange_WhenIsInRangeCalled_ThenReturnsExpected_TestData => new()
    {
        {
            // Inside range
            new DateTime(2025, 5, 15, 0, 0, 0, DateTimeKind.Unspecified),
            new DateTime(2025, 5, 14, 0, 0, 0, DateTimeKind.Unspecified),
            new DateTime(2025, 5, 16, 0, 0, 0, DateTimeKind.Unspecified),
            true
        },
        {
            // Equal to start
            new DateTime(2025, 5, 14, 0, 0, 0, DateTimeKind.Unspecified),
            new DateTime(2025, 5, 14, 0, 0, 0, DateTimeKind.Unspecified),
            new DateTime(2025, 5, 16, 0, 0, 0, DateTimeKind.Unspecified),
            true
        },
        {
            // Equal to end
            new DateTime(2025, 5, 16, 0, 0, 0, DateTimeKind.Unspecified),
            new DateTime(2025, 5, 14, 0, 0, 0, DateTimeKind.Unspecified),
            new DateTime(2025, 5, 16, 0, 0, 0, DateTimeKind.Unspecified),
            true
        },
        {
            // Before start
            new DateTime(2025, 5, 13, 22, 0, 0, DateTimeKind.Unspecified),
            new DateTime(2025, 5, 14, 0, 0, 0, DateTimeKind.Unspecified),
            new DateTime(2025, 5, 16, 0, 0, 0, DateTimeKind.Unspecified),
            false
        },
        {
            // After end
            new DateTime(2025, 5, 16, 22, 0, 0, DateTimeKind.Unspecified),
            new DateTime(2025, 5, 14, 0, 0, 0, DateTimeKind.Unspecified),
            new DateTime(2025, 5, 16, 0, 0, 0, DateTimeKind.Unspecified),
            false
        },
        {
            // Single point range (match)
            new DateTime(2025, 5, 15, 0, 0, 0, DateTimeKind.Unspecified),
            new DateTime(2025, 5, 15, 0, 0, 0, DateTimeKind.Unspecified),
            new DateTime(2025, 5, 15, 0, 0, 0, DateTimeKind.Unspecified),
            true
        },
        {
            // Single point range (mismatch)
            new DateTime(2025, 5, 14, 21, 36, 0, DateTimeKind.Unspecified),
            new DateTime(2025, 5, 15, 0, 0, 0, DateTimeKind.Unspecified),
            new DateTime(2025, 5, 15, 0, 0, 0, DateTimeKind.Unspecified),
            false
        },
        {
            // Reversed range (start > end) always false
            new DateTime(2025, 5, 15, 0, 0, 0, DateTimeKind.Unspecified),
            new DateTime(2025, 5, 16, 0, 0, 0, DateTimeKind.Unspecified),
            new DateTime(2025, 5, 14, 0, 0, 0, DateTimeKind.Unspecified),
            false
        },
    };

    /// <summary>
    /// Tests <see cref="DateTimeExtensions.IsInRange(DateTime, DateTime, DateTime)"/> ensuring:
    /// - Inclusive start and end boundaries.
    /// - Dates before and after the range are excluded.
    /// - Single-point range behavior (start == end).
    /// - Reversed range (start > end) returns false.
    /// </summary>
    /// <param name="target">The target date to check.</param>
    /// <param name="startDate">The start of the range.</param>
    /// <param name="endDate">The end of the range.</param>
    /// <param name="expectedResult">Expected result indicating if target is in range.</param>
    [Theory]
    [MemberData(nameof(GivenTargetAndRange_WhenIsInRangeCalled_ThenReturnsExpected_TestData))]
    public void GivenTargetAndRange_WhenIsInRangeCalled_ThenReturnsExpected(DateTime target, DateTime startDate, DateTime endDate, bool expectedResult)
    {
        // Act
        var result = target.IsInRange(startDate, endDate);

        // Assert
        result.ShouldBe(expectedResult);
    }
}

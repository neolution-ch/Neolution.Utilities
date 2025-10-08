namespace Neolution.Utilities.Extensions;

/// <summary>
/// The DateTime extension methods.
/// </summary>
public static class DateTimeExtensions
{
    /// <summary>
    /// Determines whether the specified <see cref="DateTime"/> value is exactly at midnight (00:00:00).
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTime"/> to evaluate.</param>
    /// <returns><c>true</c> if the time component is exactly 00:00:00; otherwise <c>false</c>.</returns>
    public static bool IsMidnight(this DateTime dateTime)
        => dateTime.IsMidnight(false);

    /// <summary>
    /// Determines whether the specified <see cref="DateTime"/> value represents midnight (00:00),
    /// optionally ignoring the seconds component.
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTime"/> to evaluate.</param>
    /// <param name="ignoreSeconds">
    /// If <c>true</c>, evaluates midnight as any time where hour and minute are zero (00:00), regardless of seconds.
    /// If <c>false</c>, requires the time to be exactly 00:00:00.
    /// </param>
    /// <returns>
    /// <c>true</c> if the time is midnight according to the <paramref name="ignoreSeconds"/> rule; otherwise <c>false</c>.
    /// </returns>
    public static bool IsMidnight(this DateTime dateTime, bool ignoreSeconds)
        => ignoreSeconds ? dateTime.Hour == 0 && dateTime.Minute == 0 : dateTime.TimeOfDay.Ticks == 0;

    /// <summary>
    /// Determines whether the specified <see cref="DateTime"/> value is the end of the month.
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTime"/> to evaluate.</param>
    /// <returns><c>true</c> if the specified <see cref="DateTime"/> value is the end of the month; otherwise, <c>false</c>.</returns>
    public static bool IsEndOfMonth(this DateTime dateTime)
        => dateTime.Day == DateTime.DaysInMonth(dateTime.Year, dateTime.Month);

    /// <summary>
    /// Determines whether the specified <see cref="DateTime"/> value is in range.
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTime"/> to evaluate.</param>
    /// <param name="startDate">The start <see cref="DateTime"/>.</param>
    /// <param name="endDate">The end <see cref="DateTime"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="DateTime"/> value is in range; otherwise, <c>false</c>.</returns>
    public static bool IsInRange(this DateTime dateTime, DateTime startDate, DateTime endDate)
        => dateTime >= startDate && dateTime <= endDate;
}

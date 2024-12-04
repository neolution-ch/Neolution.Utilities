namespace Neolution.Utilities.DateAndTime
{
    using DateTime = System.DateTime;

    /// <summary>
    /// Date utility functions
    /// </summary>
    public static class DateUtils
    {
        /// <summary>
        /// Gets the current quarter as an integer in range 1 to 4
        /// </summary>
        public static int CurrentQuarter { get; } = (int)Math.Ceiling(DateTime.Today.Month / 3d);

        /// <summary>
        /// Gets the previous quarter as an integer in range 1 to 4
        /// </summary>
        public static int PreviousQuarter { get; } = (CurrentQuarter + 2) % 4 + 1;

        /// <summary>
        /// Gets the DateTime of the first day of the current quarter.
        /// </summary>
        /// <returns>The date of the first day of the current quarter.</returns>
        public static DateTime StartOfCurrentQuarter { get; } = new(DateTime.Today.Year, (CurrentQuarter - 1) * 3 + 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Gets the DateTime of the first day of the previous quarter.
        /// </summary>
        /// <returns>The date of the first day of the previous quarter.</returns>
        public static DateTime StartOfPreviousQuarter { get; } = new(

                // if the current quarter is the first one (jan - mar), then the previous quarter lies in the previous year
                DateTime.Today.Year - (CurrentQuarter == 1 ? 1 : 0),

                // calculates the starting month of the previous quarter
                (PreviousQuarter - 1) * 3 + 1,
                1,
                0,
                0,
                0,
                DateTimeKind.Utc);

        /// <summary>
        /// Combine date and time of two different DateTimes
        /// </summary>
        /// <param name="dateOnly">the datetime for date</param>
        /// <param name="timeOnly">the datetime for time</param>
        /// <returns>a date</returns>
        public static DateTime CombineDates(DateTime dateOnly, DateTime? timeOnly)
        {
            return timeOnly is not null
                ? dateOnly.Date.Add(timeOnly.Value.TimeOfDay)
                : dateOnly.Date; // TimeOnly is null, reset the time to midnight
        }

        /// <summary>
        /// Method that check if date is at midnight or not
        /// </summary>
        /// <param name="date">the date to check</param>
        /// <param name="ignoreSeconds">Ignore seconds and milliseconds</param>
        /// <returns>a bool</returns>
        public static bool IsMidnight(DateTime date, bool ignoreSeconds)
        {
            if (ignoreSeconds)
            {
                return date.Hour == 0 && date.Minute == 0;
            }

            return date.TimeOfDay.Ticks == 0;
        }

        /// <summary>
        /// Calculate the difference between a given date and today's date in terms of years
        /// Particularly useful for people age calculation
        /// </summary>
        /// <param name="date">the date to compare respect to today's date</param>
        /// <returns>The difference between today's date and the given date in terms of years</returns>
        public static int GetYearsFromToday(DateTime date)
        {
            var today = DateTime.Today;
            var years = today.Year - date.Year;

            // Case of a leap year
            if (date.Date > today.AddYears(-years))
            {
                years--;
            }

            return years;
        }

        /// <summary>
        /// Check whether the date is in range (inclusive dates)
        /// </summary>
        /// <param name="date">The date to be checked</param>
        /// <param name="startDate">The inferior bound date</param>
        /// <param name="endDate">The upper bound date</param>
        /// <returns>Whether the date is in the given range.</returns>
        public static bool IsDateInRange(DateTime date, DateTime startDate, DateTime endDate)
        {
            return date.Date >= startDate.Date && date.Date <= endDate.Date;
        }

        /// <summary>
        /// Checks whether the date of birth is in a given range (inclusive dates)
        /// </summary>
        /// <param name="dateOfBirth">The date of birth</param>
        /// <param name="startDate">The inferior bound date</param>
        /// <param name="endDate">The superior bound date</param>
        /// <returns>A value indicating whether the date of birth is in a given range</returns>
        public static bool IsDateOfBirthInRange(DateTime dateOfBirth, DateTime startDate, DateTime endDate)
        {
            var temp = dateOfBirth.AddYears(startDate.Year - dateOfBirth.Year);

            // special case when start date falls in the year before
            if (temp < startDate)
            {
                temp = temp.AddYears(1);
            }

            return IsDateInRange(temp, startDate, endDate);
        }

        /// <summary>
        /// Calculate the difference between a two dates in terms of days (endDate - startDate)
        /// </summary>
        /// <param name="endDate">The end date</param>
        /// <param name="startDate">The start date</param>
        /// <param name="absoluteValue">Indicating whether value should be returned as absolute, hence no matter the dates order.</param>
        /// <returns>Return (endDate - startDate) in terms of days, possibly in absolute value.</returns>
        public static int GetDifferenceInDays(DateTime endDate, DateTime startDate, bool absoluteValue)
        {
            var differenceInDays = (endDate - startDate).Days;
            return absoluteValue ? Math.Abs(differenceInDays) : differenceInDays;
        }

        /// <summary>
        /// Determines whether the date is the end of the month
        /// </summary>
        /// <param name="date">The date</param>
        /// <returns>Whether the date is the end of the month</returns>
        public static bool IsEndOfMonth(DateTime date)
        {
            return date.Day == DateTime.DaysInMonth(date.Year, date.Month);
        }

        /// <summary>
        /// Gets the minimum date.
        /// </summary>
        /// <param name="dates">The dates.</param>
        /// <returns>the minimum date</returns>
        public static DateTime? GetMinimumDate(params DateTime?[] dates)
        {
            if (dates.Any(x => !x.HasValue))
            {
                return null;
            }

            return dates.Min();
        }

        /// <summary>
        /// Gets the minutes until next occurence.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="periodicityInMinutes">The periodicity in minutes.</param>
        /// <returns>minutes missing to next occurence</returns>
        public static int GetMinutesUntilNextOccurence(DateTime? date, int periodicityInMinutes)
        {
            if (!date.HasValue)
            {
                return 0;
            }

            var minutesFromLastOccurence = (int)(DateTime.UtcNow - date.Value).TotalMinutes;
            return Math.Max(0, periodicityInMinutes - minutesFromLastOccurence);
        }
    }
}

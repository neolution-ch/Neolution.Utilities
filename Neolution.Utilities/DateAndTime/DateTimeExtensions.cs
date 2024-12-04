namespace Neolution.Utilities.DateAndTime
{
    using System.Globalization;

    /// <summary>
    /// Extension Methods for DateTime.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Converts the DateTime to the full date time short representation (dddd, dd MMMM yyyy HH:mm)
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>The full date time short representation</returns>
        public static string ToFullDateTimeShortString(this DateTime dateTime)
        {
            return dateTime.ToString("f", CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Converts the DateTime to the full date time long representation (dddd, dd MMMM yyyy HH:mm:ss)
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>The full date time short representation</returns>
        public static string ToFullDateTimeLongString(this DateTime dateTime)
        {
            return dateTime.ToString("F", CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Converts the DateTime to the full date time short representation (dd.MM.yyyy HH:mm)
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>The full date time short representation</returns>
        public static string ToGeneralDateTimeShortString(this DateTime dateTime)
        {
            return dateTime.ToString("g", CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Converts the DateTime to the full date time long representation (dd.MM.yyyy HH:mm:ss)
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>The full date time short representation</returns>
        public static string ToGeneralDateTimeLongString(this DateTime dateTime)
        {
            return dateTime.ToString("G", CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Gets the date of the Monday of the current week
        /// </summary>
        /// <param name="dateTime">The date time</param>
        /// <returns>The date</returns>
        public static DateTime GetFirstDayOfTheWeek(this DateTime dateTime)
        {
            var difference = (7 + (int)dateTime.DayOfWeek - (int)DayOfWeek.Monday) % 7;
            return dateTime.AddDays(-1 * difference).Date;
        }

        /// <summary>
        /// Calculates the days between two dates according to the excel 360 logic.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>The days between two dates according to the excel 360 logic</returns>
        public static int DateDifferenceInDays360(this DateTime startDate, DateTime endDate)
        {
            int startDay = startDate.Day;
            int startMonth = startDate.Month;
            int startYear = startDate.Year;
            int endDay = endDate.Day;
            int endMonth = endDate.Month;
            int endYear = endDate.Year;

            if (startDay == 31 || startDate.IsLastDayOfFebruary())
            {
                startDay = 30;
            }

            if (startDay == 30 && endDay == 31)
            {
                endDay = 30;
            }

            return ((endYear - startYear) * 360) + ((endMonth - startMonth) * 30) + (endDay - startDay);
        }

        /// <summary>
        /// Determines whether this date is the last day of february.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>
        ///   <c>true</c> if  last day of february; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsLastDayOfFebruary(this DateTime date)
        {
            return date.Month == 2 && date.Day == DateTime.DaysInMonth(date.Year, date.Month);
        }
    }
}

namespace Neolution.Utilities.Strings
{
    using System.Globalization;

    /// <summary>
    /// The decimal extensions
    /// </summary>
    public static class DecimalExtensions
    {
        /// <summary>
        /// The long percent format
        /// </summary>
        public static readonly string LongPercentFormat = "#,##0.000#";

        /// <summary>
        /// The signed long percent format
        /// </summary>
        public static readonly string SignedLongPercentFormat = $"+{LongPercentFormat};-{LongPercentFormat};{LongPercentFormat}";

        /// <summary>
        /// Converts to long percent string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The value as long percent string</returns>
        public static string ToLongPercentString(this decimal value)
        {
            return value.ToString(LongPercentFormat, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Converts to long percent string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The value as long percent string</returns>
        public static string ToLongPercentString(this decimal? value)
        {
            return value.HasValue ? value.Value.ToLongPercentString() : null;
        }

        /// <summary>
        /// Converts to long percent string with sign (+-).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The value as long percent string</returns>
        public static string ToSignedLongPercentString(this decimal value)
        {
            return value.ToString(SignedLongPercentFormat, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Converts to signedcurrencystring.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>the signed currency value as string</returns>
        public static string ToSignedCurrencyString(this decimal value)
        {
            return value > 0 ? $"+{value:C}" : $"{value:C}";
        }
    }
}

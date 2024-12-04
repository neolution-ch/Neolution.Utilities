namespace Neolution.Utilities.Globalization
{
    using System.Globalization;

    /// <summary>
    /// CultureInfo utility functions
    /// </summary>
    public static class CultureInfoCreator
    {
        /// <summary>
        /// Creates a culture.
        /// </summary>
        /// <param name="name">The name of the culture.</param>
        /// <returns>The culture info.</returns>
        public static CultureInfo CreateWithSwissFormat(string name)
        {
            const int currencyDecimalDigits = 0;
            const int numberDecimalDigits = 2;
            const int percentDecimalDigits = 3;

            return new CultureInfo(name)
            {
                NumberFormat =
                {
                    CurrencySymbol = string.Empty,
                    CurrencyDecimalDigits = currencyDecimalDigits,
                    CurrencyPositivePattern = 0, // https://learn.microsoft.com/en-us/dotnet/api/system.globalization.numberformatinfo.currencypositivepattern#remarks
                    CurrencyNegativePattern = 1, // https://learn.microsoft.com/en-us/dotnet/api/system.globalization.numberformatinfo.currencynegativepattern#remarks
                    CurrencyDecimalSeparator = ".",
                    CurrencyGroupSeparator = "'",
                    NumberDecimalDigits = numberDecimalDigits,
                    NumberDecimalSeparator = ".",
                    NumberGroupSeparator = "'",
                    PercentDecimalDigits = percentDecimalDigits,
                    PercentDecimalSeparator = ".",
                    PercentGroupSeparator = "'",
                },
                DateTimeFormat =
                {
                    DateSeparator = ".",
                    ShortDatePattern = "dd.MM.yyyy",
                    LongDatePattern = "dddd, dd MMMM yyyy",
                    ShortTimePattern = "HH:mm",
                    LongTimePattern = "HH:mm:ss",
                },
            };
        }
    }
}

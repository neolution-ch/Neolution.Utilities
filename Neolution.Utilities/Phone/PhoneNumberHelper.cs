namespace Neolution.Utilities.Phone
{
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Provides methods for validating phone numbers.
    /// </summary>
    public static class PhoneNumberValidator
    {
        /// <summary>
        /// Determines whether the specified string is a valid Swiss phone number.
        /// </summary>
        /// <param name="phoneNumber">The phone number to validate.</param>
        /// <returns>
        ///   <c>true</c> if the specified string is a valid Swiss phone number; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSwissPhoneNumber(string phoneNumber)
        {
            return !string.IsNullOrWhiteSpace(phoneNumber) && Regex.Match(phoneNumber, @"^(\+41|0)\d+$").Success;
        }
    }

    /// <summary>
    /// Provides methods for formatting phone numbers.
    /// </summary>
    public static class PhoneNumberFormatter
    {
        /// <summary>
        /// Formats a phone number by removing spaces and adding the Swiss country code if necessary.
        /// </summary>
        /// <param name="phoneNumber">The phone number to format.</param>
        /// <returns>
        /// The formatted phone number, or an empty string if the input is null or whitespace.
        /// </returns>
        public static string FormatSwissPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return string.Empty;
            }

            var formattedPhoneNumber = phoneNumber.Replace(" ", string.Empty);

            if (formattedPhoneNumber.ElementAt(0) == '0')
            {
                formattedPhoneNumber = "+41" + formattedPhoneNumber[1..];
            }

            return formattedPhoneNumber;
        }
    }
}

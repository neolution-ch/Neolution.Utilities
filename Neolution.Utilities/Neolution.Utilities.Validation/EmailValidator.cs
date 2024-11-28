namespace Neolution.Utilities.Validation
{
    using System.Net.Mail;

    /// <summary>
    /// Provides methods to validate email addresses.
    /// </summary>
    public static class EmailValidator
    {
        /// <summary>
        /// Determines whether the specified email address is valid.
        /// </summary>
        /// <param name="emailAddress">The email address to validate.</param>
        /// <returns>
        ///   <c>true</c> if the specified email address is valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsEmailValid(string emailAddress)
        {
            try
            {
                var mailAddress = new MailAddress(emailAddress);
                return !string.IsNullOrWhiteSpace(mailAddress.Address);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Determines whether all specified email addresses are valid.
        /// </summary>
        /// <param name="emailAddresses">A collection of email addresses to validate.</param>
        /// <returns>
        ///   <c>true</c> if all email addresses are valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool AreValidEmails(string emailAddresses)
        {
            return !string.IsNullOrWhiteSpace(emailAddresses) && emailAddresses.Split(",").Select(IsEmailValid).All(isValid => isValid);
        }
    }
}

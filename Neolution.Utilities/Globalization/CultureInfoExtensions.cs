namespace Neolution.Utilities.Globalization
{
    using System.Globalization;

    /// <summary>
    /// Extension methods for <see cref="CultureInfo"/>.
    /// </summary>
    public static class CultureInfoExtensions
    {
        /// <summary>
        /// Gets the language code
        /// </summary>
        /// <param name="cultureInfo">The culture information</param>
        /// <returns>The language code</returns>
        public static string GetLanguageCode(this CultureInfo cultureInfo)
        {
            if (cultureInfo == null)
            {
                throw new ArgumentNullException(nameof(cultureInfo));
            }

            return cultureInfo.IsNeutralCulture ? cultureInfo.Name : cultureInfo.Parent.Name;
        }
    }
}
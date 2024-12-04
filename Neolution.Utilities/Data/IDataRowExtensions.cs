namespace Neolution.Utilities.Data
{
    using System.Data;
    using System.Globalization;

    /// <summary>
    /// The data row extension class
    /// </summary>
    public static class IDataRowExtensions
    {
        /// <summary>
        /// Gets the field as decimal.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>the decimal value</returns>
        public static decimal GetFieldAsDecimal(this DataRow row, string fieldName)
        {
            return decimal.Parse(GetFieldValue(row, fieldName), CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Gets the field as int.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>the int value</returns>
        public static int GetFieldAsInt(this DataRow row, string fieldName)
        {
            return int.Parse(GetFieldValue(row, fieldName), CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Gets the field as boolean.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>the boolean value</returns>
        public static bool GetFieldAsBoolean(this DataRow row, string fieldName)
        {
            var value = GetFieldValue(row, fieldName);
            return !(string.IsNullOrWhiteSpace(value) || value.Trim().ToLowerInvariant() == "0" || value.Trim().ToLowerInvariant() == "false");
        }

        /// <summary>
        /// Gets the field value.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>the string value</returns>
        private static string GetFieldValue(DataRow row, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(fieldName))
            {
                throw new ArgumentNullException(nameof(fieldName));
            }

            return row.Field<string>(fieldName);
        }
    }
}

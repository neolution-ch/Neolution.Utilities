namespace Neolution.Utilities.Strings
{
    using System.Text;

    /// <summary>
    /// StringBuilder Extensions
    /// </summary>
    public static class StringBuilderExtensions
    {
        /// <summary>
        /// Appends the line with the specified padding
        /// </summary>
        /// <param name="stringBuilder">The string builder</param>
        /// <param name="value">The value</param>
        /// <param name="padding">The padding</param>
        public static void AppendLine(this StringBuilder stringBuilder, string value, int padding)
        {
            if (stringBuilder == null)
            {
                throw new ArgumentNullException(nameof(stringBuilder));
            }

            stringBuilder.AppendLine($"{new string(' ', padding)}{value}");
        }
    }
}

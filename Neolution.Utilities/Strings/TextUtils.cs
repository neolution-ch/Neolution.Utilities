namespace Neolution.Utilities.Strings
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// Text utility functions
    /// </summary>
    public static class TextUtils
    {
        /// <summary>
        /// Converts the new line characters to <br/> tags.
        /// </summary>
        /// <param name="text">The text to convert</param>
        /// <returns>The converted text</returns>
        public static string ConvertNewLineToBr(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            return Regex.Replace(text, "\\r?\\n{1}", "<br/>");
        }

        /// <summary>
        /// Extracts the GUID from a string.
        /// </summary>
        /// <param name="text">The text</param>
        /// <returns>The extracted GUID</returns>
        public static string ExtractGuidFromString(string text)
        {
            return ExtractGuidsFromString(text).FirstOrDefault();
        }

        /// <summary>
        /// Extracts the GUIDs from a string.
        /// </summary>
        /// <param name="text">The text</param>
        /// <returns>The extracted GUIDs</returns>
        public static IList<string> ExtractGuidsFromString(string text)
        {
            const string guidRegex = "([a-zA-Z0-9]{8}\\-[a-zA-Z0-9]{4}\\-[a-zA-Z0-9]{4}\\-[a-zA-Z0-9]{4}\\-[a-zA-Z0-9]{12})";
            return Regex.Matches(text, guidRegex).AsEnumerable().SelectMany(x => x.Captures).Select(x => x.Value).ToList();
        }

        /// <summary>
        /// Truncates the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="length">The length.</param>
        /// <returns>The truncated text.</returns>
        public static string Truncate(string text, int length)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            return text.Substring(0, Math.Min(text.Length, length));
        }

        /// <summary>
        /// Replaces the invalid file name characters.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>The filename with the invalid characters replaced</returns>
        public static string ReplaceInvalidFileNameChars(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return filename;
            }

            return string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
        }
    }
}

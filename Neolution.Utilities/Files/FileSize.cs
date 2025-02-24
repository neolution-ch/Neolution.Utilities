namespace Neolution.Utilities.Files
{
    /// <summary>
    /// Provides methods to get file sizes in human-readable formats.
    /// </summary>
    public static class FileSize
    {
        /// <summary>
        /// Gets the human-readable file size.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns>The size string in human-readable format.</returns>
        public static string GetHumanReadableFileSize(long size)
        {
            const int orderDivider = 1024;
            var sizes = new[] { "B", "KB", "MB", "GB", "TB" };
            decimal len = size;
            var order = 0;
            while (len >= orderDivider && order < sizes.Length - 1)
            {
                order++;
                len /= orderDivider;
            }

            return $"{len:0.#} {sizes[order]}";
        }
    }
}

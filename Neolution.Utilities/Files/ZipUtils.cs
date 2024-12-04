namespace Neolution.Utilities.Files
{
    using System.IO.Compression;
    using System.Text;

    /// <summary>
    /// Zip utility functions
    /// </summary>
    public static class ZipFile
    {
        /// <summary>
        /// Create a zip file starting from a dictotionary of files (name, conent)
        /// </summary>
        /// <param name="files">The files to be processed in the zip.</param>
        /// <returns>The zip file containing the passed files.</returns>
        public static Task<byte[]> CreateAsync(Dictionary<string, string> files)
        {
            if (files == null)
            {
                throw new ArgumentNullException(nameof(files));
            }

            return CreateZipFileAsync(files);

            static async Task<byte[]> CreateZipFileAsync(Dictionary<string, string> files)
            {
                using var memoryStream = new MemoryStream();
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create))
                {
                    foreach (var file in files)
                    {
                        var entry = archive.CreateEntry(file.Key);
                        using var entryStream = entry.Open();
                        using var streamWriter = new StreamWriter(entryStream, Encoding.UTF8);
                        await streamWriter.WriteAsync(file.Value).ConfigureAwait(false);
                    }
                }

                return memoryStream.ToArray();
            }
        }
    }
}

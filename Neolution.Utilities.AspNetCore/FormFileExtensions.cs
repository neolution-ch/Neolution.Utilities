namespace Neolution.Utilities.AspNetCore
{
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Some FormFile extension methods.
    /// </summary>
    public static class FormFileExtensions
    {
        /// <summary>
        /// Returns byte[] from FormFile.
        /// </summary>
        /// <param name="formFile">FormFile instance.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Content.</returns>
        public static Task<byte[]> GetByteArrayAsync(this IFormFile formFile, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(formFile);

            return GetByteArrayInternalAsync();

            async Task<byte[]> GetByteArrayInternalAsync()
            {
                using var stream = new MemoryStream();
                await formFile.CopyToAsync(stream, cancellationToken).ConfigureAwait(false);

                var buffer = new byte[16 * 1024];
                int read;
                while ((read = await stream.ReadAsync(buffer.AsMemory(0, buffer.Length), cancellationToken).ConfigureAwait(false)) > 0)
                {
                    stream.Write(buffer, 0, read);
                }

                stream.Position = 0;
                return stream.ToArray();
            }
        }
    }
}

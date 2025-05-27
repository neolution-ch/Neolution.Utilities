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
                return stream.ToArray();
            }
        }
    }
}

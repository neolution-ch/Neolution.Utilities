namespace Neolution.Utilities.Uris
{
    using System.Globalization;
    using System.Web;

    /// <summary>
    /// Extension methods for Uri
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Appends the specified paths.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="paths">The paths.</param>
        /// <returns>The URI with the appended paths</returns>
        public static Uri Append(this Uri uri, params string[] paths)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            if (paths == null)
            {
                throw new ArgumentNullException(nameof(paths));
            }

            return new Uri(paths.Aggregate(uri.AbsoluteUri, (current, path) => string.Format(CultureInfo.InvariantCulture, "{0}/{1}", current.TrimEnd('/'), path.TrimStart('/'))));
        }

        /// <summary>
        /// Adds the query parameter.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>the uri with appended query parameter</returns>
        public static Uri AddQueryParameter(this Uri uri, string name, string value)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            var httpValueCollection = HttpUtility.ParseQueryString(uri.Query);

            httpValueCollection.Remove(name);
            httpValueCollection.Add(name, value);

            var uriBuilder = new UriBuilder(uri);
            uriBuilder.Query = httpValueCollection.ToString();

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Adds the query parameter.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>the uri with appended query parameter</returns>
        public static Uri AddQueryParameter(this Uri uri, string name, object value)
        {
            return uri.AddQueryParameter(name, $"{value}");
        }
    }
}

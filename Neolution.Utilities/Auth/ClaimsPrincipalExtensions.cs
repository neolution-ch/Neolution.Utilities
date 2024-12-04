namespace Neolution.Utilities.Auth
{
    using System.Globalization;
    using System.Security.Claims;

    /// <summary>
    /// Extensions for ClaimPrincipal / User.
    /// </summary>
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Users the identifier.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The identifier of the user.</returns>
        public static Guid? UserId(this ClaimsPrincipal user)
        {
            var value = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(value, CultureInfo.InvariantCulture, out Guid guid) ? guid : null;
        }

        /// <summary>
        /// Gets the logged-in user identifier.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The logged in user identifier</returns>
        public static Guid LoggedInUserId(this ClaimsPrincipal user)
        {
            return user?.UserId() ?? throw new ArgumentNullException(nameof(user));
        }

        /// <summary>
        /// Gets the first name of the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The first name of the user.</returns>
        public static string FirstName(this ClaimsPrincipal user)
        {
            return user?.FindFirst(ClaimTypes.GivenName)?.Value;
        }

        /// <summary>
        /// Gets the last name of the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The last name of the user.</returns>
        public static string LastName(this ClaimsPrincipal user)
        {
            return user?.FindFirst(ClaimTypes.Surname)?.Value;
        }

        /// <summary>
        /// Gets the email of the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The email of the user.</returns>
        public static string Email(this ClaimsPrincipal user)
        {
            return user?.FindFirst(ClaimTypes.Email)?.Value;
        }

        /// <summary>
        /// Gets the username of the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The username of the user.</returns>
        public static string UserName(this ClaimsPrincipal user)
        {
            return user?.FindFirst(ClaimTypes.Name)?.Value;
        }
    }
}

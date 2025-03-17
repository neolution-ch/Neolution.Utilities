namespace Neolution.Utilities.Linq
{
    /// <summary>
    /// IEnumerable extensions
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Performs the specified action on each element of the list.
        /// </summary>
        /// <typeparam name="T">The item type</typeparam>
        /// <param name="items">The items</param>
        /// <param name="action">The action</param>
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            ArgumentNullException.ThrowIfNull(items);
            ArgumentNullException.ThrowIfNull(action);

            foreach (var item in items)
            {
                action(item);
            }
        }

        /// <summary>
        /// FirstOrDefault for struct types.
        /// </summary>
        /// <typeparam name="T">The struct type.</typeparam>
        /// <param name="items">The items.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>The FirstOrDefault</returns>
        public static T? StructFirstOrDefault<T>(this IEnumerable<T> items, Func<T, bool> predicate)
            where T : struct
        {
            return items.Where(predicate).Cast<T?>().FirstOrDefault();
        }
    }
}
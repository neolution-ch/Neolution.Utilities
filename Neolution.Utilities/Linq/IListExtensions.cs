namespace Neolution.Utilities.Linq
{
    /// <summary>
    /// IList extensions
    /// </summary>
    public static class IListExtensions
    {
        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <typeparam name="T">The list type</typeparam>
        /// <param name="list">The list</param>
        /// <param name="items">The items</param>
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> items)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (list is List<T> asList)
            {
                asList.AddRange(items);
            }
            else
            {
                foreach (var item in items)
                {
                    list.Add(item);
                }
            }
        }

        /// <summary>
        /// Removes the range.
        /// </summary>
        /// <typeparam name="T">the list type</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="items">The items.</param>
        public static void RemoveRange<T>(this IList<T> list, IEnumerable<T> items)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            // Evaluate the enumerable because items is usually a subset of list,
            // This prevents 'Collection was modified' error
            foreach (var item in items.ToList())
            {
                list.Remove(item);
            }
        }

        /// <summary>
        /// Performs the distinct with a key selector.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="list">The list</param>
        /// <param name="keySelector">The key selector</param>
        /// <returns>The distinct list</returns>
        public static IList<TSource> DistinctBy<TSource, TKey>(this IList<TSource> list, Func<TSource, TKey> keySelector)
        {
            return list.DistinctBy(keySelector, x => x.First());
        }

        /// <summary>
        /// Performs the distinct with a key selector.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="list">The list</param>
        /// <param name="keySelector">The key selector</param>
        /// <param name="groupSelector">The group selector</param>
        /// <returns>The distinct list</returns>
        public static IList<TSource> DistinctBy<TSource, TKey>(this IList<TSource> list, Func<TSource, TKey> keySelector, Func<IGrouping<TKey, TSource>, TSource> groupSelector)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            return list.GroupBy(keySelector).Select(x => groupSelector(x)).ToList();
        }

        /// <summary>
        /// Removes all the elements that match the conditions defined by the specified predicate.
        /// </summary>
        /// <typeparam name="T">The list type</typeparam>
        /// <param name="list">The list</param>
        /// <param name="match">The delegate that defines the conditions of the elements to remove.</param>
        public static void RemoveAll<T>(this IList<T> list, Predicate<T> match)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (match == null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            list.RemoveRange(list.Where(x => match(x)));
        }
    }
}

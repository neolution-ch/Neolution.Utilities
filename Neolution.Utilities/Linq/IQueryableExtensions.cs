namespace Neolution.Utilities.Linq
{
    using System.ComponentModel;
    using System.Linq.Expressions;

    /// <summary>
    /// Extensions for the IQueryable interface.
    /// </summary>
    public static class IQueryableExtensions
    {
        /// <summary>
        /// Sorts the elements of a sequence according to a key and order direction.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="keySelector">The key selector.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <returns>The ordered query.</returns>
        public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(this IQueryable<TSource> query, Expression<Func<TSource, TKey>> keySelector, ListSortDirection sortDirection)
        {
            var ascending = sortDirection == ListSortDirection.Ascending;

            return ascending ? query.OrderBy(keySelector) : query.OrderByDescending(keySelector);
        }

        /// <summary>
        /// Sorts the elements of a sequence according to a key and order direction.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="keySelector">The key selector.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <returns>The ordered query.</returns>
        public static IOrderedQueryable<TSource> ThenBy<TSource, TKey>(this IOrderedQueryable<TSource> query, Expression<Func<TSource, TKey>> keySelector, ListSortDirection sortDirection)
        {
            var ascending = sortDirection == ListSortDirection.Ascending;

            return ascending ? query.ThenBy(keySelector) : query.ThenByDescending(keySelector);
        }
    }
}

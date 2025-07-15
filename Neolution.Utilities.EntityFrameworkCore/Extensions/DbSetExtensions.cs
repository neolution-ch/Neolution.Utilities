namespace Neolution.Utilities.EntityFrameworkCore.Extensions;

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Neolution.Utilities.EntityFrameworkCore.Abstractions;

/// <summary>
/// The DbSet extensions.
/// </summary>
public static class DbSetExtensions
{
    /// <summary>
    /// Gets the next sort order asynchronously.
    /// </summary>
    /// <typeparam name="T">The entity type</typeparam>
    /// <param name="dbSet">The database set.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The next sort order</returns>
    /// <remarks>
    /// Concurrency note: This method has inherent race conditions in high-concurrency scenarios where multiple
    /// operations might calculate the same next sort order. Consider using database sequences, unique constraints
    /// with retry logic, or application-level coordination for high-concurrency use cases.
    /// </remarks>
    public static Task<int> GetNextSortOrderAsync<T>(this DbSet<T> dbSet, CancellationToken cancellationToken)
        where T : class, ISortableEntity
    {
        ArgumentNullException.ThrowIfNull(dbSet);
        return dbSet.GetNextSortOrderInternalAsync(null, cancellationToken);
    }

    /// <summary>
    /// Gets the next sort order asynchronously.
    /// </summary>
    /// <typeparam name="T">The entity type</typeparam>
    /// <param name="dbSet">The database set.</param>
    /// <param name="predicate">The predicate.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The next sort order</returns>
    /// <remarks>
    /// Concurrency note: This method has inherent race conditions in high-concurrency scenarios where multiple
    /// operations might calculate the same next sort order. Consider using database sequences, unique constraints
    /// with retry logic, or application-level coordination for high-concurrency use cases.
    /// </remarks>
    public static Task<int> GetNextSortOrderAsync<T>(this DbSet<T> dbSet, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        where T : class, ISortableEntity
    {
        ArgumentNullException.ThrowIfNull(dbSet);
        ArgumentNullException.ThrowIfNull(predicate);
        return dbSet.GetNextSortOrderInternalAsync(predicate, cancellationToken);
    }

    /// <summary>
    /// Gets the next sort order asynchronously (internal).
    /// </summary>
    /// <typeparam name="T">The entity type</typeparam>
    /// <param name="dbSet">The database set.</param>
    /// <param name="predicate">The predicate.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The next sort order</returns>
    private static async Task<int> GetNextSortOrderInternalAsync<T>(this DbSet<T> dbSet, Expression<Func<T, bool>>? predicate, CancellationToken cancellationToken)
        where T : class, ISortableEntity
    {
        var query = dbSet.AsQueryable();

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        var maxSortOrder = await query.MaxAsync(x => (int?)x.SortOrder, cancellationToken).ConfigureAwait(false);
        return maxSortOrder + 1 ?? 0;
    }
}

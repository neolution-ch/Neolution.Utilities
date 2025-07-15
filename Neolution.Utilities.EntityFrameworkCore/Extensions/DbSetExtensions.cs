namespace Neolution.Utilities.EntityFrameworkCore.Extensions;

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Neolution.Utilities.EntityFrameworkCore.Interfaces;

/// <summary>
/// The DbSet extensions.
/// </summary>
public static class DbSetExtensions
{
    /// <summary>
    /// Gets the next sort order asynchronous.
    /// </summary>
    /// <typeparam name="T">The entity type</typeparam>
    /// <param name="dbSet">The database set.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The next sort order</returns>
    public static Task<int> GetNextSortOrderAsync<T>(this DbSet<T> dbSet, CancellationToken cancellationToken)
        where T : class, ISortableEntity
    {
        ArgumentNullException.ThrowIfNull(dbSet);
        return dbSet.GetNextSortOrderInternalAsync(null, cancellationToken);
    }

    /// <summary>
    /// Gets the next sort order asynchronous.
    /// </summary>
    /// <typeparam name="T">The entity type</typeparam>
    /// <param name="dbSet">The database set.</param>
    /// <param name="predicate">The predicate.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The next sort order</returns>
    public static Task<int> GetNextSortOrderAsync<T>(this DbSet<T> dbSet, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        where T : class, ISortableEntity
    {
        ArgumentNullException.ThrowIfNull(dbSet);
        ArgumentNullException.ThrowIfNull(predicate);
        return dbSet.GetNextSortOrderInternalAsync(predicate, cancellationToken);
    }

    /// <summary>
    /// Gets the next sort order internal asynchronous.
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

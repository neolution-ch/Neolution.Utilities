namespace Neolution.Utilities.EntityFrameworkCore.UnitTests.Extensions;

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Neolution.Utilities.EntityFrameworkCore.Extensions;
using Shouldly;

/// <summary>
/// Unit tests for the <see cref="DbSetExtensions"/> class.
/// </summary>
public class DbSetExtensionsTests
{
    /// <summary>
    /// Gets the next sort order asynchronous should throw argument null exception when database set is null.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetNextSortOrderAsync_ShouldThrowArgumentNullException_WhenDbSetIsNull()
    {
        // Arrange
        DbSet<TestDbContext.SortableEntity>? dbSet = null!;

        // Act & Assert
        await Should.ThrowAsync<ArgumentNullException>(async () =>
        {
            await dbSet.GetNextSortOrderAsync(CancellationToken.None);
        });
    }

    /// <summary>
    /// Gets the next sort order asynchronous should throw argument null exception when predicate is null.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetNextSortOrderAsync_ShouldThrowArgumentNullException_WhenPredicateIsNull()
    {
        // Arrange
        using var context = new TestDbContext();
        Expression<Func<TestDbContext.SortableEntity, bool>>? predicate = null;

        // Act & Assert
        await Should.ThrowAsync<ArgumentNullException>(async () =>
        {
            await context.SortableEntities.GetNextSortOrderAsync(predicate!, CancellationToken.None);
        });
    }

    /// <summary>
    /// Gets the next sort order asynchronous should throw operation canceled exception when cancellation token is canceled.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetNextSortOrderAsync_ShouldThrowOperationCanceledException_WhenCancellationTokenIsCanceled()
    {
        // Arrange
        using var context = new TestDbContext();
        using var cts = new CancellationTokenSource();
        cts.Cancel();

        // Act & Assert
        await Should.ThrowAsync<OperationCanceledException>(async () =>
        {
            await context.SortableEntities.GetNextSortOrderAsync(cts.Token);
        });
    }

    /// <summary>
    /// Gets the next sort order asynchronous should return zero when no entities exist.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetNextSortOrderAsync_ShouldReturnZero_WhenNoEntitiesExist()
    {
        // Arrange
        using var context = new TestDbContext();

        // Act
        var nextSortOrder = await context.SortableEntities.GetNextSortOrderAsync(CancellationToken.None);

        // Assert
        nextSortOrder.ShouldBe(0);
    }

    /// <summary>
    /// Gets the next sort order asynchronous should return next sort order when called without predicate.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetNextSortOrderAsync_ShouldReturnNextSortOrder_WhenCalledWithoutPredicate()
    {
        // Arrange
        using var context = new TestDbContext();
        context.SortableEntities.AddRange(
            new TestDbContext.SortableEntity { SortOrder = 0 },
            new TestDbContext.SortableEntity { SortOrder = 1 });
        context.SaveChanges();

        // Act
        var nextSortOrder = await context.SortableEntities.GetNextSortOrderAsync(CancellationToken.None);

        // Assert
        nextSortOrder.ShouldBe(2);
    }

    /// <summary>
    /// Gets the next sort order asynchronous should return next sort order when called with predicate.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetNextSortOrderAsync_ShouldReturnNextSortOrder_WhenCalledWithPredicate()
    {
        // Arrange
        using var context = new TestDbContext();
        context.SortableEntities.AddRange(
            new TestDbContext.SortableEntity { Group = "A", SortOrder = 0 },
            new TestDbContext.SortableEntity { Group = "A", SortOrder = 1 },
            new TestDbContext.SortableEntity { Group = "A", SortOrder = 2 },
            new TestDbContext.SortableEntity { Group = "B", SortOrder = 0 },
            new TestDbContext.SortableEntity { Group = "B", SortOrder = 1 });
        context.SaveChanges();

        // Act
        var nextSortOrder = await context.SortableEntities.GetNextSortOrderAsync(x => x.Group == "B", CancellationToken.None);

        // Assert
        nextSortOrder.ShouldBe(2);
    }

    /// <summary>
    /// Gets the next sort order asynchronous should return zero when no entities match predicate.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetNextSortOrderAsync_ShouldReturnZero_WhenNoEntitiesMatchPredicate()
    {
        // Arrange
        using var context = new TestDbContext();
        context.SortableEntities.AddRange(
            new TestDbContext.SortableEntity { Group = "A", SortOrder = 0 },
            new TestDbContext.SortableEntity { Group = "A", SortOrder = 1 },
            new TestDbContext.SortableEntity { Group = "A", SortOrder = 2 });
        context.SaveChanges();

        // Act
        var nextSortOrder = await context.SortableEntities.GetNextSortOrderAsync(x => x.Group == "B", CancellationToken.None);

        // Assert
        nextSortOrder.ShouldBe(0);
    }

    /// <summary>
    /// Gets the next sort order asynchronous should return next sort order when entities have negative or duplicate sort order.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetNextSortOrderAsync_ShouldReturnNextSortOrder_WhenEntitiesHaveNegativeOrDuplicateSortOrder()
    {
        // Arrange
        using var context = new TestDbContext();
        context.SortableEntities.AddRange(
            new TestDbContext.SortableEntity { SortOrder = -5 },
            new TestDbContext.SortableEntity { SortOrder = 0 },
            new TestDbContext.SortableEntity { SortOrder = 2 },
            new TestDbContext.SortableEntity { SortOrder = 2 });
        context.SaveChanges();

        // Act
        var nextSortOrder = await context.SortableEntities.GetNextSortOrderAsync(CancellationToken.None);

        // Assert
        nextSortOrder.ShouldBe(3);
    }
}

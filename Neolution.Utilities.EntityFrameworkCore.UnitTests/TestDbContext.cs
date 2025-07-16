namespace Neolution.Utilities.EntityFrameworkCore.UnitTests;

using Microsoft.EntityFrameworkCore;
using Neolution.Utilities.EntityFrameworkCore.Abstractions;

/// <summary>
/// The test database context for unit tests.
/// </summary>
public class TestDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TestDbContext"/> class.
    /// </summary>
    public TestDbContext()
        : base(new DbContextOptionsBuilder<TestDbContext>().UseInMemoryDatabase($"TestDb{Guid.NewGuid():N}").Options)
    {
        this.Database.EnsureCreated();
    }

    /// <summary>
    /// Gets or sets the sortable entities.
    /// </summary>
    public DbSet<SortableEntity> SortableEntities { get; set; }

    /// <summary>
    /// The sortable entity class used for testing.
    /// </summary>
    public class SortableEntity : ISortableEntity
    {
        /// <summary>
        /// Gets or sets the sortable entity identifier.
        /// </summary>
        public int SortableEntityId { get; set; }

        /// <summary>
        /// Gets or sets the group.
        /// </summary>
        public string? Group { get; set; }

        /// <inheritdoc/>
        public int SortOrder { get; set; }
    }
}

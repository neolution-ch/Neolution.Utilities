namespace Neolution.Utilities.EntityFrameworkCore.Abstractions;

/// <summary>
/// The interface for sortable entities.
/// </summary>
public interface ISortableEntity
{
    /// <summary>
    /// Gets or sets the sort order.
    /// </summary>
    int SortOrder { get; set; }
}

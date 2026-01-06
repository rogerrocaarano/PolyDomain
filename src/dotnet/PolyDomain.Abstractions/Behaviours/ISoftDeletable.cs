namespace PolyDomain.Abstractions.Behaviours;

/// <summary>
/// Defines an entity that supports "Soft Delete".
/// Instead of being physically removed from the database, it is marked as deleted.
/// </summary>
public interface ISoftDeletable
{
    /// <summary>
    /// Gets or sets a value indicating whether the entity has been deleted.
    /// </summary>
    bool IsDeleted { get; set; }

    /// <summary>
    /// Gets or sets the date and time in UTC when the entity was deleted.
    /// </summary>
    DateTimeOffset? DeletedOnUtc { get; set; }
}

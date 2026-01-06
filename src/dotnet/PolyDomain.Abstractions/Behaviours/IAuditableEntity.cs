namespace PolyDomain.Abstractions.Behaviours;

/// <summary>
/// Defines an entity that tracks creation and modification metadata.
/// Infrastructure layers can automatically populate these values.
/// </summary>
public interface IAuditableEntity
{
    /// <summary>
    /// Gets or sets the date and time in UTC when the entity was created.
    /// </summary>
    DateTimeOffset CreatedOnUtc { get; set; }

    /// <summary>
    /// Gets or sets the date and time in UTC when the entity was last modified.
    /// </summary>
    DateTimeOffset? ModifiedOnUtc { get; set; }
}

namespace PolyDomain.Abstractions.Primitives;

/// <summary>
/// Defines the contract for an Entity in Domain-Driven Design.
/// Entities have an identity that remains distinct even if their attributes are the same.
/// </summary>
/// <typeparam name="TId">The type of the identifier. Marked as covariant (out) to allow type compatibility.</typeparam>
public interface IEntity<out TId>
{
    /// <summary>
    /// Gets the unique identifier of the entity.
    /// </summary>
    TId Id { get; }
}

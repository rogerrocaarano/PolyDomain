using PolyDomain.Abstractions.Primitives;

namespace PolyDomain.Abstractions.Behaviours;

/// <summary>
/// Defines a source of domain events.
/// This interface is typically used by the infrastructure layer to dispatch events
/// after a transaction is committed.
/// </summary>
public interface IDomainEventSource
{
    /// <summary>
    /// Gets the list of domain events occurred.
    /// </summary>
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

    /// <summary>
    /// Clears the domain events to prevent duplicate processing.
    /// </summary>
    void ClearDomainEvents();
}

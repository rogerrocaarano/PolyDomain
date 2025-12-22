using PolyDomain.Abstractions.Behaviours;

namespace PolyDomain.Abstractions.Primitives;

/// <summary>
/// Defines the contract for an Aggregate Root.
/// An Aggregate Root is the main entity that controls access to the aggregate's members,
/// ensures consistency boundaries, and acts as a source of domain events.
/// </summary>
/// <typeparam name="TId">The type of the identifier.</typeparam>
public interface IAggregateRoot<out TId> : IEntity<TId>, IDomainEventSource
{
    // Inherits:
    // 1. Id (from IEntity)
    // 2. DomainEvents & ClearDomainEvents() (from IDomainEventSource)
}

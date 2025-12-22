using PolyDomain.Abstractions.Primitives;

namespace PolyDomain.Core.Primitives;

using System.Collections.Generic;

/// <summary>
/// Base class for Aggregate Roots.
/// Extends Entity capabilities by implementing the event sourcing contract.
/// </summary>
/// <typeparam name="TId">The type of the identifier.</typeparam>
public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot<TId>
{
    private readonly List<IDomainEvent> _domainEvents = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateRoot{TId}"/> class.
    /// Required by ORMs.
    /// </summary>
    protected AggregateRoot() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateRoot{TId}"/> class with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier.</param>
    protected AggregateRoot(TId id)
        : base(id) { }

    /// <inheritdoc />
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// Adds a new domain event to the aggregate.
    /// This method is protected to ensure only the Aggregate itself can raise events.
    /// </summary>
    /// <param name="domainEvent">The event to add.</param>
    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    /// <inheritdoc />
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}

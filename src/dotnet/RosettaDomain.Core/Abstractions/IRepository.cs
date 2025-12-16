namespace RosettaDomain.Core.Abstractions;

/// <summary>
/// Defines the standard contract for a repository in Domain-Driven Design.
/// Repositories are strictly limited to Aggregate Roots to ensure consistency boundaries.
/// </summary>
/// <typeparam name="TAggregate">The type of the aggregate root.</typeparam>
/// <typeparam name="TId">The type of the aggregate's identifier.</typeparam>
public interface IRepository<TAggregate, TId>
    where TAggregate : class, IAggregateRoot<TId>
{
    /// <summary>
    /// Gets the Unit of Work that manages the persistence lifecycle of this repository.
    /// Allows the client to commit the transaction involving this repository.
    /// </summary>
    IUnitOfWork UnitOfWork { get; }

    /// <summary>
    /// Adds a new aggregate to the repository.
    /// This mimics adding an item to an in-memory collection (synchronous).
    /// </summary>
    /// <param name="aggregate">The aggregate to add.</param>
    void Add(TAggregate aggregate);

    /// <summary>
    /// Updates an existing aggregate.
    /// </summary>
    /// <param name="aggregate">The aggregate to update.</param>
    void Update(TAggregate aggregate);

    /// <summary>
    /// Removes an aggregate from the repository.
    /// </summary>
    /// <param name="aggregate">The aggregate to remove.</param>
    void Remove(TAggregate aggregate);

    /// <summary>
    /// Retrieves an aggregate by its unique identifier (asynchronous I/O).
    /// </summary>
    /// <param name="id">The identifier of the aggregate.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The aggregate root if found; otherwise, null.</returns>
    Task<TAggregate?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
}

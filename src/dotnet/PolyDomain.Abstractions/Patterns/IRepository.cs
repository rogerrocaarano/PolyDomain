using PolyDomain.Abstractions.Primitives;

namespace PolyDomain.Abstractions.Patterns;

/// <summary>
/// Defines the standard contract for a repository in Domain-Driven Design.
/// Repositories are strictly limited to Aggregate Roots to ensure consistency boundaries.
/// </summary>
/// <typeparam name="TAggregate">The type of the aggregate root.</typeparam>
/// <typeparam name="TId">The type of the aggregate's identifier.</typeparam>
public interface IRepository<TAggregate, in TId>
    where TAggregate : class, IAggregateRoot<TId>
{
    /// <summary>
    /// Saves a new aggregate to the repository or upgrade an existing one.
    /// </summary>
    /// <param name="aggregate">The aggregate to save.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task SaveAsync(TAggregate aggregate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes an aggregate from the repository.
    /// </summary>
    /// <param name="aggregate">The aggregate to remove.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task RemoveAsync(TAggregate aggregate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves an aggregate by its unique identifier (asynchronous I/O).
    /// </summary>
    /// <param name="id">The identifier of the aggregate.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The aggregate root if found; otherwise, null.</returns>
    Task<TAggregate?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
}

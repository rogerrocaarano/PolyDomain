namespace PolyDomain.Abstractions.Patterns;

/// <summary>
/// Defines the contract for the Unit of Work pattern.
/// The Unit of Work coordinates the writing out of changes and resolves concurrency problems.
/// It ensures that multiple repository operations are treated as a single transaction.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Commits all changes made in this context to the underlying database.
    /// This method typically handles:
    /// 1. Dispatching Domain Events (before or after commit).
    /// 2. Database transaction management.
    /// 3. Concurrency check.
    /// </summary>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous save operation.
    /// The task result contains the number of state entries written to the database.
    /// </returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

namespace RosettaDomain.Core.Exceptions;

using System;

/// <summary>
/// Exception thrown when a concurrency violation occurs.
/// Typically happens during optimistic locking when the version of the aggregate
/// in the database does not match the version being saved.
/// </summary>
public class ConcurrencyException : DomainException
{
    /// <inheritdoc />
    public ConcurrencyException(string message)
        : base(message) { }

    /// <inheritdoc />
    public ConcurrencyException(string message, Exception? innerException)
        : base(message, innerException) { }

    /// <summary>
    /// Creates a new instance of <see cref="ConcurrencyException"/> specifically formatted for an Aggregate Root conflict.
    /// </summary>
    /// <typeparam name="T">The type of the Aggregate Root where the conflict occurred.</typeparam>
    /// <param name="id">The unique identifier of the aggregate that caused the conflict.</param>
    /// <param name="innerException">The original exception that triggered this concurrency error (e.g., from the database).</param>
    /// <returns>A configured <see cref="ConcurrencyException"/> with a descriptive message.</returns>
    public static ConcurrencyException CreateFor<T>(object id, Exception? innerException = null)
    {
        return new ConcurrencyException(
            $"A concurrency conflict occurred while saving the aggregate '{typeof(T).Name}' with ID '{id}'. The data may have been modified by another instance.",
            innerException
        );
    }
}

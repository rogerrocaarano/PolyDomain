namespace RosettaDomain.Core.Abstractions;

using System;
using System.Linq.Expressions;

/// <summary>
/// Encapsulates query logic in a reusable object.
/// Allows defining criteria for fetching entities without polluting the Repository.
/// </summary>
/// <typeparam name="T">The type of the entity.</typeparam>
public interface ISpecification<T>
{
    /// <summary>
    /// The criteria expression (Where clause).
    /// </summary>
    Expression<Func<T, bool>> Criteria { get; }
}

using System.Linq.Expressions;

namespace PolyDomain.Abstractions.Patterns;

/// <summary>
/// Encapsulates the query logic for entities in a reusable and testable format.
/// Implementations of this interface describe "what" to retrieve, decoupling the query logic
/// from the "how" implementation in the Repository or Infrastructure layer.
/// </summary>
/// <typeparam name="T">The type of the entity to which the specification applies.</typeparam>
public interface ISpecification<T>
{
    /// <summary>
    /// Gets the filtering expression (The "Where" clause).
    /// If null, no filtering is applied.
    /// </summary>
    /// <value>
    /// An expression that takes an entity and returns true if it matches the criteria.
    /// </value>
    Expression<Func<T, bool>>? Criteria { get; }

    /// <summary>
    /// Gets the list of related entities to include in the query results (Eager Loading).
    /// Used to prevent the N+1 query problem by performing SQL JOINs.
    /// </summary>
    /// <value>
    /// A list of expressions identifying navigation properties to be loaded.
    /// </value>
    List<Expression<Func<T, object>>> Includes { get; }

    /// <summary>
    /// Gets the list of related entities to include using string-based paths.
    /// Useful for including nested navigation properties (e.g., "Order.Items.Product").
    /// </summary>
    /// <value>
    /// A list of string paths representing the navigation hierarchy.
    /// </value>
    List<string> IncludeStrings { get; }

    /// <summary>
    /// Gets the expression used for sorting the results in ascending order.
    /// </summary>
    /// <value>
    /// An expression identifying the property to sort by.
    /// </value>
    Expression<Func<T, object>>? OrderBy { get; }

    /// <summary>
    /// Gets the expression used for sorting the results in descending order.
    /// </summary>
    /// <value>
    /// An expression identifying the property to sort by.
    /// </value>
    Expression<Func<T, object>>? OrderByDescending { get; }

    /// <summary>
    /// Gets the number of elements to retrieve (The "Limit" or "Top" clause).
    /// Only applied if <see cref="IsPagingEnabled"/> is true.
    /// </summary>
    int Take { get; }

    /// <summary>
    /// Gets the number of elements to bypass before returning the remaining elements (The "Offset" clause).
    /// Only applied if <see cref="IsPagingEnabled"/> is true.
    /// </summary>
    int Skip { get; }

    /// <summary>
    /// Gets a value indicating whether pagination (Skip/Take) should be applied to the query.
    /// </summary>
    bool IsPagingEnabled { get; }

    /// <summary>
    /// Gets a value indicating whether the query results should be tracked by the change tracker.
    /// Set this to true for read-only scenarios to improve performance.
    /// </summary>
    /// <remarks>
    /// When true, translates to <c>AsNoTracking()</c> in Entity Framework Core.
    /// </remarks>
    bool IsNoTracking { get; }
}

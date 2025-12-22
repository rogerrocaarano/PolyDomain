namespace PolyDomain.Abstractions.Primitives;

/// <summary>
/// Defines a business rule that must be satisfied.
/// Using rule objects allows encapsulating complex validation logic and making it reusable.
/// </summary>
public interface IBusinessRule
{
    /// <summary>
    /// Checks if the business rule is broken.
    /// </summary>
    /// <returns>True if the rule is broken; otherwise, false.</returns>
    bool IsBroken();

    /// <summary>
    /// Gets the error message describing why the rule was broken.
    /// </summary>
    string Message { get; }
}

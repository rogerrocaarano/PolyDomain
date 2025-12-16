namespace PolyDomain.Core.Exceptions;

using Abstractions;

/// <summary>
/// Exception thrown when a business rule is broken.
/// Contains details about the broken rule to provide meaningful feedback.
/// </summary>
public class BusinessRuleValidationException : DomainException
{
    /// <summary>
    /// Gets the business rule that was violated.
    /// </summary>
    public IBusinessRule BrokenRule { get; }

    /// <summary>
    /// Gets the details or message of the broken rule.
    /// </summary>
    public string Details { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessRuleValidationException"/> class.
    /// </summary>
    /// <param name="brokenRule">The business rule that was broken.</param>
    public BusinessRuleValidationException(IBusinessRule brokenRule)
        : base(brokenRule.Message)
    {
        BrokenRule = brokenRule;
        Details = brokenRule.Message;
    }

    /// <summary>
    /// Returns a string that represents the broken rule and its message.
    /// </summary>
    /// <returns>A string containing the rule type and message.</returns>
    public override string ToString()
    {
        return $"{BrokenRule.GetType().FullName}: {BrokenRule.Message}";
    }
}

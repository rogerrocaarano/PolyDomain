namespace RosettaDomain.Core.Abstractions;

/// <summary>
/// Marker interface for Domain Services.
/// Domain Services encapsulate domain logic that doesn't naturally fit within an Entity or Value Object,
/// typically operations involving multiple aggregates or complex calculations.
/// Domain Services must be stateless.
/// </summary>
public interface IDomainService
{
    // No specific methods required.
    // This interface serves as a marker for architectural clarity and DI registration.
}

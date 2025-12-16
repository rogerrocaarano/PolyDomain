namespace PolyDomain.Core.Abstractions;

/// <summary>
/// Marker interface for Value Objects.
/// In modern C#, it is recommended to implement this interface using 'record' types
/// to leverage built-in structural equality and immutability.
/// </summary>
public interface IValueObject
{
    // No members required.
    // Structural equality is handled by the 'record' type implementation.
}

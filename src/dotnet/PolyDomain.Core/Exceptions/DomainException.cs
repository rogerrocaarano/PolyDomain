namespace PolyDomain.Core.Exceptions;

using System;

/// <summary>
/// Base abstract class for all domain-layer exceptions.
/// Allows catching any domain-related error in higher layers using a single catch block.
/// </summary>
public abstract class DomainException : Exception
{
    /// <inheritdoc />
    protected DomainException(string message)
        : base(message) { }

    /// <inheritdoc />
    protected DomainException(string message, Exception? innerException)
        : base(message, innerException) { }
}

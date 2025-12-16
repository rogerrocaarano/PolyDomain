namespace RosettaDomain.Core.Exceptions;

using System;

/// <summary>
/// Exception thrown when a requested entity is not found in the persistence store.
/// Useful for handling 404 scenarios in the Application/API layer.
/// </summary>
public class EntityNotFoundException : DomainException
{
    /// <summary>
    /// Gets the name of the entity that could not be found.
    /// </summary>
    public string EntityName { get; }

    /// <summary>
    /// Gets the key or identifier that was used to search for the entity.
    /// </summary>
    public object Key { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class.
    /// </summary>
    /// <param name="entityName">The name of the entity type.</param>
    /// <param name="key">The identifier used to search.</param>
    public EntityNotFoundException(string entityName, object key)
        : base($"Entity '{entityName}' with key '{key}' was not found.")
    {
        EntityName = entityName;
        Key = key;
    }

    /// <summary>
    /// Helper factory method to create the exception in a strongly-typed way.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <param name="key">The identifier used to search.</param>
    /// <returns>A new instance of <see cref="EntityNotFoundException"/> configured for the specified type.</returns>
    public static EntityNotFoundException For<T>(object key)
    {
        return new EntityNotFoundException(typeof(T).Name, key);
    }
}

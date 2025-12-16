namespace RosettaDomain.Core.Primitives;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

/// <summary>
/// Base class for Smart Enums.
/// Enables strongly-typed enumerations that can contain behavior and data.
/// </summary>
public abstract class Enumeration : IComparable
{
    /// <summary>
    /// Gets the name of the enumeration item.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the unique identifier of the enumeration item.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Enumeration"/> class.
    /// </summary>
    /// <param name="id">The unique identifier.</param>
    /// <param name="name">The name.</param>
    protected Enumeration(int id, string name) => (Id, Name) = (id, name);

    /// <summary>
    /// Returns the name of the enumeration.
    /// </summary>
    public override string ToString() => Name;

    /// <summary>
    /// Gets all defined instances of the enumeration type.
    /// </summary>
    public static IEnumerable<T> GetAll<T>()
        where T : Enumeration
    {
        return typeof(T)
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Select(f => f.GetValue(null))
            .Cast<T>();
    }

    /// <summary>
    /// Gets an enumeration item by its ID.
    /// </summary>
    public static T FromValue<T>(int value)
        where T : Enumeration
    {
        var matchingItem = Parse<T, int>(value, "value", item => item.Id == value);
        return matchingItem;
    }

    /// <summary>
    /// Gets an enumeration item by its Name.
    /// </summary>
    public static T FromDisplayName<T>(string displayName)
        where T : Enumeration
    {
        var matchingItem = Parse<T, string>(
            displayName,
            "display name",
            item => item.Name == displayName
        );
        return matchingItem;
    }

    private static T Parse<T, K>(K value, string description, Func<T, bool> predicate)
        where T : Enumeration
    {
        var matchingItem = GetAll<T>().FirstOrDefault(predicate);

        if (matchingItem == null)
            throw new InvalidOperationException(
                $"'{value}' is not a valid {description} in {typeof(T)}"
            );

        return matchingItem;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) // CORREGIDO: object?
    {
        if (obj is not Enumeration otherValue)
        {
            return false;
        }

        var typeMatches = GetType().Equals(obj.GetType());
        var valueMatches = Id.Equals(otherValue.Id);

        return typeMatches && valueMatches;
    }

    /// <inheritdoc />
    public override int GetHashCode() => Id.GetHashCode();

    /// <inheritdoc />
    public int CompareTo(object? other) // CORREGIDO: object?
    {
        if (other == null)
            return 1;
        return Id.CompareTo(((Enumeration)other).Id);
    }
}

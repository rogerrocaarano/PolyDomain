using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PolyDomain.Abstractions.Primitives;

namespace PolyDomain.Infrastructure.EntityFrameworkCore.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyPolyDomainPrimitives(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var clrType = entityType.ClrType;

            var properties = clrType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => !p.IsDefined(typeof(NotMappedAttribute)));

            foreach (var property in properties)
            {
                if (IsValueObject(property.PropertyType))
                {
                    ConfigureComplexProperty(modelBuilder, clrType, property);
                }
                else if (IsCollection(property.PropertyType, out var itemType))
                {
                    ConfigureCollection(modelBuilder, clrType, property, itemType!);
                }
            }
        }
    }

    private static void ConfigureComplexProperty(
        ModelBuilder builder,
        Type entityType,
        PropertyInfo property
    )
    {
        builder.Entity(entityType).ComplexProperty(property.Name);
    }

    private static void ConfigureCollection(
        ModelBuilder builder,
        Type entityType,
        PropertyInfo property,
        Type itemType
    )
    {
        if (typeof(IValueObject).IsAssignableFrom(itemType))
        {
            builder.Entity(entityType).OwnsMany(itemType, property.Name, b => b.ToJson());
        }
        else if (typeof(IEntity).IsAssignableFrom(itemType))
        {
            builder.Entity(entityType).Navigation(property.Name).AutoInclude();
        }
    }

    private static bool IsValueObject(Type type) =>
        typeof(IValueObject).IsAssignableFrom(type) && !IsEnumerable(type);

    private static bool IsCollection(Type type, out Type? itemType)
    {
        itemType = null;
        if (!IsEnumerable(type) || type == typeof(string))
            return false;

        if (type.IsGenericType)
        {
            itemType = type.GetGenericArguments()[0];
            return true;
        }
        return false;
    }

    private static bool IsEnumerable(Type type) =>
        typeof(IEnumerable<object>).IsAssignableFrom(type);
}

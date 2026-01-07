using PolyDomain.Core.Primitives;

namespace PolyDomain.Infrastructure.EntityFrameworkCore.Tests.Core.Primitives;

public class ComplexAggregateImpl : AggregateRoot<Guid>
{
    public ICollection<EntityImpl> Collection { get; init; } = [];
    public EntityImpl? Entity1 { get; private set; } = null;
    public EntityImpl? Entity2 { get; private set; } = null;

    public ValueObjectImpl? ValueObject1 { get; private set; } = null;
    public ValueObjectImpl? ValueObject2 { get; private set; } = null;
    public ICollection<ValueObjectImpl> ValueObjectCollection { get; init; } = [];

    private ComplexAggregateImpl()
        : base(Guid.NewGuid()) { }

    public static ComplexAggregateImpl Create()
    {
        return new ComplexAggregateImpl();
    }

    public void AddEntityToCollection(EntityImpl entity)
    {
        Collection.Add(entity);
    }

    public void SetValueObject1(ValueObjectImpl valueObject)
    {
        ValueObject1 = valueObject;
    }

    public void SetValueObject2(ValueObjectImpl valueObject)
    {
        ValueObject2 = valueObject;
    }

    public void SetEntity1(EntityImpl entity)
    {
        Entity1 = entity;
    }

    public void SetEntity2(EntityImpl entity)
    {
        Entity2 = entity;
    }

    public void AddValueObjectToCollection(ValueObjectImpl valueObject)
    {
        ValueObjectCollection.Add(valueObject);
    }
}

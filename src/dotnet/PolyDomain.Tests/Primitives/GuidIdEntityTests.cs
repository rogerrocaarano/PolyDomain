using PolyDomain.Core.Primitives;

namespace PolyDomain.Tests.Primitives;

public class GuidIdEntityTests
{
    private class FakeEntity : Entity<Guid>
    {
        public FakeEntity(Guid id)
            : base(id) { }
    }

    [Fact]
    public void two_entities_with_same_id_should_be_equal()
    {
        // Arrange
        var id = Guid.NewGuid();
        var entity1 = new FakeEntity(id);
        var entity2 = new FakeEntity(id);

        // Act
        bool result = entity1 == entity2;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void two_entities_with_different_id_should_not_be_equal()
    {
        // Arrange
        var id1 = Guid.NewGuid();
        var id2 = Guid.NewGuid();
        var entity1 = new FakeEntity(id1);
        var entity2 = new FakeEntity(id2);

        // Act
        bool result = entity1 == entity2;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void transient_entities_with_default_id_should_not_be_equal()
    {
        // Arrange
        var id = Guid.Empty;
        var entity1 = new FakeEntity(id);
        var entity2 = new FakeEntity(id);

        // Act
        bool result = entity1 == entity2;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void entity_compared_to_null_should_be_false()
    {
        // Arrange
        var entity = new FakeEntity(Guid.NewGuid());

        // Act & Assert
        Assert.False(entity == null);
        Assert.False(null == entity);
        Assert.False(entity.Equals(null));
    }

    [Fact]
    public void entities_with_same_id_should_have_same_hashcode()
    {
        // Arrange
        var id = Guid.NewGuid();
        var entity1 = new FakeEntity(id);
        var entity2 = new FakeEntity(id);

        // Act & Assert
        Assert.Equal(entity1.GetHashCode(), entity2.GetHashCode());
    }

    [Fact]
    public void same_entity_instance_should_be_equal_to_itself()
    {
        // Arrange
        var entity = new FakeEntity(Guid.NewGuid());
        var transientEntity = new FakeEntity(Guid.Empty);

        // Act & Assert
        Assert.True(entity == entity);
        Assert.True(transientEntity == transientEntity);
        Assert.True(entity.Equals(entity));
    }
}

using PolyDomain.Core.Primitives;

namespace PolyDomain.Core.Tests;

public class IntIdEntityTests
{
    private class FakeEntity : Entity<int>
    {
        public FakeEntity(int id)
            : base(id) { }
    }

    [Fact]
    public void two_entities_with_same_id_should_be_equal()
    {
        // Arrange
        var entity1 = new FakeEntity(1);
        var entity2 = new FakeEntity(1);

        // Act
        bool result = entity1 == entity2;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void two_entities_with_different_id_should_not_be_equal()
    {
        // Arrange
        var entity1 = new FakeEntity(1);
        var entity2 = new FakeEntity(2);

        // Act
        bool result = entity1 == entity2;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void transient_entities_with_default_id_should_not_be_equal()
    {
        // Arrange
        var id = 0;
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
        var entity = new FakeEntity(1);

        // Act & Assert
        Assert.False(entity == null);
        Assert.False(null == entity);
        Assert.False(entity.Equals(null));
    }

    [Fact]
    public void entities_with_same_id_should_have_same_hashcode()
    {
        // Arrange
        var id = 1;
        var entity1 = new FakeEntity(id);
        var entity2 = new FakeEntity(id);

        // Act & Assert
        Assert.Equal(entity1.GetHashCode(), entity2.GetHashCode());
    }

    [Fact]
    public void same_entity_instance_should_be_equal_to_itself()
    {
        // Arrange
        var entity = new FakeEntity(1);
        var transientEntity = new FakeEntity(0);

        // Act & Assert
        Assert.True(entity == entity);
        Assert.True(transientEntity == transientEntity);
        Assert.True(entity.Equals(entity));
    }
}

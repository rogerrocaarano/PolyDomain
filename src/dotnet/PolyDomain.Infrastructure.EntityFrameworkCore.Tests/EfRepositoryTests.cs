using Microsoft.EntityFrameworkCore;
using PolyDomain.Infrastructure.EntityFrameworkCore.Tests.Core.Primitives;
using PolyDomain.Infrastructure.EntityFrameworkCore.Tests.Infrastructure.Persistence;

namespace PolyDomain.Infrastructure.EntityFrameworkCore.Tests;

public class EfRepositoryTests : IDisposable
{
    private readonly TestDbContext _context;
    private readonly EfRepositoryImpl<AggregateImpl> _aggregateRepository;
    private readonly EfRepositoryImpl<ComplexAggregateImpl> _complexAggregateRepository;

    public EfRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

        _context = new TestDbContext(options);
        _context.Database.OpenConnection();
        _context.Database.EnsureCreated();

        _aggregateRepository = new EfRepositoryImpl<AggregateImpl>(_context);
        _complexAggregateRepository = new EfRepositoryImpl<ComplexAggregateImpl>(_context);
    }

    public void Dispose()
    {
        _context.Database.CloseConnection();
        _context.Dispose();
    }

    [Fact]
    public async Task save_async_should_add_when_entity_is_new()
    {
        // Arrange
        var entity = AggregateImpl.Create("Data");

        // Act
        await _aggregateRepository.SaveAsync(entity);

        // Assert
        Assert.Equal(EntityState.Added, _context.Entry(entity).State);
    }

    [Fact]
    public async Task save_async_should_update_when_entity_exists_tracked()
    {
        // Arrange
        var entity = AggregateImpl.Create("Initial Data");
        _context.AggregateImpls.Add(entity);
        await _context.SaveChangesAsync();

        // Act
        entity.UpdateData("Updated Data");
        await _aggregateRepository.SaveAsync(entity);

        // Assert
        Assert.Equal(EntityState.Modified, _context.Entry(entity).State);
    }

    [Fact]
    public async Task save_async_should_update_when_entity_exists_untracked()
    {
        // Arrange
        var entity = AggregateImpl.Create("Initial Data");
        _context.AggregateImpls.Add(entity);
        await _context.SaveChangesAsync();

        _context.ChangeTracker.Clear();

        // Act
        entity.UpdateData("Updated Data");
        await _aggregateRepository.SaveAsync(entity);

        // Assert
        Assert.Equal(EntityState.Modified, _context.Entry(entity).State);
    }

    [Fact]
    public async Task get_by_id_async_should_return_entity_when_exists()
    {
        // Arrange
        var entity = AggregateImpl.Create("Data");
        _context.AggregateImpls.Add(entity);
        await _context.SaveChangesAsync();

        // Act
        var retrievedEntity = await _aggregateRepository.GetByIdAsync(entity.Id);

        // Assert
        Assert.NotNull(retrievedEntity);
        Assert.Equal(entity.Id, retrievedEntity.Id);
    }

    [Fact]
    public async Task get_by_id_async_should_return_null_when_not_exists()
    {
        // Act
        var retrievedEntity = await _aggregateRepository.GetByIdAsync(Guid.NewGuid());

        // Assert
        Assert.Null(retrievedEntity);
    }

    [Fact]
    public async Task context_should_handle_value_object_property()
    {
        // Arrange
        var valueObject = new ValueObjectImpl("val", 0);
        var complexAggregate = ComplexAggregateImpl.Create();
        complexAggregate.SetValueObject1(valueObject);

        // Act
        await _complexAggregateRepository.SaveAsync(complexAggregate);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();
        var savedEntity = await _complexAggregateRepository.GetByIdAsync(complexAggregate.Id);

        // Assert
        Assert.NotNull(savedEntity);
        Assert.NotNull(savedEntity.ValueObject1);
        Assert.Equal("val", savedEntity.ValueObject1.StringData);
        Assert.Equal(0, savedEntity.ValueObject1.IntData);
    }

    [Fact]
    public async Task context_should_handle_properties_with_same_value_object_type()
    {
        // Arrange
        var valueObject1 = new ValueObjectImpl("val1", 1);
        var valueObject2 = new ValueObjectImpl("val2", 2);
        var complexAggregate = ComplexAggregateImpl.Create();
        complexAggregate.SetValueObject1(valueObject1);
        complexAggregate.SetValueObject2(valueObject2);

        // Act
        await _complexAggregateRepository.SaveAsync(complexAggregate);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();
        var savedEntity = await _complexAggregateRepository.GetByIdAsync(complexAggregate.Id);

        // Assert
        Assert.NotNull(savedEntity);
        Assert.NotNull(savedEntity.ValueObject1);
        Assert.NotNull(savedEntity.ValueObject2);
        Assert.Equal("val1", savedEntity.ValueObject1.StringData);
        Assert.Equal(1, savedEntity.ValueObject1.IntData);
        Assert.Equal("val2", savedEntity.ValueObject2.StringData);
        Assert.Equal(2, savedEntity.ValueObject2.IntData);
    }

    [Fact]
    public async Task context_should_handle_value_object_collections()
    {
        // Arrange
        var complexAggregate = ComplexAggregateImpl.Create();
        complexAggregate.AddValueObjectToCollection(new ValueObjectImpl("val1", 1));
        complexAggregate.AddValueObjectToCollection(new ValueObjectImpl("val2", 2));

        // Act
        await _complexAggregateRepository.SaveAsync(complexAggregate);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();
        var savedEntity = await _complexAggregateRepository.GetByIdAsync(complexAggregate.Id);

        // Assert
        Assert.NotNull(savedEntity);
        Assert.Equal(2, savedEntity.ValueObjectCollection.Count);
        Assert.Contains(
            savedEntity.ValueObjectCollection,
            vo => vo.StringData == "val1" && vo.IntData == 1
        );
        Assert.Contains(
            savedEntity.ValueObjectCollection,
            vo => vo.StringData == "val2" && vo.IntData == 2
        );
    }

    [Fact]
    public async Task context_should_handle_entity_collections()
    {
        // Arrange
        var complexAggregate = ComplexAggregateImpl.Create();
        var entity1 = new EntityImpl("Entity1");
        var entity2 = new EntityImpl("Entity2");
        complexAggregate.AddEntityToCollection(entity1);
        complexAggregate.AddEntityToCollection(entity2);

        // Act
        await _complexAggregateRepository.SaveAsync(complexAggregate);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();
        var savedEntity = await _complexAggregateRepository.GetByIdAsync(complexAggregate.Id);

        // Assert
        Assert.NotNull(savedEntity);
        Assert.Equal(2, savedEntity.Collection.Count);
        Assert.Contains(savedEntity.Collection, e => e.Id == entity1.Id && e.Data == "Entity1");
        Assert.Contains(savedEntity.Collection, e => e.Id == entity2.Id && e.Data == "Entity2");
    }
}

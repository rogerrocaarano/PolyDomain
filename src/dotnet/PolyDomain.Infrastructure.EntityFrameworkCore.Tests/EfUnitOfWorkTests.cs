using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PolyDomain.Infrastructure.EntityFrameworkCore.Persistence;
using PolyDomain.Infrastructure.EntityFrameworkCore.Tests.Core.Primitives;
using PolyDomain.Infrastructure.EntityFrameworkCore.Tests.Infrastructure.Persistence;

namespace PolyDomain.Infrastructure.EntityFrameworkCore.Tests;

public class EfUnitOfWorkTests : IDisposable
{
    private readonly SqliteConnection _connection;
    private readonly TestDbContext _context;
    private readonly EfUnitOfWork<TestDbContext> _unitOfWork;

    public EfUnitOfWorkTests()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<TestDbContext>().UseSqlite(_connection).Options;

        _context = new TestDbContext(options);
        _context.Database.EnsureCreated();

        _unitOfWork = new EfUnitOfWork<TestDbContext>(_context);
    }

    [Fact]
    public async Task save_changes_async_should_persist_changes_to_database()
    {
        // Arrange
        var entity = AggregateImpl.Create("Data");
        await _context.AggregateImpls.AddAsync(entity);

        // Act
        var resultCount = await _unitOfWork.SaveChangesAsync();

        // Assert
        Assert.Equal(1, resultCount);

        _context.ChangeTracker.Clear();
        var savedEntity = await _context.AggregateImpls.FindAsync(entity.Id);

        Assert.NotNull(savedEntity);
        Assert.Equal("Data", savedEntity.Data);
    }

    [Fact]
    public async Task Save_changes_async_should_propagate_cancellation_token()
    {
        // Arrange
        using var cts = new CancellationTokenSource();
        cts.Cancel();

        var entity = AggregateImpl.Create("Data");

        // Act
        await _context.AggregateImpls.AddAsync(entity);

        // Assert
        await Assert.ThrowsAsync<OperationCanceledException>(() =>
            _unitOfWork.SaveChangesAsync(cts.Token)
        );
    }

    public void Dispose()
    {
        _context.Dispose();
        _connection.Close();
        _connection.Dispose();
    }
}

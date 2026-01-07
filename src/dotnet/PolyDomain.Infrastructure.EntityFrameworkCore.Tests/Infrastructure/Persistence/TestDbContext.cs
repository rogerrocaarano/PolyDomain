using Microsoft.EntityFrameworkCore;
using PolyDomain.Infrastructure.EntityFrameworkCore.Persistence;
using PolyDomain.Infrastructure.EntityFrameworkCore.Tests.Core.Primitives;

namespace PolyDomain.Infrastructure.EntityFrameworkCore.Tests.Infrastructure.Persistence;

public class TestDbContext : PolyDomainDbContext<TestDbContext>
{
    public DbSet<AggregateImpl> AggregateImpls { get; set; }
    public DbSet<ComplexAggregateImpl> ComplexAggregateImpls { get; set; }

    public TestDbContext(DbContextOptions<TestDbContext> options)
        : base(options) { }
}

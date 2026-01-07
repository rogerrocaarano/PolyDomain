using Microsoft.EntityFrameworkCore;
using PolyDomain.Infrastructure.EntityFrameworkCore.Extensions;

namespace PolyDomain.Infrastructure.EntityFrameworkCore.Persistence;

/// <summary>
/// A base DbContext that automatically configures PolyDomain primitives.
/// </summary>
public abstract class PolyDomainDbContext<TContext> : DbContext
    where TContext : DbContext
{
    protected PolyDomainDbContext(DbContextOptions<TContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyPolyDomainPrimitives();
    }
}

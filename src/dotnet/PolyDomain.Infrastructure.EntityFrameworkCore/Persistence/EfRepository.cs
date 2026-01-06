using Microsoft.EntityFrameworkCore;
using PolyDomain.Abstractions.Behaviours;
using PolyDomain.Abstractions.Patterns;
using PolyDomain.Abstractions.Primitives;

namespace PolyDomain.Infrastructure.EntityFrameworkCore.Persistence;

/// <summary>
/// Provides a base implementation for repositories using Entity Framework Core.
/// </summary>
/// <typeparam name="TContext">The type of the <see cref="DbContext"/>.</typeparam>
/// <typeparam name="TAggregate">The type of the aggregate root.</typeparam>
/// <typeparam name="TId">The type of the identifier for the aggregate root.</typeparam>
public abstract class EfRepository<TContext, TAggregate, TId> : IRepository<TAggregate, TId>
    where TContext : DbContext
    where TAggregate : class, IAggregateRoot<TId>
{
    /// <summary>
    /// The database context instance.
    /// </summary>
    protected readonly TContext Context;

    /// <summary>
    /// The <see cref="DbSet{TEntity}"/> for the current aggregate root.
    /// </summary>
    protected readonly DbSet<TAggregate> DbSet;

    /// <summary>
    /// Initializes a new instance of the <see cref="EfRepository{TContext, TAggregate, TId}"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <exception cref="ArgumentNullException">Thrown when the context is null.</exception>
    protected EfRepository(TContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
        DbSet = context.Set<TAggregate>();
    }

    public virtual async Task SaveAsync(
        TAggregate aggregate,
        CancellationToken cancellationToken = default
    )
    {
        // If already tracked, EF Core knows what to do.
        if (IsTracked(aggregate))
            return;

        var exists = await DbSet.AnyAsync(e => e.Id.Equals(aggregate.Id), cancellationToken);
        if (exists)
        {
            // if exists and not tracked we force Modified state
            DbSet.Update(aggregate);
            return;
        }
        // add as new entity
        await DbSet.AddAsync(aggregate, cancellationToken);
    }

    /// <summary>
    /// Checks if the specified aggregate is currently being tracked by the change tracker.
    /// </summary>
    /// <param name="aggregate">The aggregate to check.</param>
    /// <returns>True if the entity is tracked; otherwise, false.</returns>
    protected bool IsTracked(TAggregate aggregate)
    {
        return Context.Entry(aggregate).State != EntityState.Detached;
    }

    public virtual Task Remove(TAggregate aggregate)
    {
        if (aggregate is ISoftDeletable softDeletable)
        {
            // deletion metadata
            softDeletable.IsDeleted = true;
            softDeletable.DeletedOnUtc = DateTime.UtcNow;

            // If not tracked attach and mark as modified
            if (!IsTracked(aggregate))
            {
                DbSet.Update(aggregate);
            }
            return Task.CompletedTask;
        }

        // Fallback to physical delete
        DbSet.Remove(aggregate);
        return Task.CompletedTask;
    }

    public virtual async Task<TAggregate?> GetByIdAsync(
        TId id,
        CancellationToken cancellationToken = default
    )
    {
        // FindAsync is optimized for PK lookups and handles generic TId types natively.
        return await DbSet.FindAsync(new object?[] { id }, cancellationToken);
    }
}

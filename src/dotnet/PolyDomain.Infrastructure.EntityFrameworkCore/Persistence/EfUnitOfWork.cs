using Microsoft.EntityFrameworkCore;
using PolyDomain.Abstractions.Patterns;

namespace PolyDomain.Infrastructure.EntityFrameworkCore.Persistence;

public class EfUnitOfWork<TContext> : IUnitOfWork
    where TContext : DbContext
{
    private readonly TContext _context;

    public EfUnitOfWork(TContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}

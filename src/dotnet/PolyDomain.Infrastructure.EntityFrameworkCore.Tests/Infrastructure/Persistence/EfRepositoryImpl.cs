using PolyDomain.Abstractions.Primitives;
using PolyDomain.Infrastructure.EntityFrameworkCore.Persistence;

namespace PolyDomain.Infrastructure.EntityFrameworkCore.Tests.Infrastructure.Persistence;

public class EfRepositoryImpl<T> : EfRepository<TestDbContext, T, Guid>
    where T : class, IAggregateRoot<Guid>
{
    public EfRepositoryImpl(TestDbContext context)
        : base(context) { }
}

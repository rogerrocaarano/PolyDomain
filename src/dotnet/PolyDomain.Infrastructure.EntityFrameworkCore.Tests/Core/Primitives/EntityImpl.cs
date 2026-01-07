using PolyDomain.Core.Primitives;

namespace PolyDomain.Infrastructure.EntityFrameworkCore.Tests.Core.Primitives;

public class EntityImpl : Entity<Guid>
{
    public string Data { get; set; }

    public EntityImpl(string data)
        : base(Guid.NewGuid())
    {
        Data = data;
    }
}

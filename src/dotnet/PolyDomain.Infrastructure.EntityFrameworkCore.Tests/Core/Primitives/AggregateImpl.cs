using PolyDomain.Core.Primitives;

namespace PolyDomain.Infrastructure.EntityFrameworkCore.Tests.Core.Primitives;

public class AggregateImpl : AggregateRoot<Guid>
{
    public string Data { get; private set; }

    private AggregateImpl(string data)
        : base(Guid.NewGuid())
    {
        Data = data;
    }

    public static AggregateImpl Create(string data)
    {
        return new AggregateImpl(data);
    }

    public void UpdateData(string newData)
    {
        Data = newData;
    }
}

using PolyDomain.Abstractions.Primitives;

namespace PolyDomain.Infrastructure.EntityFrameworkCore.Tests.Core.Primitives;

public record ValueObjectImpl(string StringData, int IntData) : IValueObject;

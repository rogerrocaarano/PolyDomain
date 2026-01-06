using PolyDomain.Abstractions.Primitives;
using PolyDomain.Core.Exceptions;
using PolyDomain.Core.Primitives;

namespace PolyDomain.Core.Tests;

public class EntityBusinessRuleTests
{
    private class FakeEntity : Entity<int>
    {
        public FakeEntity()
            : base(1) { }

        public void InvokeCheckRule(IBusinessRule rule)
        {
            CheckRule(rule);
        }
    }

    private record StubBusinessRule(bool Broken, string Message) : IBusinessRule
    {
        public bool IsBroken() => Broken;
    }

    [Fact]
    public void check_rule_should_not_throw_exception_when_rule_is_satisfied()
    {
        // Arrange
        var entity = new FakeEntity();
        // Broken = false
        var validRule = new StubBusinessRule(Broken: false, Message: "All good");

        // Act & Assert
        var exception = Record.Exception(() => entity.InvokeCheckRule(validRule));

        Assert.Null(exception);
    }

    [Fact]
    public void check_rule_should_throw_exception_when_rule_is_broken()
    {
        // Arrange
        var entity = new FakeEntity();
        // Broken = true
        var brokenRule = new StubBusinessRule(Broken: true, Message: "Rule broken!");

        // Act & Assert
        var exception = Assert.Throws<BusinessRuleValidationException>(() =>
            entity.InvokeCheckRule(brokenRule)
        );

        // Verificamos integridad
        Assert.Equal(brokenRule, exception.BrokenRule);
        Assert.Equal("Rule broken!", exception.Details);
    }
}

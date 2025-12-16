using RosettaDomain.Core.Abstractions;
using RosettaDomain.Core.Primitives;

namespace RosettaDomain.Tests.Primitives;

public class AggregateRootTests
{
    public record TestDomainEvent(string Message) : IDomainEvent;

    private class FakeAggregate : AggregateRoot<Guid>
    {
        public FakeAggregate()
            : base(Guid.NewGuid()) { }

        public void ExecuteAction(string message)
        {
            var domainEvent = new TestDomainEvent(message);
            AddDomainEvent(domainEvent);
        }
    }

    [Fact]
    public void aggregate_should_register_domain_event_when_action_occurs()
    {
        // Arrange
        var aggregate = new FakeAggregate();

        // Act
        aggregate.ExecuteAction("Something happened");

        // Assert
        Assert.NotEmpty(aggregate.DomainEvents);
        Assert.Single(aggregate.DomainEvents);
    }

    [Fact]
    public void aggregate_should_store_correct_event_data()
    {
        // Arrange
        var aggregate = new FakeAggregate();
        var expectedMessage = "UserCreated";

        // Act
        aggregate.ExecuteAction(expectedMessage);

        // Assert
        var domainEvent = aggregate.DomainEvents.First() as TestDomainEvent;
        Assert.NotNull(domainEvent);
        Assert.Equal(expectedMessage, domainEvent.Message);
    }

    [Fact]
    public void clear_domain_events_should_remove_all_events()
    {
        // Arrange
        var aggregate = new FakeAggregate();
        aggregate.ExecuteAction("Event 1");
        aggregate.ExecuteAction("Event 2");

        // Pre-Assert (Sanity check)
        Assert.Equal(2, aggregate.DomainEvents.Count);

        // Act
        aggregate.ClearDomainEvents();

        // Assert
        Assert.Empty(aggregate.DomainEvents);
    }
}

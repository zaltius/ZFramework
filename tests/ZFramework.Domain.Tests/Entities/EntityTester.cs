using FluentAssertions;
using ZFramework.Domain.Events;
using ZFramework.Domain.Tests.Entities.Implementations;
using Xunit;

namespace ZFramework.Domain.Tests.Entities;

public class EntityTester : DomainTester
{
    private readonly IDomainEvent domainEvent = new TestDomainEvent();

    private readonly TestEntity entityTest = new();

    public EntityTester()
    {
    }

    [Fact]
    public void Should_add_domain_event()
    {
        entityTest.AddEntityTestDomainEvent(domainEvent);

        entityTest.GetDomainEvents().Should().NotBeNull();
        entityTest.GetDomainEvents().Should().HaveCount(1);
    }

    [Fact]
    public void Should_clear_all_domain_events()
    {
        entityTest.AddEntityTestDomainEvent(domainEvent);
        entityTest.ClearDomainEvents();

        entityTest.GetDomainEvents().Should().NotBeNull();
        entityTest.GetDomainEvents().Should().HaveCount(0);
    }

    [Fact]
    public void Should_have_creation_time()
    {
        entityTest.CreationTime.Date.Should().Be(DateTime.Now.Date);
    }

    [Fact]
    public void Should_remove_domain_event()
    {
        entityTest.AddEntityTestDomainEvent(domainEvent);
        entityTest.RemoveEntityTestDomainEvent(domainEvent);

        entityTest.GetDomainEvents().Should().NotBeNull();
        entityTest.GetDomainEvents().Should().HaveCount(0);
    }
}
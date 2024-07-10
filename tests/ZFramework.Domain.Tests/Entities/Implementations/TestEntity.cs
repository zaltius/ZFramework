using ZFramework.Domain.Entities.Auditing;
using ZFramework.Domain.Events;

namespace ZFramework.Domain.Tests.Entities.Implementations;

public class TestEntity : FullAuditedEntity<Guid>
{
    public string? Property { get; set; }

    public void AddEntityTestDomainEvent(IDomainEvent domainEvent)
    {
        AddDomainEvent(domainEvent);
    }

    public void RemoveEntityTestDomainEvent(IDomainEvent domainEvent)
    {
        RemoveDomainEvent(domainEvent);
    }
}
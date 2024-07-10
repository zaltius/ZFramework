using FluentAssertions;
using ZFramework.Common.Exceptions;
using ZFramework.Domain.Entities.Auditing;
using ZFramework.Domain.Tests.Entities.Implementations;
using Xunit;

namespace ZFramework.Domain.Tests.Entities;

public class AuditedExtensionsHelperTester : DomainTester
{
    private readonly TestEntity entityTest = new();

    public AuditedExtensionsHelperTester()
    {
    }

    [Fact]
    public void Should_check_concurrency_fail()
    {
        var modifiedEntityTest = new TestEntity
        {
            LastModificationTime = entityTest.LastModificationTime?.AddSeconds(-1)
        };

        entityTest.Invoking(e => e.CheckConcurrencyUpdate(modifiedEntityTest))
            .Should()
            .Throw<ConcurrentUpdateException>();
    }

    [Fact]
    public void Should_check_concurrency_success()
    {
        var modifiedEntityTest = new TestEntity
        {
            LastModificationTime = entityTest.LastModificationTime
        };

        entityTest.Invoking(e => e.CheckConcurrencyUpdate(modifiedEntityTest))
            .Should()
            .NotThrow<Exception>();
    }

    protected override void SetUp()
    {
        entityTest.LastModificationTime = DateTime.UtcNow;
    }
}
using FluentAssertions;
using ZFramework.Domain.Tests.Entities.Implementations;
using Xunit;

namespace ZFramework.Infrastructure.EntityFrameworkCore.Tests.Auditing;

public sealed class AuditingTester : InfrastructureTester
{
    private AuditingTestData _testData;

    public AuditingTester() : base()
    {
        SetUp();
    }

    [Fact]
    public void Should_correct_audit_properties_for_new_entities()
    {
        var testEntity1 = TestEntityRepository.Insert(_testData.NewEntityWithFakeAuditing1);
        var testEntity2 = TestEntityRepository.Insert(_testData.NewEntityWithFakeAuditing2);

        UnitOfWork.Commit();

        testEntity1.CreationTime.Should().BeAfter(DateTime.UtcNow.AddMinutes(-1));
        testEntity1.CreationTime.Should().BeBefore(DateTime.UtcNow.AddMinutes(1));

        testEntity1.LastModificationTime.Should().NotBeNull();
        testEntity1.LastModificationTime.Should().BeAfter(DateTime.UtcNow.AddMinutes(-1));
        testEntity1.LastModificationTime.Should().BeBefore(DateTime.UtcNow.AddMinutes(1));

        testEntity1.IsDeleted.Should().BeFalse();
        testEntity1.DeletionTime.Should().BeNull();

        testEntity2.CreationTime.Should().BeAfter(DateTime.UtcNow.AddMinutes(-1));
        testEntity2.CreationTime.Should().BeBefore(DateTime.UtcNow.AddMinutes(1));

        testEntity2.LastModificationTime.Should().NotBeNull();
        testEntity2.LastModificationTime.Should().BeAfter(DateTime.UtcNow.AddMinutes(-1));
        testEntity2.LastModificationTime.Should().BeBefore(DateTime.UtcNow.AddMinutes(1));

        testEntity2.IsDeleted.Should().BeTrue();
        testEntity2.DeletionTime.Should().NotBeNull();
        testEntity2.DeletionTime.Should().BeAfter(DateTime.UtcNow.AddMinutes(-1));
        testEntity2.DeletionTime.Should().BeBefore(DateTime.UtcNow.AddMinutes(1));
    }

    [Fact]
    public void Should_correct_audit_properties_for_updated_entities()
    {
        var testEntity1 = TestEntityRepository.Insert(_testData.NewEntityWithFakeAuditing1);
        var testEntity3 = TestEntityRepository.Insert(_testData.NewEntityWithoutAuditing3);

        UnitOfWork.Commit();

        var oldTestEntity1LastModificationTime = testEntity1.LastModificationTime!;
        var oldTestEntity3LastModificationTime = testEntity3.LastModificationTime!;

        testEntity1.CreationTime = _testData.NewEntityWithFakeAuditing1.CreationTime;
        testEntity1.LastModificationTime = _testData.NewEntityWithFakeAuditing1.LastModificationTime;
        testEntity1.DeletionTime = _testData.NewEntityWithFakeAuditing1.DeletionTime;
        testEntity1.IsDeleted = _testData.NewEntityWithFakeAuditing1.IsDeleted;

        testEntity3.CreationTime = _testData.ExistingEntityWithFakeAuditing3.CreationTime;
        testEntity3.LastModificationTime = _testData.ExistingEntityWithFakeAuditing3.LastModificationTime;
        testEntity3.DeletionTime = _testData.ExistingEntityWithFakeAuditing3.DeletionTime;
        testEntity3.IsDeleted = _testData.ExistingEntityWithFakeAuditing3.IsDeleted;

        testEntity1 = TestEntityRepository.Update(testEntity1);
        testEntity3 = TestEntityRepository.Update(testEntity3);

        UnitOfWork.Commit();

        testEntity1.CreationTime.Should().BeAfter(DateTime.UtcNow.AddMinutes(-1));
        testEntity1.CreationTime.Should().BeBefore(DateTime.UtcNow.AddMinutes(1));

        testEntity1.LastModificationTime.Should().NotBeNull();
        testEntity1.LastModificationTime.Should().BeAfter(oldTestEntity1LastModificationTime.Value);
        testEntity1.LastModificationTime.Should().BeAfter(DateTime.UtcNow.AddMinutes(-1));
        testEntity1.LastModificationTime.Should().BeBefore(DateTime.UtcNow.AddMinutes(1));

        testEntity1.IsDeleted.Should().BeFalse();
        testEntity1.DeletionTime.Should().BeNull();

        testEntity3.CreationTime.Should().BeAfter(DateTime.UtcNow.AddMinutes(-1));
        testEntity3.CreationTime.Should().BeBefore(DateTime.UtcNow.AddMinutes(1));

        testEntity3.LastModificationTime.Should().NotBeNull();
        testEntity3.LastModificationTime.Should().BeAfter(oldTestEntity3LastModificationTime.Value);
        testEntity3.LastModificationTime.Should().BeAfter(DateTime.UtcNow.AddMinutes(-1));
        testEntity3.LastModificationTime.Should().BeBefore(DateTime.UtcNow.AddMinutes(1));

        testEntity3.IsDeleted.Should().BeTrue();
        testEntity3.DeletionTime.Should().NotBeNull();
        testEntity3.DeletionTime.Should().BeAfter(DateTime.UtcNow.AddMinutes(-1));
        testEntity3.DeletionTime.Should().BeBefore(DateTime.UtcNow.AddMinutes(1));
    }

    protected override void SetUp()
    {
        var newEntityWithFakeAuditing1 = new TestEntity()
        {
            CreationTime = DateTime.MaxValue,
            DeletionTime = DateTime.MaxValue,
            IsDeleted = false,
            LastModificationTime = DateTime.MaxValue
        };

        var newEntityWithFakeAuditing2 = new TestEntity()
        {
            CreationTime = DateTime.MaxValue,
            DeletionTime = DateTime.MaxValue,
            IsDeleted = true,
            LastModificationTime = DateTime.MaxValue,
        };

        var newEntityWithoutAuditing3 = new TestEntity();

        var existingEntityWithFakeAuditing1 = new TestEntity()
        {
            CreationTime = DateTime.MinValue,
            DeletionTime = DateTime.MinValue,
            IsDeleted = false,
            LastModificationTime = DateTime.MinValue,
        };

        var existingEntityWithFakeAuditing3 = new TestEntity()
        {
            CreationTime = DateTime.MinValue,
            DeletionTime = DateTime.MinValue,
            IsDeleted = true,
            LastModificationTime = DateTime.MinValue,
        };

        _testData = new AuditingTestData()
        {
            NewEntityWithFakeAuditing1 = newEntityWithFakeAuditing1,
            NewEntityWithFakeAuditing2 = newEntityWithFakeAuditing2,
            ExistingEntityWithFakeAuditing1 = existingEntityWithFakeAuditing1,
            ExistingEntityWithFakeAuditing3 = existingEntityWithFakeAuditing3,
            NewEntityWithoutAuditing3 = newEntityWithoutAuditing3
        };
    }
}
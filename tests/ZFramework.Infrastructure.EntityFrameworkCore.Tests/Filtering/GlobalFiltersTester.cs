using FluentAssertions;
using ZFramework.Domain.Tests.Entities.Implementations;
using Xunit;

namespace ZFramework.Infrastructure.EntityFrameworkCore.Tests.Filtering;

public sealed class GlobalFiltersTester : InfrastructureTester
{
    private TestEntity _entityToBeDeleted;

    public GlobalFiltersTester()
    {
        SetUp();
    }

    [Fact]
    public void Should_hide_soft_deleted_entities()
    {
        var nonDeletedEntities = TestEntityRepository.GetAll();

        _entityToBeDeleted.IsDeleted = true;

        _entityToBeDeleted = TestEntityRepository.Update(_entityToBeDeleted);

        UnitOfWork.Commit();

        var deletedEntities = TestEntityRepository.GetAll();

        nonDeletedEntities.Items.Any(d => d.Id.Equals(_entityToBeDeleted.Id)).Should().BeTrue();
        deletedEntities.Items.Any(d => d.Id.Equals(_entityToBeDeleted.Id)).Should().BeFalse();
    }

    protected override void SetUp()
    {
        _entityToBeDeleted = new TestEntity() { Property = "TEST 1" };

        TestDbContext.EntityTests.Add(_entityToBeDeleted);

        UnitOfWork.Commit();
    }
}
using FluentAssertions;
using ZFramework.Common.Filtering;
using ZFramework.Domain.Tests.Entities.Implementations;
using Xunit;

namespace ZFramework.Infrastructure.EntityFrameworkCore.Tests.Repositories;

public class RepositoryBaseTester : InfrastructureTester
{
    private TestEntity _entityTest1;

    private TestEntity _entityTest2;

    private int _insertedItems;

    public RepositoryBaseTester() : base()
    {
        SetUp();
    }

    [Fact]
    public void Should_delete_entity()
    {
        TestDbContext.EntityTests.Local.Should().Contain(_entityTest1);

        TestEntityRepository.DeleteById(_entityTest1.Id);

        TestDbContext.EntityTests.Local.Should().NotContain(_entityTest1);
    }

    [Fact]
    public void Should_get_all_with_filtering()
    {
        var filteringOptions = new FilteringOptions()
        {
            Filters = new Dictionary<string, string>()
                {
                    { nameof(_entityTest2.Property), _entityTest2.Property! }
                }
        };

        var result = TestEntityRepository.GetAll(filteringOptions);

        result.Should().NotBeNull();
        result.CurrentPage.Should().Be(1);
        result.TotalCount.Should().Be(1);
        result.Items.Should().HaveCount(1);
        result!.Items.First().Should().Be(_entityTest2);
    }

    [Fact]
    public void Should_get_all_with_sorting()
    {
        var filteringOptions = new FilteringOptions()
        {
            SortingCriteria = "Property DESC"
        };

        var result = TestEntityRepository.GetAll(filteringOptions);

        result.Should().NotBeNull();
        result.CurrentPage.Should().Be(1);
        result.TotalCount.Should().Be(2);
        result.Items.Should().HaveCount(_insertedItems);
        result!.Items.ElementAt(0).Should().Be(_entityTest2);
        result!.Items.ElementAt(1).Should().Be(_entityTest1);
    }

    [Fact]
    public void Should_get_all_with_sorting_and_paging()
    {
        var filteringOptions = new FilteringOptions()
        {
            SortingCriteria = "Property",
            MaxResultCount = 1,
            SkipCount = 0
        };

        var resultSkip0 = TestEntityRepository.GetAll(filteringOptions);

        filteringOptions.SkipCount = 1;

        var resultSkip1 = TestEntityRepository.GetAll(filteringOptions);

        resultSkip0.Should().NotBeNull();
        resultSkip0.CurrentPage.Should().Be(1);
        resultSkip0.TotalCount.Should().Be(2);
        resultSkip0.Items.Should().HaveCount(filteringOptions.MaxResultCount);
        resultSkip0!.Items.ElementAt(0).Should().Be(_entityTest1);

        resultSkip1.Should().NotBeNull();
        resultSkip1.CurrentPage.Should().Be(2);
        resultSkip1.TotalCount.Should().Be(2);
        resultSkip1.Items.Should().HaveCount(filteringOptions.MaxResultCount);
        resultSkip1!.Items.ElementAt(0).Should().Be(_entityTest2);
    }

    [Fact]
    public void Should_get_all_without_filters()
    {
        var result = TestEntityRepository.GetAll();

        result.Should().NotBeNull();
        result.CurrentPage.Should().BeNull();
        result.TotalCount.Should().Be(2);
        result!.Items.Should().HaveCount(_insertedItems);
    }

    [Fact]
    public void Should_get_by_id()
    {
        var result = TestEntityRepository.GetById(_entityTest1.Id);

        result.Should().NotBeNull();
        result!.Id.Should().Be(_entityTest1.Id);
    }

    [Fact]
    public void Should_insert_new_entity()
    {
        var entityTest = new TestEntity() { Property = "INSERT TEST" };

        TestEntityRepository.Insert(entityTest);

        var result = TestDbContext.EntityTests.Find(entityTest.Id);

        result.Should().NotBeNull();
        result!.Id.Should().Be(entityTest.Id);
    }

    [Fact]
    public void Should_update_entity()
    {
        _entityTest1.Property += "UPDATED";

        var updatedEntity1 = TestEntityRepository.Update(_entityTest1);

        updatedEntity1.Should().NotBeNull();
        updatedEntity1!.Property.Should().Be(_entityTest1.Property);
        updatedEntity1!.LastModificationTime?.Date.Should().Be(DateTime.UtcNow.Date);
        updatedEntity1!.IsDeleted.Should().BeFalse();

        var dbContextEntity = TestDbContext.EntityTests.Local.First(i => i.Id.Equals(_entityTest1.Id));

        dbContextEntity.Property.Should().Be(_entityTest1.Property);
    }

    protected override void SetUp()
    {
        _entityTest1 = new TestEntity() { Property = "TEST 1" };
        _entityTest2 = new TestEntity() { Property = "TEST 2" };

        TestDbContext.EntityTests.Add(_entityTest1);
        TestDbContext.EntityTests.Add(_entityTest2);

        UnitOfWork.Commit();

        _insertedItems = 2;
    }
}
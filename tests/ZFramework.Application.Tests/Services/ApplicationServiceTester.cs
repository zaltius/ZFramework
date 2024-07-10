using FluentAssertions;
using ZFramework.Application.Tests.Services.Implementations;
using ZFramework.Common.Exceptions;
using ZFramework.Common.Filtering;
using Xunit;

namespace ZFramework.Application.Tests.Services
{
    public class ApplicationServiceTester : ApplicationTester
    {
        private ApplicationServiceTestData _testData;

        public ApplicationServiceTester()
        {
            SetUp();
        }

        [Fact]
        public void Should_delete_by_id()
        {
            var testEntityDtoCreation1 = TestEntityApplicationService.Insert(_testData.TestEntityCreationDto1);
            var testEntityDtoCreation2 = TestEntityApplicationService.Insert(_testData.TestEntityCreationDto2);

            var testEntityReadingDto1 = TestEntityApplicationService.GetById(testEntityDtoCreation1.Id);

            TestEntityApplicationService.DeleteById(testEntityDtoCreation1.Id);

            var deletedTestEntityDto = TestEntityApplicationService.GetById(testEntityDtoCreation1.Id);

            try
            {
                // It does not matter wether it throws an exception.
                TestEntityApplicationService.DeleteById(Guid.NewGuid());
            }
            catch { }

            var testEntityReadingDto2 = TestEntityApplicationService.GetById(testEntityDtoCreation2.Id);

            testEntityReadingDto1.Should().NotBeNull();
            deletedTestEntityDto.Should().BeNull();
            testEntityReadingDto2.Should().NotBeNull();
        }

        [Fact]
        public void Should_get_all_with_filtering()
        {
            TestEntityApplicationService.Insert(_testData.TestEntityCreationDto1);
            TestEntityApplicationService.Insert(_testData.TestEntityCreationDto2);

            var filteredTestEntities = TestEntityApplicationService.GetAll(_testData.FilteringOptions);

            filteredTestEntities.TotalCount.Should().Be(2);
            filteredTestEntities.CurrentPage.Should().Be(2);
            filteredTestEntities.Items.Should().HaveCount(1);
            filteredTestEntities.Items.First().Property.Should().Be(_testData.TestEntityCreationDto1.Property);
        }

        [Fact]
        public void Should_get_all_without_filtering()
        {
            TestEntityApplicationService.Insert(_testData.TestEntityCreationDto1);
            TestEntityApplicationService.Insert(_testData.TestEntityCreationDto2);

            var nonFilteredTestEntities = TestEntityApplicationService.GetAll();

            nonFilteredTestEntities.TotalCount.Should().Be(2);
            nonFilteredTestEntities.CurrentPage.Should().BeNull();
            nonFilteredTestEntities.Items.Should().HaveCount(2);
            nonFilteredTestEntities.Items.First().Property.Should().Be(_testData.TestEntityCreationDto1.Property);
        }

        [Fact]
        public void Should_get_by_id()
        {
            var testEntityReadingDto1 = TestEntityApplicationService.Insert(_testData.TestEntityCreationDto1);

            var testEntity1 = TestEntityApplicationService.GetById(testEntityReadingDto1.Id);

            var fakeTestEntity = TestEntityApplicationService.GetById(Guid.NewGuid());

            testEntity1.Id.Should().Be(testEntityReadingDto1.Id);
            fakeTestEntity.Should().BeNull();
        }

        [Fact]
        public void Should_insert()
        {
            var testEntityCreationDto1 = TestEntityApplicationService.Insert(_testData.TestEntityCreationDto1);

            var testEntityReadingDto1 = TestEntityApplicationService.GetById(testEntityCreationDto1.Id);

            testEntityReadingDto1.Property.Should().Be(_testData.TestEntityCreationDto1.Property);
            TestEntityApplicationService.GetAll().Items.Should().HaveCount(1);
        }

        [Fact]
        public void Should_throw_getting_all_with_fake_filtering()
        {
            TestEntityApplicationService.Insert(_testData.TestEntityCreationDto1);
            TestEntityApplicationService.Insert(_testData.TestEntityCreationDto2);

            TestEntityApplicationService.Invoking(s => s.GetAll(_testData.FakeFilteringOptions))
                .Should().Throw<FailedValidationException>();
        }

        [Fact]
        public void Should_throw_updaing_invalid_entity()
        {
            TestEntityApplicationService.Invoking(s => s.Update(_testData.FakeTestEntityUpdateDto))
                .Should().Throw<NullEntityException>();
        }

        [Fact]
        public void Should_update()
        {
            var testEntityCreationDto1 = TestEntityApplicationService.Insert(_testData.TestEntityCreationDto1);

            TestEntityApplicationService.Update(_testData.TestEntityUpdatedDto1);

            var result = TestEntityApplicationService.GetById(testEntityCreationDto1.Id);

            result.Property.Should().Be(_testData.TestEntityUpdatedDto1.Property);
        }

        protected override void SetUp()
        {
            var testEntityCreationDto1 = new TestEntityDto()
            {
                Id = Guid.NewGuid(),
                Property = "Test 1"
            };

            var testEntityUpdatedDto = new TestEntityDto()
            {
                Id = testEntityCreationDto1.Id,
                Property = "Test 1 updated"
            };

            var testEntityCreationDto2 = new TestEntityDto()
            {
                Id = Guid.NewGuid(),
                Property = "Test 2"
            };

            var fakeTestEntityUpdateDto = new TestEntityDto()
            {
                Id = Guid.NewGuid(),
                Property = "Test 3"
            };

            var filteringOptions = new FilteringOptions()
            {
                MaxResultCount = 1,
                SkipCount = 1,
                SortingCriteria = "Property DESC"
            };

            var fakeFilteringOptions = new FilteringOptions()
            {
                SortingCriteria = "F A K E"
            };

            _testData = new ApplicationServiceTestData()
            {
                TestEntityCreationDto1 = testEntityCreationDto1,
                TestEntityCreationDto2 = testEntityCreationDto2,
                TestEntityUpdatedDto1 = testEntityUpdatedDto,
                FakeTestEntityUpdateDto = fakeTestEntityUpdateDto,
                FilteringOptions = filteringOptions,
                FakeFilteringOptions = fakeFilteringOptions
            };
        }
    }
}
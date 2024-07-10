using ZFramework.Application.Tests.Services.Implementations;
using ZFramework.Common.Filtering;

namespace ZFramework.Application.Tests.Services
{
    internal sealed class ApplicationServiceTestData
    {
        public IFilteringOptions FakeFilteringOptions { get; set; }

        public TestEntityDto FakeTestEntityUpdateDto { get; set; }

        public IFilteringOptions FilteringOptions { get; set; }

        public TestEntityDto TestEntityCreationDto1 { get; set; }

        public TestEntityDto TestEntityCreationDto2 { get; set; }

        public TestEntityDto TestEntityUpdatedDto1 { get; set; }
    }
}
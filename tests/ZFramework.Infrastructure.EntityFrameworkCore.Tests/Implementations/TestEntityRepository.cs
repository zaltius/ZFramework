using ZFramework.Domain.Tests.Entities.Implementations;
using ZFramework.Infrastructure.EntityFrameworkCore.Repositories;

namespace ZFramework.Infrastructure.EntityFrameworkCore.Tests.Implementations;

public class TestEntityRepository : RepositoryBase<TestEntity, Guid, TestDbContext>, ITestEntityRepository
{
    public TestEntityRepository(TestDbContext dbContext) : base(dbContext)
    {
    }
}
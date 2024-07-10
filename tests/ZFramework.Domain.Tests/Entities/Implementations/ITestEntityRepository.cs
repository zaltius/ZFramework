using ZFramework.Domain.EntityFrameworkCore;

namespace ZFramework.Domain.Tests.Entities.Implementations;

public interface ITestEntityRepository : IRepository<TestEntity, Guid>
{
}
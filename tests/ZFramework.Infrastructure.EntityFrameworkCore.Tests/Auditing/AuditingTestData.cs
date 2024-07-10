using ZFramework.Domain.Tests.Entities.Implementations;

namespace ZFramework.Infrastructure.EntityFrameworkCore.Tests.Auditing;

public class AuditingTestData
{
    public TestEntity ExistingEntityWithFakeAuditing1 { get; set; }

    public TestEntity ExistingEntityWithFakeAuditing3 { get; set; }

    public TestEntity NewEntityWithFakeAuditing1 { get; set; }

    public TestEntity NewEntityWithFakeAuditing2 { get; set; }

    public TestEntity NewEntityWithoutAuditing3 { get; set; }
}